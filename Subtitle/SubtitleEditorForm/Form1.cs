using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SubtitleEditor.Subtitle.Srt;
using SubtitleEditor.FFmpeg;

namespace SubtitleEditorForm
{
    public partial class MainForm : Form
    {
        Model model;
        FFmpegWrapper renderer;
        int fakePersent = 0;
        public MainForm()
        {
            InitializeComponent();
            renderer = new FFmpegWrapper();
            notification1.ShowProcessBar(false);
            model = new Model(this);
            /* 禁用最大化窗体按钮 */
            this.MaximizeBox = false;

            /* 注册事件处理函数 */
            control.Click += model.clickHandler;
            button_play.Click += model.playAndPauseHandler;
            /* 执行testing任务 */
            this.NewSubt_Click(this, new EventArgs());
        }

        static bool isSrtExported = false;
        //字幕是否来自文件（保存/另存为用）
        static bool isVideoOpen = false;
        //视频是否已经打开
        static bool isSubtitleOpen = false;
        //字幕是否已经打开
        //上面两个可以用于判断是否可以添加字幕进行操作……
        static String currentSrtPath = "";
        //当前字幕文件的路径
        static String currentVideoPath = "";
        //当前视频文件的路径
        static String[] subtitles = null;

        /*
         * 以下为菜单栏的事件
         */

        /*
         * 关闭视频
         */
        private void CloseVideo_Click(object sender, EventArgs e)
        {
            this.CloseViedo.Enabled = false;
            this.button_play.Enabled = false;
            isVideoOpen = false;
            this.button_play.Text = "播放";
            currentVideoPath = "";
            model.media.SetURL(currentVideoPath);
        }

        /*
         * 从文件夹中打开一个视频文件
         */
        private void OpenVideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            { 
                currentVideoPath = ofd.FileName;
                this.CloseViedo.Enabled = true;
                this.button_play.Enabled = true;
                model.media.SetURL(currentVideoPath);
            }
        }

        /*
         * 新建一个字幕文件，解除编辑限制
         * 编辑后可以另存为，没有另存为前该字幕文件没有路径（未保存）……
         */
        private void NewSubt_Click(object sender, EventArgs e)
        {
            this.EditArea.Enabled = true;
            this.SaveSubtAs.Enabled = true;
            this.SaveSubt.Enabled = false;
            this.EditArea.Text = "";
            isSubtitleOpen = true;
            currentSrtPath = "";
            model.newTask();
            model.testing();
        }

        /*
         * 打开字幕文件，可选的类型是txt和srt格式
         * 字幕文件的内容会显示在编辑框内
         */
        private void LoadSubt_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "srt files (*.srt)|*.srt|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.EditArea.Enabled = true;
                currentSrtPath = ofd.FileName;
                isSrtExported = true;
                isSubtitleOpen = true;
                this.SaveSubt.Enabled = true;
                StreamReader sr = new StreamReader(ofd.FileName);
                this.EditArea.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        /*
         * 保存对字幕文件的编辑修改
         * 当且仅当字幕文件是由文件打开或者新建已另存为的情况下才有效
         */
        private void SaveSubt_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "srt files (*.srt)|*.srt|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            StreamWriter sw = new StreamWriter(currentSrtPath);
            sw.Write(this.EditArea.Text);
            sw.Close();
        }

        /*
         * 另存为字幕文件
         */
        private void SaveSubtAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "srt files (*.srt)|*.srt|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream srtfile = new FileStream(sfd.FileName, FileMode.OpenOrCreate);
                model.export(srtfile);
                srtfile.Close();
                isSrtExported = true;
                currentSrtPath = sfd.FileName;
                this.SaveSubt.Enabled = true;
            }
        }

        /*
         * 显示帮助信息
         */
        private void Help_Click(object sender, EventArgs e)
        {
            String help = "使用说明:\n\n" + "此程序主要用于在视频中添加字幕。\n"
                + "菜单栏中可以对视频，字幕文件进行操作以及查看帮助和制作者。\n"
                + "载入视频后请点击“播放”或者视频控制栏的播放键以播放视频。\n"
                + "右侧的字幕编辑区可以编辑字幕，已添加的字幕请尽量不要修改。\n"
                + "请在需要让当前行字幕出现的时候点击“行始”按钮。\n"
                + "请在需要让当前行字幕消失的时候点击“行终”按钮。\n"
                + "最下方显示了通知栏，视频下方显示了当前字幕和时间。\n"
                + "最后在菜单栏中点击“将字幕渲染到视频”即可完成操作。";
            MessageBox.Show(help);
        }

        /*
         * 显示作者信息
         */
        private void Authors_Click(object sender, EventArgs e)
        {
            String authors = "制作者：\n\n"
                + "信息科学技术学院 \t胡越予\n"
                + "信息科学技术学院 \t陳嘉輝\n"
                + "法学院 \t\t吴嘉桐\n"
                + "心理学系 \t\t汪效锐\n"
                + "信息管理系 \t张志豪";
            MessageBox.Show(authors);
        }

        /*
         * 添加字幕的控制按钮
         * 在显示“开始”时按下表示该条字幕在何时开始显示
         * 在显示“结束”时按下表示该条字幕在何时停止显示
         */
        private void control_Click(object sender, EventArgs e)
        {
            TimeCheck.Enabled = true;
            if (this.control.Text == "行始" && model.line_number < model.lines.Length)
            {
                this.control.Text = "行终";
            }
            else
            {
                if (model.line_number >= model.lines.Length)
                {
                    this.Restart.Visible = true;
                    this.Restart.Enabled = true;
                }
                this.control.Text = "行始";
            }
            subtitles = model.lines;
        }

        /*
         * 返回视频路径
         */
        public String GetVideoPath()
        {
            return currentVideoPath;
        }

        /*
         * 返回字幕路径
         */
        public String GetSubtitlePath()
        {
            return currentSrtPath;
        }

        /*
         * 播放的按键
         */
        private void button_play_Click(object sender, EventArgs e)
        {
            control.Enabled = true;
            if(button_play.Text != "暂停")
                this.button_play.Text = "暂停";
            else if (button_play.Text == "暂停")
                this.button_play.Text = "继续";
        }

        /*
         * 点击该按钮时当前要添加的字幕回到最上端
         */
        private void Restart_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer.Ctlcontrols.stop();
            axWindowsMediaPlayer.Ctlcontrols.currentPosition = 0;
            model.line_number = 0;
            this.control.Text = "开始";
            timer_sub.Start();
            model.media.play();
            this.Restart.Enabled = false;
            this.Restart.Visible = false;
        }

        /*
         * 找到第一处修改的字幕的行数
         */
        private int FindFirstEdited()
        {
            for (int index = 0; index < subtitles.Length; index++)
            {
                if (index < model.lines.Length)
                {
                    if (subtitles[index] != model.lines[index])
                        return index;
                }
                else
                {
                    if (subtitles[index] != "")
                        return index;
                }
            }
            return 299999999;
        }

        private void TimeCheck_Tick(object sender, EventArgs e)
        {
            if (FindFirstEdited() <= model.line_number)
            {
                TimeCheck.Stop();
                MessageBox.Show("提醒：在已添加的字幕中修改可能导致糟糕的后果。");
            }
        }

        private void timer_sub_Tick(object sender, EventArgs e)
        {
            TimeSpan pos = model.media.GetCurrentPosition();
            SrtEntry theEntry =  model.Srt_Entries.Find((entry) => 
            { return (entry.Begin < pos && entry.Begin + entry.Duration > pos); });
            if(theEntry != null)
            {
                model.adjust.adjust.StartTime = theEntry.Begin;
                model.adjust.adjust.EndTime = theEntry.Begin + theEntry.Duration;
                model.adjust.adjust.subline = theEntry.Text;
            }
            else
            {
                model.adjust.adjust.subline = "";
            }
        }

        private void Render_Click_handler(object sender, EventArgs e)
        {
            timer_responding.Interval = 50;
            timer_responding.Start();
            notification1.SetMessage("正在渲染，请稍等");
            notification1.ShowProcessBar(true);
            renderer.renderComplete += (sdr, args) => { notification1.SetMessage("生成完成"); timer_responding.Stop(); notification1.SetPercent(100); };
            FileStream tempfs = new FileStream("temp.srt", FileMode.OpenOrCreate);
            model.export(tempfs, new UTF8Encoding());
            tempfs.Close();
            renderer.Render(currentVideoPath, "temp.srt");
        }

        private void timer_responding_Tick(object sender, EventArgs e)
        {
            int gap = 10;
            notification1.SetPercent((fakePersent % 9000) / 100.0);
            if (fakePersent < 9000)
            {
                fakePersent+=gap;
            }
        }

        private void btn_Clear_entries_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer.Ctlcontrols.stop();
            axWindowsMediaPlayer.Ctlcontrols.currentPosition = 0;
            model.line_number = 0;
            model.Srt_Entries.Clear();
            this.control.Text = "开始";
            notification1.SetMessage("已刷新");
        }
    }
}
