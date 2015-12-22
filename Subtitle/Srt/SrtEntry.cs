using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleEditor.Subtitle.Srt
{
    public class SrtEntry : ISrtEntry
    {
        private TimeSpan begin, duration;
        private string text;
        private IRectangle rectangle;

        public TimeSpan Begin
        {
            get
            {
                return begin;
            }
            set
            {
                begin = value;
            }
        }
        public TimeSpan Duration
        {
            get
            {
                return duration;
            }
            set
            {
                if (value < TimeSpan.Zero)
                {
                    duration = TimeSpan.Zero;
                    return;
                }
                duration = value;
            }
        }
        public TimeSpan End
        {
            get
            {
                return Begin + Duration;
            }
            set
            {
                Duration = value - Begin;
            }
        }
        public IRectangle Rectangle
        {
            get
            {
                return rectangle;
            }
            set
            {
                rectangle = value;
            }
        }
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        public SrtEntry(TimeSpan begin, TimeSpan duration, string text, IRectangle rectangle = null)
        {
            this.begin = begin;
            if (duration > TimeSpan.Zero)
            {
                this.duration = duration;
            }
            else
            {
                this.duration = TimeSpan.Zero;
            }
            this.text = text;
            this.rectangle = rectangle;
        }

        public bool Equals(ISrtEntry other)
        {
            if (ReferenceEquals(other, null))
                return false;
            return (Begin == other.Begin) && (Duration == other.Duration) && (End == other.End) && (Text == other.Text);
        }

        public override bool Equals(object obj)
        {
            var other = obj as ISrtEntry;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return begin.GetHashCode() ^ duration.GetHashCode() ^ text.GetHashCode();
        }
    }
}
