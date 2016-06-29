using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;


public class CreateVerInfo : Editor
{
	[MenuItem("Tools/生成版本文件")]
	private static void CreateInfo()
	{
		VerInfoList listInfo = new VerInfoList();

		VerInfo info = new VerInfo("116.228.88.149",5555,"1.0");
		listInfo.list.Add(info);

		info = new VerInfo("192.168.10.28",5555,"1.1");
		listInfo.list.Add(info);

		info = new VerInfo("games.emagroup.cn",5555,"99");
		listInfo.list.Add(info);

		string jsonInfo = JsonUtility.ToJson(listInfo);

		System.IO.File.WriteAllText(Application.dataPath + "/_VerInfo/VerInfo.txt",jsonInfo);
		Debug.Log("生成配置文件成功");
		AssetDatabase.Refresh();

//		PlayerSettings.bundleVersion
	}
}
