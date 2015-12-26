using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubtitleEditorForm
{
    public delegate void ChangeHandler(object sender, EventArgs args); 

    public partial class AdjustTimeController : UserControl
    {
        public event ChangeHandler TimeChange;
        
        public AdjustTimeController()
        {
            InitializeComponent();
            numericHourEnd.ValueChanged += (sender,args) => { TimeChange(sender, args); };
            numericMinuteEnd.ValueChanged += (sender, args) => { TimeChange(sender, args); };
            numericSecondEnd.ValueChanged += (sender, args) => { TimeChange(sender, args); };
            numericHourStart.ValueChanged += (sender, args) => { TimeChange(sender, args); };
            numericMinuteStart.ValueChanged += (sender, args) => { TimeChange(sender, args); };
            numericSecondStart.ValueChanged += (sender, args) => { TimeChange(sender, args); };
        }
        public TimeSpan StartTime
        {
            set
            {
                TimeSpan starttime = value;
                numericHourStart.Value = starttime.Hours;
                numericMinuteStart.Value = starttime.Minutes;
                numericSecondStart.Value = starttime.Seconds;
            }
            get
            {
                return new TimeSpan(Convert.ToInt32(numericHourStart.Value),
                    Convert.ToInt32(numericMinuteStart.Value),
                    Convert.ToInt32(numericSecondStart.Value));
            }
        }
        public TimeSpan EndTime
        {
            set
            {
                TimeSpan endtime = value;
                numericHourEnd.Value = endtime.Hours;
                numericMinuteEnd.Value = endtime.Minutes;
                numericSecondEnd.Value = endtime.Seconds;
            }
            get
            {
                return new TimeSpan(Convert.ToInt32(numericHourEnd.Value),
                    Convert.ToInt32(numericMinuteEnd.Value),
                    Convert.ToInt32(numericSecondEnd.Value));
            }
        }
        public string subline
        {
            get
            {
                return richCaption.Text;
            }
            set
            {
                richCaption.Text = value;
            }
        }

    }

}
