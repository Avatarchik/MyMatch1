//**************************
// 参见游客网，课程框架
// http://www.youkexueyuan.com/course_show/121.html
//**************************
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using MyFrameWork;

public class CreateResIniFile : MonoBehaviour 
{

    static BuildAssetBundleOptions s_option = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets;

#if UNITY_STANDALONE_WIN && UNITY_EDITOR
    static BuildTarget s_Target = BuildTarget.StandaloneWindows;
    static string s_OutputDir = Application.dataPath + s_AssetPrefix + "/StreamingAssets/WinData/LatestData/";
#elif UNITY_ANDROID
    static BuildTarget s_Target = BuildTarget.Android;
    static string s_OutputDir = Application.dataPath + s_AssetPrefix + "/StreamingAssets/AndroidData/LatestData/";
#else
	static BuildTarget s_Target = BuildTarget.iOS;
    static string s_OutputDir = Application.dataPath + s_AssetPrefix + "/StreamingAssets/IOSData/LatestData/";
#endif

    static string s_AssetPrefix = "Assets";
    static string s_AltasDir = "/Atlas/";
    static string s_TextureDir = "/Artist/NGUI/Texture/";
    static string s_FontDir = "/Font/";
    static string s_LayoutDir = "/Resources/Test/UI/";
    static string s_TemplateDir = "/Template/";
    static string s_TempDir = "/Temp/";


    #region Layout制作
    // <summary>
    // 单独制作选中Layout的assetbundle
    // 需要确保选中的prefab在"/Resources/Test/UI/"下
    // </summary>
    [MenuItem("BuildBundle/UI/BuildPrefabSelect")]
    public static void BuildLayoutSelect()
    {
//         GameObject[] gos = Selection.gameObjects;
//         foreach (GameObject go in gos)
//         {
//             string name = go.name;
//             ModifyPrefab(s_LayoutDir, name);
//             ComposeBundle(name);
//         }
    }

    /// <summary>
    /// 全部制作"/Assets/Artist/NGUI/Layout"下的Layout
    /// </summary>
    [MenuItem("BuildBundle/UI/BuildLayoutAll")]
    public static void BuildLayoutAll()
    {
        int substart = Application.dataPath.Length;
        string fileDir = Application.dataPath + s_LayoutDir;
        DirectoryInfo fileDirInfo = new DirectoryInfo(fileDir);
        FileInfo[] fileInfos = fileDirInfo.GetFiles("*.prefab");
        //创建文件夹
        if (!Directory.Exists(s_OutputDir))
            Directory.CreateDirectory(s_OutputDir);

        foreach (FileInfo fileInfo in fileInfos)
        {
            string assetDir = fileInfo.FullName;
            assetDir = assetDir.Substring(substart, assetDir.Length - substart);
            Object fileObj = AssetDatabase.LoadAssetAtPath(s_AssetPrefix + assetDir, typeof(Object)) as Object;
            string fileInfoName = fileInfo.Name.Substring(0, fileInfo.Name.Length - 7);
            BuildPipeline.BuildAssetBundle(fileObj, null, s_OutputDir + fileInfoName + ".unity3d", s_option, s_Target);
        }
    }
    #endregion

    /// <summary>
    /// 检查指定路径下选中的Prefab是否正确
    /// 1）图集，2）字体
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool CheckPrefab()
    {
        bool ret = true;

        GameObject[] gos = Selection.gameObjects;
        foreach (GameObject go in gos)
        {
            GameObject newObj = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath(s_AssetPrefix + s_LayoutDir + go.name + ".prefab", typeof(UnityEngine.Object))) as GameObject;
            newObj.name = go.name;

            List<string> objnameList = new List<string>();

            Transform[] trans = newObj.GetComponentsInChildren<Transform>(true);
            foreach (Transform tran in trans)
            {
                string objname = Util.GetFullName(tran.gameObject);
                if (objnameList.Contains(objname))
                {
                    ret = false;
                    Debug.LogError("Objname : [" + objname + "] name duplicate!!!!");
                }
                else
                {
                    objnameList.Add(objname);
                }
            }


            GameObject.DestroyImmediate(newObj);
        }

        return ret;
    }

    //Prefab检查
    [MenuItem("PrefabCheck/CheckSelectPrefab")]
    public static void CheckSelectLayoutPrefab()
    {
        if (CheckPrefab())
        {
            Debug.Log("Select Prefab OK!!!");
        }
    }

    //获取物体全名
    [MenuItem("PrefabCheck/OutputObjectName")]
    public static void OutputObjectName()
    {
        Debug.Log("OutputObjectName");
        Debug.Log(Util.GetFullName(Selection.activeGameObject));
    }

    [MenuItem("Tools/CreateResIniFile")]
	public static void Createini()
	{

		Dictionary<string, string> dic = new Dictionary<string, string>();
		string pathRes = Application.dataPath +"/Resources/";
		string pathIni = pathRes + "/res.txt";
		if (File.Exists(pathIni))
		{
			File.Delete(pathIni);
		}

		CreateResInfo(pathRes, ref dic);
		List<string> list = new List<string>();
		foreach(KeyValuePair<string,string> keyValue in dic)
		{
			list.Add(keyValue.Key +"="+keyValue.Value);
		}
		File.WriteAllLines(pathRes +"/res.txt",list.ToArray());
		DebugUtil.Debug("生成完毕 ");
		AssetDatabase.Refresh();
	}

	public static void CreateResInfo(string path,ref Dictionary<string,string>dic)
	{
		DirectoryInfo dir = new DirectoryInfo(path);
		if (!dir.Exists)
		{
			return;
		}
		FileInfo[] files = dir.GetFiles();
		for (int i = 0; i < files.Length;i++ )
		{

			FileInfo info = files[i];

			if(info.Name.Equals(".DS_Store") || info.Name.EndsWith(".txt"))
				continue;
			
			if (!(info.Name.IndexOf(".meta",0) > 0))
			{

				string pathdir = info.FullName.Replace("\\","/")
					.Replace((Application.dataPath + "/Resources/"), "")
					.Replace(info.Name, "").TrimEnd('/');
				string fileName = Path.GetFileNameWithoutExtension(info.Name);
				Debug.Log("fileName =" + fileName + " info.Name = " + info.Name);
				if (!dic.ContainsKey(fileName))
				{
					dic.Add(fileName, pathdir);
				}
				else
				{
					Debugger.LogError("存在相同的资源名称 名称为：" + info.Name + "/path1=" + dic[fileName] + "/ path2 =" + pathdir);
                    //DebugUtil.Error("存在相同的资源名称 名称为：" + info.Name + "/path1=" + dic[fileName] + "/ path2 =" + pathdir);
                }
            }
		}
		DirectoryInfo[] dirs = dir.GetDirectories();
		if (dirs.Length > 0)
		{
			for (int i = 0; i < dirs.Length;i++ )
			{
				string tempPath = Path.Combine(path, dirs[i].Name);
				CreateResInfo(tempPath, ref dic);
			}
		}
	}


	[MenuItem("GameObject/Create Other/获取选中路径")]
	public static void GetGoPathInHierarchy()
	{
		if(Selection.activeGameObject == null) return;

		string strPath = Selection.activeGameObject.name;
		Transform trans = Selection.activeGameObject.transform;
		while(trans.parent != null)
		{
			strPath = trans.parent.name + "/" + strPath;

			trans = trans.parent;
		}
		Debug.Log("name:" + Selection.activeGameObject + ",path:" + strPath);
	}

}
