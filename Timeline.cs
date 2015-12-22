using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

using SubtitleEditor.Subtitle.Srt;

namespace SubtitleEditor
{
    internal class Timeline
    {
        public delegate bool SubtitlePredicate(ISrtEntry entry);

        private LinkedList<ISrtEntry> entries;

        public Timeline()
        {
            entries = new LinkedList<ISrtEntry>();
        }

        /// <summary>
        /// 产生一条时间戳记录
        /// </summary>
        /// <param name="begin">字幕条目开始时间</param>
        /// <param name="end">字幕条目结束时间</param>
        /// <param name="text">字幕内容</param>
        public void Push(TimeSpan begin, TimeSpan end, string text)
        {
            Push(new SrtEntry(begin, end - begin, text));
        }

        void Push(Subtitle.Srt.ISrtEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException("entry");
            entries.AddLast(entry);
        }

        /// <summary>
        /// 删除一个字幕记录。
        /// </summary>
        /// <param name="entry">要删除的记录。</param>
        public void Remove(ISrtEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException("entry");
            RemoveAll((i) => entry.Equals(i));
        }

        /// <summary>
        /// 删除符合predicate的字幕记录。
        /// </summary>
        /// <param name="predicate">删除记录的标准。</param>
        public void RemoveAll(SubtitlePredicate predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            var current = entries.First;
            while (current != null)
            {
                if (predicate(current.Value))
                {
                    var next = current.Next;
                    entries.Remove(current);
                    current = next;
                    continue;
                }
                current = current.Next;
            }
        }

        /// <summary>
        /// 返回符合predicate的字幕记录。
        /// </summary>
        /// <param name="predicate">选择记录的标准。</param>
        /// <returns>字幕记录的集合。</returns>
        public IEnumerable<ISrtEntry> Filter(SubtitlePredicate predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            return entries.Where((i) => predicate(i));
        }

        /// <summary>
        /// 返回第一个符合predicate的字幕记录。
        /// </summary>
        /// <param name="predicate">选择记录的标准。</param>
        /// <returns>字幕记录或null，如果没有符合标准的记录。</returns>
        public ISrtEntry FilterOne(SubtitlePredicate predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            var allFiltered = Filter(predicate);
            if (allFiltered.Any())
                return allFiltered.First();
            return null;
        }

        /// <summary>
        /// 导出srt文件。
        /// </summary>
        /// <param name="filename">输出文件名。</param>
        public void WriteTo(string filename)
        {
            WriteToAsync(filename).Wait();
        }

        async Task WriteToAsync(string filename)
        {
            using (var file = File.Open(filename, FileMode.Create))
                await WriteToAsync(file);
        }

        async Task WriteToAsync(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            await Subtitle.Formats.Srt.WriteToAsync(stream, new SrtDocument(entries));
        }
    }
}
