/*******************************************************
 * 
 * 文件名(File Name)：             ResourceMgr
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/02/25 15:40:49
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MyFrameWork
{
	public class ResourceMgr : Manager
    {
		public static ResourceMgr Instance
		{
			get
			{
				return AppFacade.Instance.GetManager<ResourceMgr>(ManagerName.Resource);
			}
		}

		#region 获取资源对应的全路径
		private Dictionary<string, string> _assetPathDic = new Dictionary<string, string>();

        void Awake()
        {
            Init();
        }
        /// <summary>
        /// 获取资源的完整路径
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <returns></returns>
        public string GetFileFullName(string assetName)
		{
			string parent = "";
			if (!GetParentPathName(assetName, ref parent))
			{
				//Debugger.LogError("你要加载的文件不存在Resources当中 assetName =" + assetName);
                DebugUtil.Error("你要加载的文件不存在Resources当中 assetName =" + assetName);

            }
			return parent == "" ? assetName : parent + "/" + assetName;
		}

		/// <summary>
		/// 获取资源的父目录
		/// </summary>
		/// <param name="assetName">资源名</param>
		/// <param name="r">返回的字符串</param>
		/// <returns></returns>
		public bool GetParentPathName(string assetName, ref string r)
		{
			if (_assetPathDic.Count == 0)
			{
				UnityEngine.TextAsset tex = Resources.Load<TextAsset>("res");
				StringReader sr = new StringReader(tex.text);
				string fileName = sr.ReadLine();
				while (fileName != null)
				{
					//Debug.Log("fileName =" + fileName);
					string[] ss = fileName.Split('=');
					_assetPathDic.Add(ss[0], ss[1]);
					fileName = sr.ReadLine();
				}

				sr.Close();
				sr.Dispose();
			}

			if (_assetPathDic.ContainsKey(assetName))
			{
				r = _assetPathDic[assetName];
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

		/// <summary>
		/// Cpu个数
		/// </summary>
		private int _cpuCount;
		/// <summary>
		/// 已加载的资源
		/// </summary>
		private Dictionary<string,AssetInfo> _dicLoadedAsset;
		/// <summary>
		/// 正在加载的列表
		/// </summary>
		public List<RequestInfo> _inLoads;
		/// <summary>
		/// 等待加载的列表
		/// </summary>
		public Queue<RequestInfo> _questWaiting;

		public void Init()
		{
			_cpuCount = SystemInfo.processorCount;
			if(_cpuCount <= 0 || _cpuCount > 8)
			{
				_cpuCount = 1;
			}

			_dicLoadedAsset = new Dictionary<string, AssetInfo>();
			_inLoads = new List<RequestInfo>();
			_questWaiting = new Queue<RequestInfo>();

            //CoroutineController.Instance.StartCoroutine(Update());
        }

		#region 预加载多个资源
		public void PreLoadMultiAsset(string[] assetNames,System.Action<bool> listener,System.Type type)
		{
			int count = assetNames.Length;
			bool isLoadSuccess = true;
			for(int i = 0;i < assetNames.Length;i++)
			{
				LoadAsset(assetNames[i],(asset)=>
					{
						if(asset == null) 
						{
							isLoadSuccess = false;
						}

						count--;
						//都加载完成了再回调
						if(count == 0)
						{
							listener(isLoadSuccess);
						}
					},true,false,type);
			}
		}

		public void PreLoadMultiAsset(string[] assetNames,System.Action<bool> listener)
		{
			PreLoadMultiAsset(assetNames,listener,null);
		}
		#endregion

		#region 加载资源
		/// <summary>
		/// 加载资源
		/// </summary>
		/// <param name="assetName">资源名称</param>
		/// <param name="listener">结果回调</param>
		/// <param name="isAsync">是否异步加载</param>
		/// <param name="isKeepInMemory">是否常驻内存</param>
		/// <param name="type">资源类型，默认GameObject</param>
		public void LoadAsset(string assetName, System.Action<object> listener, bool isAsync = true, bool isKeepInMemory = false, System.Type type = null)
		{
			//是否已加载成功
			if(_dicLoadedAsset.ContainsKey(assetName))
			{
				//已加载，直接返回
				listener(_dicLoadedAsset[assetName].Asset);
			}
			else
			{

				if(type == null)
					type = typeof(GameObject);
				
				if(isAsync)
				{
					LoadAsyncAsset(assetName,listener,isKeepInMemory,type);
				}
				else
				{
					//直接加载资源
					Object asset = Resources.Load(GetFileFullName(assetName),type);
					if(asset != null)
					{
						AssetInfo assetInfo = new AssetInfo();
						assetInfo.Asset = asset;
						assetInfo.IsKeepInMemory = isKeepInMemory;

						_dicLoadedAsset.Add(assetName,assetInfo);
						AddAssetToName(assetName);
					}
					else
					{
						//Debugger.LogError("LoadAsset failed!!! assetName:" + assetName);
                        DebugUtil.Error("LoadAsset failed!!! assetName:" + assetName);
                    }

					if(listener != null)
					{
						listener(asset);
					}
				}
			}
		}

		private void LoadAsyncAsset(string assetName,System.Action<object> listener,bool isKeepInMemory,System.Type type)
		{
			RequestInfo requestInfo = null;
			for(int i = 0;i < _inLoads.Count;i++)
			{
				//是否已经在加载队列中
				if(_inLoads[i].AssetName == assetName)
				{
					requestInfo = _inLoads[i];
					break;
				}
			}

			if(requestInfo == null)
			{
				//是否在等待列表中
				foreach(var item in _questWaiting)
				{
					if(item.AssetName == assetName)
					{
						requestInfo = item;
						break;
					}
				}
			}

			if(requestInfo != null)
			{
				//已在队列中，添加监听回调
				requestInfo.AddListener(listener);
			}
			else
			{
				//放到等待队列中
				requestInfo = new RequestInfo(assetName,listener,isKeepInMemory,type);
				_questWaiting.Enqueue(requestInfo);
			}
		}
		#endregion

		#region 加载资源并实例化
		public void LoadAssetAndInstance(string assetName, System.Action<object> listener, bool isAsync = true, bool isKeepInMemory = false, System.Type type = null)
		{
			LoadAsset(assetName,
						(asset)=>
						{
							object go = null;
							if(asset != null)
							{
								go = MonoBehaviour.Instantiate(asset as Object);
							}
							
							if(listener != null)
								listener(go);
						},
						isAsync,
						isKeepInMemory,
						type);
		}
		#endregion

		/// <summary>
		/// 从预加载中直接实例化游戏物体
		/// </summary>
		/// <returns>The and instance game object from preload.</returns>
		/// <param name="assetName">Asset name.</param>
		public GameObject LoadAndInstanceGameObjectFromPreload(string assetName)
		{
			//是否已加载成功
			if(_dicLoadedAsset.ContainsKey(assetName))
			{
				var asset = _dicLoadedAsset[assetName].Asset;
				if(asset != null)
				{
					return MonoBehaviour.Instantiate(asset as GameObject);
				}
			}

			//Debugger.LogError("not preload Gameobject:" + assetName);
            DebugUtil.Error("not preload Gameobject:" + assetName);
            return null;
		}


		public void Update()
		{
			if(_inLoads.Count > 0)
			{
				//检查下载中队列
				RequestInfo requestLoaded = null;
				for(int i = _inLoads.Count - 1;i >= 0;i--)
				{
					requestLoaded = _inLoads[i];

					if(requestLoaded.IsDone)
					{
						//有下载成功的
						if(requestLoaded.Asset != null)
						{
							//添加到已下载资源字典中
							AssetInfo assetInfo = new AssetInfo();
							assetInfo.Asset = requestLoaded.Asset;
							assetInfo.IsKeepInMemory = requestLoaded.IsKeepInMemory;

							if(_dicLoadedAsset.ContainsKey(requestLoaded.AssetName))
							{
								_dicLoadedAsset[requestLoaded.AssetName] = assetInfo;
							}
							else
							{
								_dicLoadedAsset.Add(requestLoaded.AssetName,assetInfo);
							}

							AddAssetToName(requestLoaded.AssetName);
						}

						//移出队列
						_inLoads.Remove(requestLoaded);

						//通知
						requestLoaded.CallBack();
					}
				}
			}

			//从等待队列到下载队列
			while(_questWaiting.Count > 0 && _inLoads.Count < _cpuCount)
			{
				RequestInfo requestInfo = _questWaiting.Dequeue();
				_inLoads.Add(requestInfo);

				//开启下载
				requestInfo.LoadAsync();
			}
		}

		#region 资源处理
		/// <summary>
		/// 从字典中取得一个资源.
		/// </summary>
		/// <returns>The asset.</returns>
		/// <param name="assetName">Asset name.</param>
		public AssetInfo GetAsset(string assetName)
		{
			AssetInfo info = null;
			_dicLoadedAsset.TryGetValue(assetName,out info);

			return info;
		}

		/// <summary>
		/// 释放一个资源.
		/// </summary>
		/// <param name="assetName">Asset name.</param>
		public void ReleaseAsset(string assetName)
		{
			AssetInfo info = null;
			_dicLoadedAsset.TryGetValue(assetName,out info);
			if(info != null && !info.IsKeepInMemory)
			{
				_dicLoadedAsset.Remove(assetName);
			}

		}

		/// <summary>
		/// 修改是否常驻内存
		/// </summary>
		/// <returns><c>true</c> if this instance is keep in memory the specified assetName isKeepInMemory; otherwise, <c>false</c>.</returns>
		/// <param name="assetName">Asset name.</param>
		/// <param name="isKeepInMemory">If set to <c>true</c> is keep in memory.</param>
		public void IsKeepInMemory(string assetName,bool isKeepInMemory)
		{
			AssetInfo info = null;
			_dicLoadedAsset.TryGetValue(assetName,out info);
			if(info != null)
			{
				info.IsKeepInMemory = isKeepInMemory;
			}
		}
		#endregion

		#region 资源释放以及监听

		/// <summary>
		/// 资源堆.
		/// </summary>
		public Stack<List<string>> _stackAsset = new Stack<List<string>>();

		public void AddAssetToName(string assetName)
		{
			List<string> list = null;
			if(_stackAsset.Count == 0)
			{
				list = new List<string>();
				_stackAsset.Push(list);
			}

			list = _stackAsset.Peek();
			list.Add(assetName);
		}

		public void PushAssetStack()
		{
			List<string> list = new List<string>();

			List<string> keys = new List<string>(_dicLoadedAsset.Keys);
			for(int i = 0;i < keys.Count;i++)
			{
				_dicLoadedAsset[keys[i]].StatckCount++;
				list.Add(keys[i]);
			}

			_stackAsset.Push(list);
		}

		public void PopAssetStack()
		{
			if(_stackAsset.Count == 0) return;


			List<string> listToRemove = new List<string>();

			List<string> list = _stackAsset.Pop();
			AssetInfo info = null;
			for(int i = 0;i < list.Count;i++)
			{
				if(_dicLoadedAsset.TryGetValue(list[i],out info))
				{
					info.StatckCount--;
					if(info.StatckCount < 1 && !info.IsKeepInMemory)
					{
						listToRemove.Add(list[i]);
					}
				}
			}

			bool needGC = false;
			for(int i = 0;i < listToRemove.Count;i++)
			{
				if(_dicLoadedAsset.ContainsKey(listToRemove[i]))
				{
					_dicLoadedAsset.Remove(listToRemove[i]);
					needGC = true;
				}
			}

			if(needGC)
			{
				GC();
			}
		}

		public void GC()
		{
			Resources.UnloadUnusedAssets();
			System.GC.Collect();
		}
		#endregion
	}
}
