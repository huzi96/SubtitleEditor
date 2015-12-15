using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleEditor.Subtitle
{
    public struct Rectangle: IRectangle
    {
        int x1, x2, y1, y2;

        public int X1
        {
            get
            {
                return x1;
            }

            set
            {
                x1 = value;
            }
        }

        public int X2
        {
            get
            {
                return x2;
            }

            set
            {
                x2 = value;
            }
        }

        public int Y1
        {
            get
            {
                return y1;
            }

            set
            {
                y1 = value;
            }
        }

        public int Y2
        {
            get
            {
                return y2;
            }

            set
            {
                y2 = value;
            }
        }
    }
}
