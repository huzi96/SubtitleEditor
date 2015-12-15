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
	//产生一条时间戳记录
	void Push(TimeSpan begin, TimeSpan end, string text);

	delegate bool SubtitlePredicate(Subtitle.Srt.ISrtEntry entry);

	// 删除符合predicate的字幕记录
	void RemoveAll(SubtitlePredicate predicate);
	// 返回符合predicate的字幕记录
	IEnumerable<Subtitle.Srt.ISrtEntry> Filter(SubtitlePredicate predicate);

	//导出srt文件
	void WriteTo(string filename);

}

//然后controller应该可以放在form类里，接受键盘事件和按钮事件并调用相应方法
