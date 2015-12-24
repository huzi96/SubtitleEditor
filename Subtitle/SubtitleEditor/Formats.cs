using System;

namespace SubtitleEditor.Subtitle
{
    /// <summary>
    /// 获取受支持的字幕格式。
    /// </summary>
    public static class Formats
    {
        /// <summary>
        /// 获取支持将.srt作为Srt格式读写的IFormat。
        /// </summary>
        public static IFormat<Srt.ISrtDocument> Srt
        {
            get
            {
                return Subtitle.Srt.SrtFormat.Instance;
            }
        }
    }
}
