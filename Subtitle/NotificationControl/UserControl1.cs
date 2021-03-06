﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotificationControl
{
    public partial class Notification: UserControl
    {
        const int ControlWidth = 1025;
        const int ControlHeight = 30;

        double percent;
        string message;

        Label OuterBorder;
        Label InnerBorder;
        Label ProcessBar;
        Label Percent;
        Label Message;

        Color word_color = Color.Purple;

        System.Windows.Forms.Timer timer;

        public Notification()
        {
            InitializeComponent();
            this.BackColor = Color.Blue;
            Width = ControlWidth;
            Height = ControlHeight;

            OuterBorder = new Label();
            InnerBorder = new Label();
            ProcessBar = new Label();
            Percent = new Label();
            Message = new Label();

            percent = 0;
            message = "通知栏";

            AdaptLabelSize();

            this.Controls.Add(Message);
            this.Controls.Add(Percent);
            this.Controls.Add(ProcessBar);
            this.Controls.Add(InnerBorder);
            this.Controls.Add(OuterBorder);

            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += timer_Tick;
            timer.Enabled = true;
        }

        private void AdaptLabelSize()          //根据控件的大小调整Label的大小
        {
            OuterBorder.Top = this.Height * 2 / 30;
            OuterBorder.Left = this.Width * 600 / 1000;
            OuterBorder.Height = this.Height * 26 / 30;
            OuterBorder.Width = this.Width * 300 / 1000;
            OuterBorder.BackColor = Color.White;

            InnerBorder.Top = OuterBorder.Top + 2;
            InnerBorder.Left = OuterBorder.Left + 2;
            InnerBorder.Height = OuterBorder.Height - 4;
            InnerBorder.Width = OuterBorder.Width - 4;
            InnerBorder.BackColor = Color.Blue;

            ProcessBar.Top = InnerBorder.Top + 2;
            ProcessBar.Left = InnerBorder.Left + 2;
            ProcessBar.Height = InnerBorder.Height - 4;
            ProcessBar.Width = Convert.ToInt32(percent * (InnerBorder.Width - 4) / 100);
            ProcessBar.BackColor = Color.White;

             Percent.Top = this.Top + 2;
            Percent.Left = OuterBorder.Left +OuterBorder.Width + 20;
            Percent.Font = new Font("黑体", 12);
            Percent.ForeColor = word_color;
            Percent.Text = percent + "%";

            Message.Top = this.Top + 2;
            Message.Left = this.Left + 10;
            Message.Height = this.Height * 26 / 30;
            Message.Width = this.Width * 590 / 1000;
            Message.Font = new Font("黑体", 12);
            Message.ForeColor = word_color;
            Message.Text = message;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Fresh();
        }

        private void Fresh()
        {
            ProcessBar.Width = Convert.ToInt32(percent * (InnerBorder.Width - 4) / 100);
            Percent.Text = percent + "%";
            Message.Text = message;
        }

        public void ShowProcessBar(bool flag)
        {
            OuterBorder.Visible = flag;
            InnerBorder.Visible = flag;
            ProcessBar.Visible = flag;
            Percent.Visible = flag;
        }

        public void SetPercent(double value)
        {
            percent = value;
        }

        public void SetMessage(string value)
        {
            message = value;
        }

        public void SetWidth(int _width)
        {
            Width = _width;
            AdaptLabelSize();
        }

        public void SetHeight(int _height)
        {
            Height = _height;
            AdaptLabelSize();
        }
    }
}
