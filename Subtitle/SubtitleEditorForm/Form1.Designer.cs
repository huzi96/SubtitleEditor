namespace SubtitleEditorForm
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.Video = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenVideo = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseViedo = new System.Windows.Forms.ToolStripMenuItem();
            this.Subt = new System.Windows.Forms.ToolStripMenuItem();
            this.NewSubt = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadSubt = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveSubt = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveSubtAs = new System.Windows.Forms.ToolStripMenuItem();
            this.Explanation = new System.Windows.Forms.ToolStripMenuItem();
            this.Help = new System.Windows.Forms.ToolStripMenuItem();
            this.Authors = new System.Windows.Forms.ToolStripMenuItem();
            this.SubtitleArea = new System.Windows.Forms.Label();
            this.EditArea = new System.Windows.Forms.RichTextBox();
            this.VideoLabel = new System.Windows.Forms.Label();
            this.control = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.button_play = new System.Windows.Forms.Button();
            this.button_export = new System.Windows.Forms.Button();
            this.Restart = new System.Windows.Forms.Button();
            this.TimeCheck = new System.Windows.Forms.Timer(this.components);
            this.notification1 = new NotificationControl.Notification();
            this.axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.AutoSize = false;
            this.Menu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Video,
            this.Subt,
            this.Explanation});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
            this.Menu.Size = new System.Drawing.Size(1025, 28);
            this.Menu.TabIndex = 0;
            this.Menu.Text = "menuStrip1";
            // 
            // Video
            // 
            this.Video.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenVideo,
            this.CloseViedo});
            this.Video.Name = "Video";
            this.Video.Size = new System.Drawing.Size(74, 20);
            this.Video.Text = "视频";
            // 
            // OpenVideo
            // 
            this.OpenVideo.Name = "OpenVideo";
            this.OpenVideo.Size = new System.Drawing.Size(209, 38);
            this.OpenVideo.Text = "载入视频";
            this.OpenVideo.Click += new System.EventHandler(this.OpenVideo_Click);
            // 
            // CloseViedo
            // 
            this.CloseViedo.Enabled = false;
            this.CloseViedo.Name = "CloseViedo";
            this.CloseViedo.Size = new System.Drawing.Size(209, 38);
            this.CloseViedo.Text = "关闭视频";
            this.CloseViedo.Click += new System.EventHandler(this.CloseVideo_Click);
            // 
            // Subt
            // 
            this.Subt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewSubt,
            this.LoadSubt,
            this.toolStripMenuItem1,
            this.SaveSubt,
            this.SaveSubtAs});
            this.Subt.Name = "Subt";
            this.Subt.Size = new System.Drawing.Size(74, 20);
            this.Subt.Text = "字幕";
            // 
            // NewSubt
            // 
            this.NewSubt.Name = "NewSubt";
            this.NewSubt.Size = new System.Drawing.Size(233, 38);
            this.NewSubt.Text = "新建字幕";
            this.NewSubt.Click += new System.EventHandler(this.NewSubt_Click);
            // 
            // LoadSubt
            // 
            this.LoadSubt.Name = "LoadSubt";
            this.LoadSubt.Size = new System.Drawing.Size(233, 38);
            this.LoadSubt.Text = "载入字幕";
            this.LoadSubt.Click += new System.EventHandler(this.LoadSubt_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(230, 6);
            // 
            // SaveSubt
            // 
            this.SaveSubt.Enabled = false;
            this.SaveSubt.Name = "SaveSubt";
            this.SaveSubt.Size = new System.Drawing.Size(233, 38);
            this.SaveSubt.Text = "保存字幕";
            this.SaveSubt.Click += new System.EventHandler(this.SaveSubt_Click);
            // 
            // SaveSubtAs
            // 
            this.SaveSubtAs.Enabled = false;
            this.SaveSubtAs.Name = "SaveSubtAs";
            this.SaveSubtAs.Size = new System.Drawing.Size(233, 38);
            this.SaveSubtAs.Text = "另存为字幕";
            this.SaveSubtAs.Click += new System.EventHandler(this.SaveSubtAs_Click);
            // 
            // Explanation
            // 
            this.Explanation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Help,
            this.Authors});
            this.Explanation.Name = "Explanation";
            this.Explanation.Size = new System.Drawing.Size(74, 20);
            this.Explanation.Text = "说明";
            // 
            // Help
            // 
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(185, 38);
            this.Help.Text = "帮助";
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // Authors
            // 
            this.Authors.Name = "Authors";
            this.Authors.Size = new System.Drawing.Size(185, 38);
            this.Authors.Text = "制作者";
            this.Authors.Click += new System.EventHandler(this.Authors_Click);
            // 
            // SubtitleArea
            // 
            this.SubtitleArea.AutoSize = true;
            this.SubtitleArea.Font = new System.Drawing.Font("华文细黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SubtitleArea.Location = new System.Drawing.Point(635, 77);
            this.SubtitleArea.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.SubtitleArea.Name = "SubtitleArea";
            this.SubtitleArea.Size = new System.Drawing.Size(369, 41);
            this.SubtitleArea.TabIndex = 1;
            this.SubtitleArea.Text = "请在此处编辑字幕：";
            // 
            // EditArea
            // 
            this.EditArea.AcceptsTab = true;
            this.EditArea.AutoWordSelection = true;
            this.EditArea.Font = new System.Drawing.Font("Consolas", 9F);
            this.EditArea.Location = new System.Drawing.Point(639, 112);
            this.EditArea.Margin = new System.Windows.Forms.Padding(6);
            this.EditArea.Name = "EditArea";
            this.EditArea.Size = new System.Drawing.Size(338, 403);
            this.EditArea.TabIndex = 2;
            this.EditArea.Text = "";
            // 
            // VideoLabel
            // 
            this.VideoLabel.AutoSize = true;
            this.VideoLabel.Font = new System.Drawing.Font("华文细黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.VideoLabel.Location = new System.Drawing.Point(8, 77);
            this.VideoLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.VideoLabel.Name = "VideoLabel";
            this.VideoLabel.Size = new System.Drawing.Size(213, 41);
            this.VideoLabel.TabIndex = 3;
            this.VideoLabel.Text = "视频控制区";
            // 
            // control
            // 
            this.control.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.control.Location = new System.Drawing.Point(285, 518);
            this.control.Margin = new System.Windows.Forms.Padding(6);
            this.control.Name = "control";
            this.control.Size = new System.Drawing.Size(90, 35);
            this.control.TabIndex = 4;
            this.control.Text = "开始";
            this.control.UseVisualStyleBackColor = true;
            this.control.Click += new System.EventHandler(this.control_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // button_play
            // 
            this.button_play.Enabled = false;
            this.button_play.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_play.Location = new System.Drawing.Point(83, 518);
            this.button_play.Name = "button_play";
            this.button_play.Size = new System.Drawing.Size(90, 35);
            this.button_play.TabIndex = 6;
            this.button_play.Text = "播放";
            this.button_play.UseVisualStyleBackColor = true;
            this.button_play.Click += new System.EventHandler(this.button_play_Click);
            // 
            // button_export
            // 
            this.button_export.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_export.Location = new System.Drawing.Point(482, 518);
            this.button_export.Name = "button_export";
            this.button_export.Size = new System.Drawing.Size(81, 35);
            this.button_export.TabIndex = 7;
            this.button_export.Text = "导出";
            this.button_export.UseVisualStyleBackColor = true;
            // 
            // Restart
            // 
            this.Restart.Enabled = false;
            this.Restart.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Restart.Location = new System.Drawing.Point(730, 518);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(159, 35);
            this.Restart.TabIndex = 8;
            this.Restart.Text = "重新添加字幕";
            this.Restart.UseVisualStyleBackColor = true;
            this.Restart.Visible = false;
            this.Restart.Click += new System.EventHandler(this.Restart_Click);
            // 
            // TimeCheck
            // 
            this.TimeCheck.Interval = 200;
            this.TimeCheck.Tick += new System.EventHandler(this.TimeCheck_Tick);
            // 
            // notification1
            // 
            this.notification1.BackColor = System.Drawing.Color.PaleGreen;
            this.notification1.Location = new System.Drawing.Point(0, 31);
            this.notification1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.notification1.Name = "notification1";
            this.notification1.Size = new System.Drawing.Size(1025, 30);
            this.notification1.TabIndex = 9;
            // 
            // axWindowsMediaPlayer
            // 
            this.axWindowsMediaPlayer.Enabled = true;
            this.axWindowsMediaPlayer.Location = new System.Drawing.Point(12, 112);
            this.axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
            this.axWindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer.OcxState")));
            this.axWindowsMediaPlayer.Size = new System.Drawing.Size(640, 360);
            this.axWindowsMediaPlayer.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1009, 525);
            this.Controls.Add(this.notification1);
            this.Controls.Add(this.Restart);
            this.Controls.Add(this.button_export);
            this.Controls.Add(this.button_play);
            this.Controls.Add(this.axWindowsMediaPlayer);
            this.Controls.Add(this.control);
            this.Controls.Add(this.VideoLabel);
            this.Controls.Add(this.EditArea);
            this.Controls.Add(this.SubtitleArea);
            this.Controls.Add(this.Menu);
            this.MainMenuStrip = this.Menu;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximumSize = new System.Drawing.Size(2000, 800);
            this.MinimumSize = new System.Drawing.Size(1025, 563);
            this.Name = "MainForm";
            this.Text = "Subtitle Editor";
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem Video;
        private System.Windows.Forms.ToolStripMenuItem OpenVideo;
        private System.Windows.Forms.ToolStripMenuItem CloseViedo;
        private System.Windows.Forms.ToolStripMenuItem Subt;
        private System.Windows.Forms.ToolStripMenuItem NewSubt;
        private System.Windows.Forms.ToolStripMenuItem LoadSubt;
        private System.Windows.Forms.ToolStripMenuItem SaveSubt;
        private System.Windows.Forms.ToolStripMenuItem SaveSubtAs;
        private System.Windows.Forms.ToolStripMenuItem Explanation;
        private System.Windows.Forms.ToolStripMenuItem Help;
        private System.Windows.Forms.ToolStripMenuItem Authors;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.Label SubtitleArea;
        public System.Windows.Forms.RichTextBox EditArea;
        private System.Windows.Forms.Label VideoLabel;
        private System.Windows.Forms.Button control;
        public AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button button_play;
        private System.Windows.Forms.Button button_export;
        private System.Windows.Forms.Button Restart;
        private System.Windows.Forms.Timer TimeCheck;
        private NotificationControl.Notification notification1;
    }
}

