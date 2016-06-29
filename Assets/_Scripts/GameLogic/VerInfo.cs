using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class VerInfo
{
	#if APPSTORE
	public static string CurrentVer = "1.1";
	#elif NEWEMA
	public static string CurrentVer = "1.2";
	#else
	public static string CurrentVer = UnityEditor.PlayerSettings.bundleVersion;
	#endif
	public string SocketIp = "games.emagroup.cn";
	public int Port = 5555;
	public string Ver = "1.2";

	public VerInfo(string socketIp,int port,string ver)
	{
		SocketIp = socketIp;
		Port = port;
		Ver = ver;
	}
}

public class VerInfoList
{
	public List<VerInfo> list = new List<VerInfo>();

	public int otherData = 1000;
}
