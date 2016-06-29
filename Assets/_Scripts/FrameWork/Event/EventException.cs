/*******************************************************
 * 
 * 文件名(File Name)：             SpriteTitleChange.cs
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/03/01 13:42:28
 *
 *******************************************************/


public class EventException : System.Exception 
{
	/// <summary>
	/// 使用指定的错误消息初始化 EventException 类的新实例。
	/// </summary>
	/// <param name="message">描述错误的消息。</param>
	public EventException(string message)
		: base(message)
	{
	}

	/// <summary>
	/// 使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 EventException 类的新实例。
	/// </summary>
	/// <param name="message">解释异常原因的错误消息。</param>
	/// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用。</param>
	public EventException(string message, System.Exception innerException)
		: base(message, innerException)
	{
	}
}
