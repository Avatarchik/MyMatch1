/*******************************************************
 * 
 * 文件名(File Name)：             SpriteTitleChange.cs
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/03/01 13:30:13
 *
 *******************************************************/

using UnityEngine;
using System.Collections;

namespace MyFrameWork
{
	public class EventDef
	{
		public const string Unkown = "Unkown";

		//测试事件
		public static class TestEvent
		{
			public const string Test1 = "TestEvent.Test1";
			public const string Test2 = "TestEvent.Test2";
			public const string Test3 = "TestEvent.Test3";
		}

		// 定义事件参数类

		// 网络事件
		public static class NetworkEvent
		{
			public const string Connect = "NetworkEvent.Connect"; //连接请求事件
			public const string OnClose = "NetworkEvent.OnClose";
			public const string OnDataRecv = "NetworkEvent.OnDataRecv";
			public const string OnConnected = "NetworkEvent.OnConnected";
		}
	}
}
