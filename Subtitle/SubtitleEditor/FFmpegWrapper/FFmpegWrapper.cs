﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SubtitleEditor.FFmpeg
{
    public class FFmpegWrapper
    {
        //对这些参数指定的视频文件调用ffmpeg进行字幕渲染
        public void Render(String videoFileName, String srtFileName, String TargetFile = "")
        {
            string extention = videoFileName.Split('.').Last<string>();
            if (TargetFile == "")
            {
                TargetFile = videoFileName + ".out.mp4";
            }
            if (TargetFile == videoFileName)
            {
                throw new Exception("Will overwrite the original file");
            }
            const string ffmpegFilePath = @"..\bin\ffmpeg.exe";
            Process prc = new Process();
            prc.StartInfo.CreateNoWindow = false;
            prc.StartInfo.FileName = ffmpegFilePath;
            prc.StartInfo.UseShellExecute = true;
            prc.StartInfo.RedirectStandardError = false;
            prc.StartInfo.RedirectStandardInput = false;
            prc.StartInfo.RedirectStandardOutput = false;
            prc.EnableRaisingEvents = true;
            
            prc.StartInfo.Arguments = string.Format("-y -i {0} -vf subtitles={1} {2}", videoFileName,srtFileName,TargetFile);
            prc.Exited += (sender, args) => { this.renderComplete(sender, args); };
            prc.Start();
        }
        public delegate void renderEventHandler(object sender, System.EventArgs arg);
        //异步渲染完成的事件
        public event renderEventHandler renderComplete;
    }
}
