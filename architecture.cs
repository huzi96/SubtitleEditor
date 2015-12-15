// 错误应该抛出异常
class Player
{
	//控制WMP控件（或者是其它播放控件
	//可以从WMP派生？

	//加载媒体文件
	void Load(string filename);
	void Play();
	void Pause();
	
	//当前进度时间
	TimeSpan Time
	{
		get;
		set;
	}
}

class Muxer
{
	//混流
	//ffmpeg
	void Mux(string mediaFile, string srtFile);
}

class Timeline
{
	void Tap()
	{
	//用户按下开始，停止键表示一条字幕的开始和结束
	//如果之前没有按下开始键，那么这次调用就是表示开始
	//否则表示结束
	//如果是开始则记录下开始时间
	//如果是结束则产生一条时间戳记录

	}

	//用户按下结束键，结束记录，可以生成SRT文件了
	void Terminate();
	//导出srt文件
	void WriteTo(string filename);

}

//然后controller应该可以放在form类里，接受键盘事件和按钮事件并调用相应方法
