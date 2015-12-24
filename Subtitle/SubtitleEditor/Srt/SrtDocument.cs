using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleEditor.Subtitle.Srt
{
    public class SrtDocument : ISrtDocument
    {
        private List<ISrtEntry> items;

        public SrtDocument(IEnumerable<ISrtEntry> items)
        {
            this.items = new List<ISrtEntry>(items.OrderBy((i) => i.Begin));
        }

        public IEnumerable<ISrtEntry> SrtItems
        {
            get
            {
                return items;
            }
        }
    }
}
