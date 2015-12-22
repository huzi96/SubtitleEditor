using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace SubtitleEditor.FFmpeg
{
    class FFmpegWrapper
    {
        //对这些参数指定的视频文件调用ffmpeg进行字幕渲染
        public void Render(String videoFileName, String srtFileName, String TargetFile = "")
        {
            string extention = videoFileName.Split('.').Last<string>();
            if (TargetFile == "")
            {
                TargetFile = "out." + extention;
            }
            if (TargetFile == videoFileName)
            {
                throw new Exception("Will overwrite the original file");
            }
            const string ffmpegFilePath = @"..\bin\ffmpeg.exe";
            Process prc = new Process();
            prc.StartInfo.CreateNoWindow = true;
            prc.StartInfo.FileName = ffmpegFilePath;
            prc.StartInfo.UseShellExecute = false;
            prc.StartInfo.RedirectStandardError = false;
            prc.StartInfo.RedirectStandardInput = true;
            prc.StartInfo.RedirectStandardOutput = false;
            prc.EnableRaisingEvents = true;
            
            prc.StartInfo.Arguments = string.Format("-i {0} -vf subtitles={1} {2}", videoFileName,srtFileName,TargetFile);
            prc.Exited += (sender, args) => { this.renderComplete(sender, args); MessageBox.Show("finish"); };
            prc.Start();
        }
        public delegate void renderEventHandler(object sender, System.EventArgs arg);
        //异步渲染完成的事件
        public event renderEventHandler renderComplete;
    }
}
