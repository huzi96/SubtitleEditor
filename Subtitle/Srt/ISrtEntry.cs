using System;

namespace SubtitleEditor.Subtitle.Srt
{
    /// <summary>
    /// 表示一条可以被写入为Srt格式的字幕。
    /// </summary>
    public interface ISrtEntry
    {
        /// <summary>
        /// 获取/设置字幕的开始时间。
        /// </summary>
        TimeSpan Begin
        {
            get;
            set;
        }

        /// <summary>
        /// 获取/设置字幕的结束时间。
        /// 若设置的时间小于开始时间，将被设置为开始时间。
        /// </summary>
        TimeSpan End
        {
            get;
            set;
        }

        /// <summary>
        /// 获取/设置字幕的持续时间。
        /// 若设置的时间为负，将被设置为0。
        /// 当持续时间被更改，字幕的开始时间保持不变。
        /// </summary>
        TimeSpan Duration
        {
            get;
            set;
        }

        /// <summary>
        /// 获取/设置字幕的内容。
        /// </summary>
        string Text
        {
            get;
            set;
        }

        /// <summary>
        /// 获取/设置字幕的显示矩形。
        /// 当不需要指定显示矩形，值为（设置为）null。
        /// </summary>
        IRectangle Rectangle
        {
            get;
            set;
        }
    }
}
