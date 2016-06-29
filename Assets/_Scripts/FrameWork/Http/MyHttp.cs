using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;
using BestHTTP;

public class MyHttp : Singleton<MyHttp>
{
	private Dictionary<string,VerInfo> _dicVerInfo = new Dictionary<string, VerInfo>();
	public VerInfo GetVerInfo(string ver)
	{
		VerInfo info = null;
		_dicVerInfo.TryGetValue(ver,out info);

		return info;
	}

	#if NEWEMA
	string url = "http://cdn.emagroup.cn/mymatch/android/VerInfo.txt";
	#elif APPSTORE
	string url = "http://cdn.emagroup.cn/mymatch/android/VerInfo.txt";
	#else
	string url = "file://" + Application.dataPath + "/_VerInfo/Verinfo.txt";
	#endif

	private System.Action<bool> OnCallBack;
	// Use this for initialization
	public void RequestVerInfo(System.Action<bool> callBack) 
	{
		OnCallBack = callBack;
//		#if !NEWEMA
		HTTPRequest request = new HTTPRequest(new System.Uri(url),OnFinishRequest);
		request.Send();
//		#endif
	}
	
	void OnFinishRequest(HTTPRequest request, HTTPResponse response)
	{
		if(response != null)
		{
			string info = response.DataAsText;
			if(!string.IsNullOrEmpty(info))
			{
				//string info = System.IO.File.ReadAllText(Application.dataPath + "/_VerInfo/VerInfo.txt",System.Text.Encoding.UTF8);
				VerInfoList verInfo = JsonUtility.FromJson<VerInfoList>(response.DataAsText);
				foreach(var item in verInfo.list)
				{
					VerInfo infoNew = new VerInfo(item.SocketIp,item.Port,item.Ver);
					if(_dicVerInfo.ContainsKey(infoNew.Ver))
					{
						_dicVerInfo[infoNew.Ver] = infoNew;
					}
					else
					{
						_dicVerInfo.Add(infoNew.Ver,infoNew);
					}
				}

				if(OnCallBack != null)
					OnCallBack(true);

				return;
			}
		}


		Debug.LogError("网络连接出错，配置信息无法获取");

		if(OnCallBack != null)
			OnCallBack(false);

//		System.IO.StringReader sr = new System.IO.StringReader(response.DataAsText);
//		if(sr != null)
//		{
//			string lineTxt = sr.ReadLine();
//			while(!string.IsNullOrEmpty(lineTxt))
//			{
//				Debug.Log("line:" + lineTxt);
//
//				lineTxt = sr.ReadLine();
//			}
//
//			sr.Close();
//		}
//
//		string info = System.IO.File.ReadAllText(Application.dataPath + "/_VerInfo/VerInfo.txt",System.Text.Encoding.UTF8);
//		VerInfoList verInfo = JsonUtility.FromJson<VerInfoList>(info);
//		foreach(var item in verInfo.list)
//		{
//			Debug.Log(item.SocketIp + "," + item.Ver);
//		}
	}
}
