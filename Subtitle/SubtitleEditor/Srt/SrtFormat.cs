using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubtitleEditor.Subtitle.Srt
{
    public class SrtFormat : IFormat<ISrtDocument>
    {
        private static SrtFormat instance = null;

        private SrtFormat()
        { }

        private TimeSpan ParseSrtTimeSpan(string s)
        {
            var match = new Regex(@"^(\d+):(\d+):(\d+),(\d+)$").Match(s);
            if (!match.Success)
                throw new InvalidDataException("期待SRT格式时间，获得：" + s);
            var ms = long.Parse(match.Groups[4].Value);
            ms += long.Parse(match.Groups[3].Value) * 1000;
            ms += long.Parse(match.Groups[2].Value) * 60000;
            ms += long.Parse(match.Groups[1].Value) * 3600000;
            return TimeSpan.FromMilliseconds(ms);
        }

        private async Task<ISrtEntry> ReadEntryAsync(TextReader reader, string head)
        {
            if (head == null)
                return null;

            var t = 0;
            var time = head;
            if (int.TryParse(head, out t))
                time = await reader.ReadLineAsync();

            var match = new Regex(@"^\s*(\d+:\d+:\d+,\d+)\s*-->\s*(\d+:\d+:\d+,\d+)").Match(time);
            if (!match.Success)
                throw new InvalidDataException("期待表示持续时间的SRT指令，获得：" + time);
            var begin = ParseSrtTimeSpan(match.Groups[1].Value);
            var end = ParseSrtTimeSpan(match.Groups[2].Value);
            var body = await ReadEntryBodyAsync(reader);

            var rectMatch = new Regex(@"X1\s*:\s*(\d+)\s*X2\s*:\s*(\d+)\s*Y1\s*:\s*(\d+)\s*Y2\s*:\s*(\d+)\s*$")
                .Match(time);
            IRectangle rect = null;
            if (rectMatch.Success)
            {
                rect = new Rectangle
                {
                    X1 = int.Parse(rectMatch.Groups[1].Value),
                    X2 = int.Parse(rectMatch.Groups[2].Value),
                    Y1 = int.Parse(rectMatch.Groups[3].Value),
                    Y2 = int.Parse(rectMatch.Groups[4].Value)
                };
            }

            return new SrtEntry(begin, end - begin, body, rect);
        }

        private async Task<string> ReadEntryBodyAsync(TextReader reader)
        {
            var line = await reader.ReadLineAsync();
            var body = line;
            while (line != "" && line != null)
                body += '\n' + (line = await reader.ReadLineAsync());
            return body;
        }

        private async Task<string> ReadBlankLinesAsync(TextReader reader)
        {
            var line = await reader.ReadLineAsync();
            while (line == "")
                line = await reader.ReadLineAsync();
            return line;
        }

        public ISrtDocument ReadFrom(Stream s)
        {
            var task = ReadFromAsync(s);
            task.Wait();
            return task.Result;
        }

        public async Task<ISrtDocument> ReadFromAsync(Stream s)
        {
            var list = new LinkedList<ISrtEntry>();
            using (var reader = new StreamReader(s,true))
            {
                while (!reader.EndOfStream)
                {
                    var head = await ReadBlankLinesAsync(reader);
                    if (head == null)
                        break;
                    var entry = await ReadEntryAsync(reader, head);
                    if (entry == null)
                        break;
                    list.AddLast(entry);
                }
            }
            return new SrtDocument(list);
        }

        public void WriteTo(Stream s, ISrtDocument data)
        {
            WriteTo(s, new UTF8Encoding(false), data);
        }

        public Task WriteToAsync(Stream s, ISrtDocument data)
        {
            return WriteToAsync(s, new UTF8Encoding(false), data);
        }

        public void WriteTo(Stream s, Encoding e, ISrtDocument data)
        {
            var task = WriteToAsync(s, e, data);
            task.Wait();
        }

        public async Task WriteToAsync(Stream s, Encoding e, ISrtDocument data)
        {
            using (var writer = new StreamWriter(s, e))
            {
                var srt = data.SrtItems.OrderBy((i) => i.Begin);
                var count = 1;
                foreach (var item in srt)
                {
                    // 1. 字幕条目顺序编号
                    await writer.WriteLineAsync(count.ToString());
                    ++count;
                    // 2.1. 字幕时间
                    await writer.WriteAsync(string.Format(
                        @"{0:hh\:mm\:ss\,fff} --> {1:hh\:mm\:ss\,fff}", item.Begin, item.End
                    ));
                    // 2.2. 字幕矩形
                    if (item.Rectangle != null)
                    {
                        await writer.WriteAsync(string.Format(
                            @" X1:{0:D} X2:{1:D} Y1:{2:D} Y2:{3:D}",
                            item.Rectangle.X1, item.Rectangle.X2,
                            item.Rectangle.Y1, item.Rectangle.Y2
                        ));
                    }
                    else
                    {
                        await writer.WriteLineAsync();
                    }
                    // 3. 字幕内容
                    await writer.WriteLineAsync(item.Text);
                    // 4. 空行标记条目结束
                    await writer.WriteLineAsync();
                }
            }
        }

        public static SrtFormat Instance
        {
            get
            {
                if (instance == null)
                    instance = new SrtFormat();
                return instance;
            }
        }
    }
}
