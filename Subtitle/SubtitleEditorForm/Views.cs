using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SubtitleEditor;
using SubtitleEditor.Subtitle.Srt;
using SubtitleEditor.Subtitle;
using System.Windows.Forms;
using System.IO;

namespace SubtitleEditorForm
{
    public class Model
    {
        public MediaControl media;
        public EditorControl editor;
        public AdjustControl adjust;
        public NotificationControl.Notification notification;
        public Timer timer_sub;

        public string[] lines
        {
            get
            {
                return editor.editor.Lines;
            }
        }
        public int line_number { set; get; }
        public List<SrtEntry> Srt_Entries;

        /// <summary>
        /// 在调用某些关于产生字幕的函数时增加一，通过奇偶判断此时是一行开头还是一行结尾
        /// </summary>
        int TickTock;

        TimeSpan last_begin, last_end;

        /// <summary>
        /// 主要的控制模型，与form互相拥有彼此的引用
        /// </summary>
        public Model(MainForm form)
        {
            /*初始化变量*/
            TickTock = 0;
            Srt_Entries = new List<SrtEntry>();
            /*建立model之下的部件*/
            media = new MediaControl(form.axWindowsMediaPlayer, form.GetVideoPath());
            editor = new EditorControl(form.EditArea);
            adjust = new AdjustControl(form.adjustTimeController1);
            notification = form.notification1;
            timer_sub = form.timer_sub;
        }

        public void newTask()
        {
            line_number = 0;
            Srt_Entries.Clear();
            TickTock = 0;
        }

        public void playAndPauseHandler(object sender, EventArgs args)
        {
            if (media.playing)
            {
                media.pause();
                media.playing = false;
            }
            else
            {
                media.play();
                media.playing = true;
            }
        }

        /// <summary>
        /// 导出srt内容到流
        /// </summary>
        /// <param name="s">要到处到的流</param>
        public void export(System.IO.Stream s)
        {
            SrtDocument srt_doc = new SrtDocument(Srt_Entries);
            var srt = Formats.Srt;
            srt.WriteTo(s, System.Text.Encoding.Default ,srt_doc);
        }

        public void export(System.IO.Stream s, System.Text.Encoding encoding)
        {
            SrtDocument srt_doc = new SrtDocument(Srt_Entries);
            var srt = Formats.Srt;
            srt.WriteTo(s, encoding, srt_doc);
        }

        /// <summary>
        /// 每次调用产生一次ticktock的跳变,在一半的情况下输出一条记录到srt_entry里
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void clickHandler(object sender, EventArgs args)
        {
            if (TickTock % 2 == 1)
            {
                last_end = media.GetCurrentPosition();
                try
                {
                    Srt_Entries.Add(new SrtEntry(last_begin, last_end - last_begin, lines[line_number]));

                    adjust.adjust.subline = lines[line_number];
                    adjust.adjust.EndTime = last_end;
                }
                catch (IndexOutOfRangeException e)
                {
                    MessageBox.Show("已经到达字幕底部");

                    TickTock = 1;
                }
                /* 下面设置adjust control */
                line_number++;

            }
            else
            {
                try
                {
                    last_begin = media.GetCurrentPosition();
                    editor.SetLineHighlight(line_number);
                    adjust.adjust.StartTime = last_begin;
                    adjust.adjust.EndTime = new TimeSpan(0);
                    adjust.adjust.subline = lines[line_number];
                }
                catch (IndexOutOfRangeException e)
                {
                    notification.SetMessage("到达字幕底部");
                }
            }
            TickTock++;
            TickTock %= 10;
        }

        /// <summary>
        /// 只是为了做个测试
        /// </summary>
        public void testing()
        {
            editor.Fill("There is a fire starting in my heart\nReaching a fever pitch");
        }
    }
    public class MediaControl
    {
        const int ControlWidth = 600;
        const int ControlHeight = 400;
        AxWMPLib.AxWindowsMediaPlayer main_player;
        public bool playing;

        /// <summary>
        /// 媒体控制类，传入一个windows media player 对象和这个对象对应的媒体文件url
        /// </summary>
        public MediaControl(AxWMPLib.AxWindowsMediaPlayer player, string url)
        {
            main_player = player;
            main_player.Ctlcontrols.currentPosition = 0;
            main_player.URL = url;
            main_player.Width = ControlWidth;
            main_player.Height = ControlHeight;

            playing = false;
        }

        public void SetURL(string url)
        {
            main_player.URL = url;
            main_player.Ctlcontrols.stop();
        }

        /// <summary>
        /// 获取当前的视频进度
        /// </summary>
        public TimeSpan GetCurrentPosition()
        {
            return TimeSpan.FromSeconds(main_player.Ctlcontrols.currentPosition);
        }

        /// <summary>
        /// 播放
        /// </summary>
        public void play()
        {
            main_player.Ctlcontrols.play();
        }

        public void pause()
        {
            main_player.Ctlcontrols.pause();
        }
        
    }
    public class EditorControl
    {
        public RichTextBox editor;
        int lastline;
        public EditorControl(RichTextBox editor)
        {
            this.editor = editor;
        }
        
        public void Fill(String text)
        {
            editor.Text = text;
        }
        public void Fill(string[] lines)
        {
            editor.Clear();
            foreach (string line in lines)
            {
                editor.Text += line;
                editor.Text += '\n';
            }
        }
        public void AddLine(String text)
        {
            editor.Text += text + '\n';
        }
        public void AddLine(string []lines)
        {
            foreach (string line in lines)
            {
                editor.Text += line + '\n';
            }
        }
        public void SetLineHighlight(int linenum)
        {
            int lastlen = editor.Lines[lastline].Length;
            editor.Select(editor.GetFirstCharIndexFromLine(lastline), lastlen);
            editor.SelectionColor = System.Drawing.Color.Black;
            editor.SelectionFont = new System.Drawing.Font("Consolas", 9);

            try
            {
                int len = editor.Lines[linenum].Length;
                lastline = linenum;
                editor.Select(editor.GetFirstCharIndexFromLine(linenum), len);
                editor.SelectionColor = System.Drawing.Color.Blue;
                editor.SelectionFont = new System.Drawing.Font("Consolas", 9, System.Drawing.FontStyle.Bold);
            }
            catch (IndexOutOfRangeException e)
            {
                MessageBox.Show("已经到达字幕底部！");
            }
        }
        public string get_line(int linenum)
        {
            return editor.Lines[linenum];
        }
        public string get_line()
        {
            return editor.Lines[lastline];
        }
    }
    public class AdjustControl
    {
        public AdjustTimeController adjust;

        public AdjustControl(AdjustTimeController _cont)
        {
            adjust = _cont;
        }

    }

}
