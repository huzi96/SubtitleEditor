using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleEditor.Subtitle.Srt
{
    public class SrtFormat : IFormat<ISrtDocument>
    {
        private static SrtFormat instance = null;

        private SrtFormat()
        { }

        public ISrtDocument ReadFrom(Stream s)
        {
            var task = ReadFromAsync(s);
            task.RunSynchronously();
            return task.Result;
        }

        public Task<ISrtDocument> ReadFromAsync(Stream s)
        {
            throw new NotImplementedException();
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
            task.RunSynchronously();
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
