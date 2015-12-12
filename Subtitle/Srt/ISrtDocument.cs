using System.Collections.Generic;

namespace SubtitleEditor.Subtitle.Srt
{
    /// <summary>
    /// 表示一个Srt格式字幕文件。
    /// </summary>
    public interface ISrtDocument
    {
        /// <summary>
        /// 获取内含字幕条目的可遍历集合。
        /// </summary>
        IEnumerable<ISrtEntry> SrtItems
        {
            get;
        }
    }
}
