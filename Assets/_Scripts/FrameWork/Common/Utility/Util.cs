using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Reflection;
using LuaInterface;
using MyFrameWork;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MyFrameWork
{
    public class Util {

        /// <summary>
        /// make sure path Has been created.
        /// </summary>
        /// <param name="localPath">path</param>
        /// <returns>path</returns>
        static public string CreateDirectory(string localPath)
        {
            string path = localPath;
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        public static int Int(object o) {
            return Convert.ToInt32(o);
        }

        public static float Float(object o) {
            return (float)Math.Round(Convert.ToSingle(o), 2);
        }

        public static long Long(object o) {
            return Convert.ToInt64(o);
        }

        public static int Random(int min, int max) {
            return UnityEngine.Random.Range(min, max);
        }

        public static float Random(float min, float max) {
            return UnityEngine.Random.Range(min, max);
        }

        public static string Uid(string uid) {
            int position = uid.LastIndexOf('_');
            return uid.Remove(0, position + 1);
        }

        public static long GetTime() {
            TimeSpan ts = new TimeSpan(DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
            return (long)ts.TotalMilliseconds;
        }

        /// <summary>
        /// 搜索子物体组件-GameObject版
        /// </summary>
        public static T Get<T>(GameObject go, string subnode) where T : Component {
            if (go != null) {
                Transform sub = go.transform.FindChild(subnode);
                if (sub != null) return sub.GetComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// 搜索子物体组件-Transform版
        /// </summary>
        public static T Get<T>(Transform go, string subnode) where T : Component {
            if (go != null) {
                Transform sub = go.FindChild(subnode);
                if (sub != null) return sub.GetComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// 搜索子物体组件-Component版
        /// </summary>
        public static T Get<T>(Component go, string subnode) where T : Component {
            return go.transform.FindChild(subnode).GetComponent<T>();
        }

        /// <summary>
        /// 添加组件
        /// </summary>
        public static T Add<T>(GameObject go) where T : Component {
            if (go != null) {
                T[] ts = go.GetComponents<T>();
                for (int i = 0; i < ts.Length; i++) {
                    if (ts[i] != null) GameObject.Destroy(ts[i]);
                }
                return go.gameObject.AddComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// 添加组件
        /// </summary>
        public static T Add<T>(Transform go) where T : Component {
            return Add<T>(go.gameObject);
        }

        /// <summary>
        /// 查找子对象
        /// </summary>
        public static GameObject Child(GameObject go, string subnode) {
            return Child(go.transform, subnode);
        }

        /// <summary>
        /// 查找子对象
        /// </summary>
        public static GameObject Child(Transform go, string subnode) {
            Transform tran = go.FindChild(subnode);
            if (tran == null) return null;
            return tran.gameObject;
        }

        /// <summary>
        /// 取平级对象
        /// </summary>
        public static GameObject Peer(GameObject go, string subnode) {
            return Peer(go.transform, subnode);
        }

        /// <summary>
        /// 取平级对象
        /// </summary>
        public static GameObject Peer(Transform go, string subnode) {
            Transform tran = go.parent.FindChild(subnode);
            if (tran == null) return null;
            return tran.gameObject;
        }

        /// <summary>
        /// 计算字符串的MD5值
        /// </summary>
        public static string md5(string source) {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(source);
            byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
            md5.Clear();

            string destString = "";
            for (int i = 0; i < md5Data.Length; i++) {
                destString += System.Convert.ToString(md5Data[i], 16).PadLeft(2, '0');
            }
            destString = destString.PadLeft(32, '0');
            return destString;
        }

        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        public static string md5file(string file) {
            try {
                FileStream fs = new FileStream(file, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(fs);
                fs.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++) {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            } catch (Exception ex) {
                throw new Exception("md5file() fail, error:" + ex.Message);
            }
        }

        /// <summary>
        /// 清除所有子节点
        /// </summary>
        public static void ClearChild(Transform go) {
            if (go == null) return;
            for (int i = go.childCount - 1; i >= 0; i--) {
                GameObject.Destroy(go.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// 清理内存
        /// </summary>
        public static void ClearMemory() {
            GC.Collect(); Resources.UnloadUnusedAssets();
            LuaManager mgr = AppFacade.Instance.GetManager<LuaManager>(ManagerName.Lua);
            if (mgr != null) mgr.LuaGC();
        }
        /// <summary>
        /// 根据平台获取热更URL
        /// </summary>
        /// 
        public static string WebURL
        {
            get
            {
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    return AppConst.WebUrl_IOS;
                }
                else if(Application.platform == RuntimePlatform.Android)
                {
                    return AppConst.WebUrl_Android;
                }
                else
                {
                    return Application.streamingAssetsPath + "/";
                }
            }
        }
        /// <summary>
        /// 取得数据存放目录
        /// </summary>
        public static string DataPath {
            get {
                string game = AppConst.AppName.ToLower();
				if (Application.platform == RuntimePlatform.IPhonePlayer) 
				{
                    Debug.Log("Util.DataPath:Application.isMobilePlatform:" + Application.persistentDataPath + "/" + game + "/");
                    return Application.persistentDataPath + "/" + game + "/";
                }
                if (Application.isMobilePlatform)
                {
                    Debug.Log("Util.DataPath:Application.isMobilePlatform:" + Application.persistentDataPath + "/" + game + "/");
                    return Application.persistentDataPath + "/" + game + "/";
                }
                if (Application.platform == RuntimePlatform.WindowsPlayer) {
                    return Application.streamingAssetsPath + "/";
                }
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    return Application.streamingAssetsPath + "/";
                }
                if (AppConst.DebugMode && Application.isEditor) {
                    return Application.streamingAssetsPath + "/";
                }
                if (Application.platform == RuntimePlatform.OSXEditor)
                {
                    return Application.streamingAssetsPath + "/";
                }
                return "c:/" + game + "/";
            }
        }

        /// <summary>
        /// 应用程序内容路径
        /// </summary>
        public static string AppContentPath() {
            string path = string.Empty;
            switch (Application.platform) {
                case RuntimePlatform.Android:
                    path = "jar:file://" + Application.dataPath + "!/assets/";
                break;
                case RuntimePlatform.IPhonePlayer:
                    path = Application.dataPath + "/Raw/";
                break;
                default:
                    path = Application.dataPath + "/StreamingAssets/";
                break;
            }
            return path;
        }

        /// <summary>
        /// 添加lua单机事件
        /// </summary>
        public static void AddClick(GameObject go, System.Object luafuc) {
            UIEventListener.Get(go).onClick += delegate(GameObject o) {
                LuaInterface.LuaFunction func = (LuaInterface.LuaFunction)luafuc;
                func.Call();
            };
        }

        public static void Log(string str) {
            DebugUtil.Info(str);
        }

        public static void LogWarning(string str) {
            DebugUtil.Warning(str);
        }

        public static void LogError(string str) {
            DebugUtil.Error(str);
        }

        public static GameObject LoadAsset(AssetBundle bundle, string name) {
#if UNITY_5
        return bundle.LoadAsset(name, typeof(GameObject)) as GameObject;
#else
            return bundle.Load(name, typeof(GameObject)) as GameObject;
#endif
        }

        public static Component AddComponent(GameObject go,string nameSpace,string classname) {
            Assembly asmb = Assembly.GetExecutingAssembly();
            Type t = null;
            if (nameSpace.Length > 2)
                t = asmb.GetType(nameSpace + "." + classname);
            else
                t = asmb.GetType(classname);
            return go.AddComponent(t);
        }

        /// <summary>
        /// 载入Prefab
        /// </summary>
        /// <param name="name"></param>
        public static GameObject LoadPrefab(string name) {
            return Resources.Load(name, typeof(GameObject)) as GameObject;
        }

        /// <summary>
        /// 执行Lua方法
        /// </summary>
        public static object[] CallMethod(string module, string func, params object[] args) {
            LuaManager luaMgr = AppFacade.Instance.GetManager<LuaManager>(ManagerName.Lua);
            if (luaMgr == null) return null;
            return luaMgr.CallFunction(module + "." + func, args);
        }

        /// <summary>
        /// 防止初学者不按步骤来操作
        /// </summary>
        /// <returns></returns>
        static int CheckRuntimeFile() {
            if (!Application.isEditor) return 0;
            string streamDir = Application.dataPath + "/StreamingAssets/";
            if (!Directory.Exists(streamDir)) {
                return -1;
            } else {
                string[] files = Directory.GetFiles(streamDir);
                if (files.Length == 0) return -1;

                if (!File.Exists(streamDir + "files.txt")) {
                    return -1;
                }
            }
            string sourceDir = AppConst.FrameworkRoot + "/ToLua/Source/Generate/";
            if (!Directory.Exists(sourceDir)) {
                return -2;
            } else {
                string[] files = Directory.GetFiles(sourceDir);
                if (files.Length == 0) return -2;
            }
            return 0;
        }

        /// <summary>
        /// 检查运行环境
        /// </summary>
        public static bool CheckEnvironment() {
#if UNITY_EDITOR
            int resultId = Util.CheckRuntimeFile();
            if (resultId == -1) {
                Debug.LogError("没有找到框架所需要的资源，单击Game菜单下Build xxx Resource生成！！");
                EditorApplication.isPlaying = false;
                return false;
            } else if (resultId == -2) {
                Debug.LogError("没有找到Wrap脚本缓存，单击Lua菜单下Gen Lua Wrap Files生成脚本！！");
                EditorApplication.isPlaying = false;
                return false;
            }
            if (Application.loadedLevelName == "Test" && !AppConst.DebugMode) {
                Debug.LogError("测试场景，必须打开调试模式，AppConst.DebugMode = true！！");
                EditorApplication.isPlaying = false;
                return false;
            }
#endif            
            return true;
        }
        /// <summary>
        /// 获取一个GameObj的FullName
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetFullName(GameObject obj)
        {
            if (obj.transform.parent != null)
            {
                return GetFullName(obj.transform.parent.gameObject) + "/" + obj.name;
            }
            else
            {
                return obj.name;
            }
        }
    }
}