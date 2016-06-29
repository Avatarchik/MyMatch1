/*******************************************************
 * 
 * 文件名(File Name)：             DDOLSingleton
 *
 * 作者(Author)：                  Yangzj
 *
 * 创建时间(CreateTime):           2016/02/25 12:07:04
 *
 *******************************************************/

using UnityEngine;
using System.Collections;

namespace MyFrameWork
{
	/// <summary>
	/// 不销毁的mono单例类
	/// </summary>
	public class DDOLSingleton<T> : MonoBehaviour where T : DDOLSingleton<T>
	{
		public static T _instance = null;

		public static T Instance
		{
			get{
				if (null == _instance)
				{
					GameObject go = GameObject.Find("DDOLGameObject");
					if (null == go)
					{
						go = new GameObject("DDOLGameObject");
						DontDestroyOnLoad(go);
					}

					_instance = go.GetOrAddComponent<T>();

				}
				return _instance;
			}
		}

		/// <summary>
		/// Raises the application quit event.
		/// </summary>
		private void OnApplicationQuit ()
		{
			_instance = null;
		}

		public virtual void ReleaseValue()
		{
		}
	}
}
