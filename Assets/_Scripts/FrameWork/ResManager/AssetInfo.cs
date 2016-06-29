/*******************************************************
 * 
 * 文件名(File Name)：             AssetInfo
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/02/25 15:25:56
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace MyFrameWork
{
	/// <summary>
	/// 加载成功的资源
	/// </summary>
	public class AssetInfo 
	{
		/// <summary>
		/// 保存的资源
		/// </summary>
		public object Asset;
		/// <summary>
		/// 是否常驻内存
		/// </summary>
		public bool IsKeepInMemory;
		/// <summary>
		/// 堆栈计数
		/// </summary>
		public int StatckCount = 0;
	}

	/// <summary>
	/// 加载请求
	/// </summary>
	public class RequestInfo
	{
		public bool IsKeepInMemory;

		/// <summary>
		/// 加载结果回调
		/// </summary>
		public List<System.Action<object>> Listeners;
		/// <summary>
		/// Adds the listener.
		/// </summary>
		/// <param name="listener">Listener.</param>
		public void AddListener(System.Action<object> listener)
		{
			if(listener != null && !Listeners.Contains(listener))
			{
				Listeners.Add(listener);
			}
		}

		/// <summary>
		/// 资源名称
		/// </summary>
		public string AssetName;
		/// <summary>
		/// 全资源路径
		/// </summary>
		/// <value>The name of the asset full.</value>
		public string AssetFullName
		{
			get
			{
				return AppFacade.Instance.GetManager<ResourceMgr>(ManagerName.Resource).GetFileFullName(AssetName);
			}
		}
		/// <summary>
		/// 加载类型
		/// </summary>
		public System.Type type;

		public ResourceRequest Request;

		public bool IsDone
		{
			get
			{
				return (Request != null && Request.isDone);
			}
		}

		public object Asset
		{
			get
			{
				return (Request == null ? null : Request.asset);
			}
		}

		/// <summary>
		/// 加载请求
		/// </summary>
		/// <param name="assetName">资源名称</param>
		/// <param name="listener">回调</param>
		/// <param name="isKeepInMemory">是否常驻内存</param>
		/// <param name="type">资源类型</param>
		public RequestInfo(string assetName,System.Action<object> listener,bool isKeepInMemory,System.Type type)
		{
			this.AssetName = assetName;
			this.IsKeepInMemory = isKeepInMemory;
			this.type = type;

			this.Listeners = new List<System.Action<object>>(){listener};
		}

		/// <summary>
		/// 开始异步加载
		/// </summary>
		public void LoadAsync()
		{
			if(Asset == null)
			{
				Request = (type == null ? Resources.LoadAsync(AssetFullName) : Resources.LoadAsync(AssetFullName,type));
			}
		}

		public void CallBack()
		{
			if(IsDone)
			{
				for(int i = 0;i < Listeners.Count;i++)
				{
					if(Asset == null)
					{
						//Debugger.LogError("Asset load failed,assetName:" + AssetName);
                        DebugUtil.Error("Asset load failed,assetName:" + AssetName);
                    }

					Listeners[i](Asset);
				}
			}
		}

	}
}
