using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleEditor.Subtitle
{
    /// <summary>
    /// 表示一个可以作为特定数据结构读写的格式。
    /// </summary>
    /// <typeparam name="T">支持读写的数据结构。</typeparam>
    public interface IFormat<T>
    {
        /// <summary>
        /// 从流中读取数据。
        /// </summary>
        /// <param name="s">要从中读取的流。</param>
        /// <returns>从流中读取的数据。</returns>
        T ReadFrom(Stream s);

        /// <summary>
        /// 从流中异步读取数据。
        /// </summary>
        /// <param name="s">要从中读取的流。</param>
        /// <returns>从流中读取的数据。</returns>
        Task<T> ReadFromAsync(Stream s);

        /// <summary>
        /// 使用默认编码向流中写入数据。
        /// </summary>
        /// <param name="s">要向中写入的流。</param>
        /// <param name="data">要写入的数据。</param>
        void WriteTo(Stream s, T data);

        /// <summary>
        /// 使用默认编码向流中异步写入数据。
        /// </summary>
        /// <param name="s">要向中写入的流。</param>
        /// <param name="data">要写入的数据。</param>
        Task WriteToAsync(Stream s, T data);

        /// <summary>
        /// 向流中写入数据。
        /// </summary>
        /// <param name="s">要向中写入的流。</param>
        /// <param name="e">要使用的编码。</param>
        /// <param name="data">要写入的数据。</param>
        void WriteTo(Stream s, Encoding e, T data);

        /// <summary>
        /// 向流中异步写入数据。
        /// </summary>
        /// <param name="s">要向中写入的流。</param>
        /// <param name="e">要使用的编码。</param>
        /// <param name="data">要写入的数据。</param>
        Task WriteToAsync(Stream s, Encoding e, T data);
    }
}
