using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SubtitleEditorForm
{
    public partial class MainForm : Form
    {
        Model model;
        public MainForm()
        {
            InitializeComponent();
            model = new Model(this);

            /* 注册事件处理函数 */
            control.Click += model.clickHandler;
            //button_play.Click += (sender,args)=> { model.media.play(); } ;
            button_export.Click += (sender, args) => { model.testing_export(); };
            /* 执行testing任务 */
            model.testing();
        }

        static bool isSubtitleOpenFromFile = false;
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

        /*
         * 以下为菜单栏的事件
         */

        /*
         * 关闭视频
         */
        private void CloseVideo_Click(object sender, EventArgs e)
        {
            /*
             * close the video
             */
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
            /*
             * open the video
             */
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
                isSubtitleOpenFromFile = true;
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
                StreamWriter sw = new StreamWriter(sfd.FileName);
                sw.Write(this.EditArea.Text);
                sw.Close();
                isSubtitleOpenFromFile = true;
                currentSrtPath = sfd.FileName;
                this.SaveSubt.Enabled = true;
            }
        }

        /*
         * 显示帮助信息
         * 未完成
         */
        private void Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("");
        }

        /*
         * 显示作者信息
         * 未完成
         */
        private void Authors_Click(object sender, EventArgs e)
        {
            MessageBox.Show("");
        }

        /*
         * 添加字幕的控制按钮
         * 在显示“开始”时按下表示该条字幕在何时开始显示
         * 在显示“结束”时按下表示该条字幕在何时停止显示
         */
        private void control_Click(object sender, EventArgs e)
        {
            if (this.control.Text == "开始")
            {
                this.control.Text = "结束";
                //...
            }
            else
            {
                this.control.Text = "开始";
                //...
            }
        }

        /*
         * 返回视频路径（useful？）
         */
        public String GetVideoPath()
        {
            return currentVideoPath;
        }

        /*
         * 返回字幕路径（useful？）
         */
        public String GetSubtitlePath()
        {
            return currentSrtPath;
        }

        /*
         * 播放的按键……
         * 刚载入视频时需要点击该按钮才能开始播放（似乎没法重新播放？）
         * 
         * 视频播放结束后点击该按钮可以再次播放（如何判断？）
         * -> 写一个方法使得视频播放结束之后按钮的文字恢复为“播放”？
         */
        private void button_play_Click(object sender, EventArgs e)
        {
            model.media.play();
            if(button_play.Text != "暂停")
                this.button_play.Text = "暂停";
            else if (button_play.Text == "暂停")
                this.button_play.Text = "继续";
        }
        
    }
}
