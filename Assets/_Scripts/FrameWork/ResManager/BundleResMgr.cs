using UnityEngine;
using System.Collections;
using MyFrameWork;
using System.IO;

//消息处理函数原型
public delegate void MessageFunction(System.Object obj,Command cmd);

public class WWWRequestHandle
{
    public string url;
    public Command cmd;
    public MessageFunction completeCallback;
    public MessageFunction errorCallback;
    public bool IsDone;
    public int Id = 0;
    public System.Diagnostics.Stopwatch m_LoadResTimeCost = new System.Diagnostics.Stopwatch();

    public void Abort()
    {
        BundleResMgr.Instance.Abort(this);
    }

    public byte[] RetainAsBytes()
    {
        return BundleResMgr.Instance.RetainAsBytes(url);
    }
    public AssetBundle RetainAsAssetBundle()
    {
        return BundleResMgr.Instance.RetainAsAssetBundle(url);
    }
    //public AudioClip RetainAsAudioClip(bool threeD, bool streaming);
    public string RetainAsText()
    {
        return BundleResMgr.Instance.RetainAsText(url);
    }
    public float Progress
    {
        get { return BundleResMgr.Instance.QueryProgress(this); }
    }

    public void Release()
    {
        BundleResMgr.Instance.Release(url);
    }

}

public class BundleResMgr : Singleton<BundleResMgr>
{

    public WWWRequestHandle RequestWWW(string url,Command cmd, MessageFunction completeCallback, MessageFunction errorCallback)
    {
        if (url == null)
        {
            DebugUtil.Error("ResourceManager::RequestWWW() url is null");
            return null;
        }
        //DebugOutPut.Log("ResourceManager WWW at url: " + url, DebugLogLevel.LogLevel1);
        WWWRequestHandle ret = new WWWRequestHandle();
        ret.url = url;
        ret.cmd = cmd;
        ret.completeCallback = completeCallback;
        ret.errorCallback = errorCallback;
        fetchingQueue.Add(ret);
        ret.m_LoadResTimeCost.Start();
        return ret;
    }
    public WWWRequestHandle RequestWWW(string url, Command cmd, MessageFunction completeCallback, MessageFunction errorCallback, int Id)
    {
        if (url == null)
        {
            DebugUtil.Error("ResourceManager::RequestWWW() url is null");
            return null;
        }
        WWWRequestHandle ret = new WWWRequestHandle();
        ret.url = url;
        ret.cmd = cmd;
        ret.Id = Id;
        ret.completeCallback = completeCallback;
        ret.errorCallback = errorCallback;
        fetchingQueue.Add(ret);
        ret.m_LoadResTimeCost.Start();
        return ret;
    }

    public byte[] RetainAsBytes(string url)
    {
        return RetainInternal(url, EType.bytes, false, false).bytes;
    }
    public AssetBundle RetainAsAssetBundle(string url)
    {
        return RetainInternal(url, EType.assetbundle, false, false).assetBundle;
    }
    public AudioClip RetainAsAudioClip(string url, bool threeD, bool streaming)
    {
        return RetainInternal(url, EType.audioclip, threeD, streaming).audioClip;
    }
    public string RetainAsText(string url)
    {
        return RetainInternal(url, EType.text, false, false).text;
    }

    public void Release(string url)
    {
        FetchedResourceRef res = FindFetchedRes(url);
        if (res == null)
            return;
        int refcount = res.Release();
        if (refcount <= 0)
        {
            fetchedResourceMap.Remove(url);
            //LateUnloadUnusedAssets();
        }
        return;
    }

    /*
    public void LateUnloadUnusedAssets()
    {
        LastScript.NeedUnloadUnusedAssets = true;
    }*/

    public bool HasResource(string url)
    {
        FetchedResourceRef fetchedres = FindFetchedRes(url);
        WWW fetchedWWW = FindFetchedWWW(url);
        if (fetchedres == null && fetchedWWW == null)
            return false;
        else
            return true;
    }

    public void Abort(WWWRequestHandle handle)
    {
        if (handle == null)
            return;
        if (workingRequest == handle)
        {
            workingWWW.Dispose();
            workingWWW = null;
            workingRequest = null;
            fetchingQueue.RemoveAt(0);
        }
        else
        {
            fetchingQueue.Remove(handle);
        }
    }

    public float QueryProgress(WWWRequestHandle handle)
    {
        if (handle == null)
            return 0;
        if (workingRequest == handle)
            return workingWWW.progress;
        else if (fetchingQueue.Contains(handle))
            return 0;
        else
            return 1;
    }

    /////////////////////////////////////////////////////////////// 
    //private 	

    public enum EType
    {
        bytes = 0,
        assetbundle = 1,
        audioclip = 2,
        text = 3,
    }
    public class FetchedResourceRef
    {
        public EType type = EType.bytes;
        public byte[] bytes = null;
        public AssetBundle assetBundle = null;
        public AudioClip audioClip = null;
        public string text = null;
        private int refCount = 0;

        public object GetObj()
        {
            switch (type)
            {
                case EType.assetbundle:
                    return assetBundle;
                case EType.audioclip:
                    return audioClip;
                case EType.text:
                    return text;
                case EType.bytes:
                default:
                    return bytes;
            }
        }
        public void SetObj(WWW www, bool threeD, bool streaming)
        {
            switch (type)
            {
                case EType.assetbundle:
                    assetBundle = www.assetBundle;
                    break;
                case EType.audioclip:
                    audioClip = www.GetAudioClip(threeD, streaming);
                    break;
                case EType.text:
                    text = www.text;
                    break;
                case EType.bytes:
                default:
                    bytes = www.bytes;
                    break;
            }
        }

        public int AddRef()
        {
            ++refCount;
            return refCount;
        }
        public int Release()
        {
            --refCount;
            if (refCount <= 0)
            {
                switch (type)
                {
                    case EType.assetbundle:
                        if (assetBundle != null) //protection for editor mode
                            assetBundle.Unload(true);
                        assetBundle = null;
                        break;
                    case EType.audioclip:
                        audioClip = null;
                        break;
                    case EType.text:
                        text = null;
                        break;
                    case EType.bytes:
                    default:
                        bytes = null;
                        break;
                }
            }
            return refCount;
        }

        //debug function
        public int DebugGetRefCount()
        {
            return refCount;
        }

    };
#if MemoryTest
	public System.Collections.Generic.Dictionary<string, FetchedResourceRef> fetchedResourceMap = new System.Collections.Generic.Dictionary<string, FetchedResourceRef>();
	public System.Collections.Generic.Dictionary<string, WWW> fetchedWWWMap = new System.Collections.Generic.Dictionary<string, WWW>();
	public System.Collections.Generic.List<WWWRequestHandle> fetchingQueue = new System.Collections.Generic.List<WWWRequestHandle>();
#else
    System.Collections.Generic.Dictionary<string, FetchedResourceRef> fetchedResourceMap = new System.Collections.Generic.Dictionary<string, FetchedResourceRef>();
    System.Collections.Generic.Dictionary<string, WWW> fetchedWWWMap = new System.Collections.Generic.Dictionary<string, WWW>();
    System.Collections.Generic.List<WWWRequestHandle> fetchingQueue = new System.Collections.Generic.List<WWWRequestHandle>();
#endif

    private FetchedResourceRef RetainInternal(string url, EType type, bool threeD, bool streaming)
    {
        FetchedResourceRef fetchedres = FindFetchedRes(url);
        WWW fetchedWWW = FindFetchedWWW(url);
        if (fetchedres == null && fetchedWWW == null)
        {
            DebugUtil.Error("attempt to retain an unfetched resource, url: " + url);
            return null;
        }
        else if (fetchedres != null)
        {
            fetchedres.AddRef();
            return fetchedres;
        }
        else if (fetchedWWW != null)
        {
            FetchedResourceRef res = new FetchedResourceRef();
            res.type = type;
            res.SetObj(fetchedWWW, threeD, streaming);
            res.AddRef();
            fetchedResourceMap[url] = res;

            fetchedWWW.Dispose();
            fetchedWWWMap.Remove(url);

            return res;
        }
        else //should never be here
        {
            throw new System.Exception("entering impossible code path");
        }

    }




    WWWRequestHandle workingRequest = null;
    WWW workingWWW = null;
    public void Update()
    {
        if (workingRequest == null) //not in downloading process
        {
            if (fetchingQueue.Count > 0)
            {
                //use tmp handle to clean status in resourcemanager first
                WWWRequestHandle tmp = fetchingQueue[0];
                if (HasResource(tmp.url))
                {
                    fetchingQueue.RemoveAt(0);
                    tmp.IsDone = true;
                    if (tmp.completeCallback != null)
                        tmp.completeCallback(tmp,tmp.cmd);
                    tmp.m_LoadResTimeCost.Stop();
                    System.TimeSpan cost = tmp.m_LoadResTimeCost.Elapsed;
                    string timecostdetial = string.Format("{0} Loaded,time cost: {1}", tmp.url, cost.TotalSeconds);
                    DebugUtil.Info(timecostdetial);
                    return;
                }
                workingRequest = tmp;
                workingWWW = new WWW(workingRequest.url);
                //DebugOutPut.Log("ResourceManager new WWW at url: " + workingRequest.url, DebugLogLevel.LogLevel1);
            }
        }
        else //currently downloading
        {
            if (workingWWW.error != null)
            {
                if (workingWWW.error.Contains("WWW request was cancelled")) //not an error
                {
                    DebugUtil.Error("ResourceCanceled: " + workingWWW.url);
                    workingWWW = null;
                    workingRequest = null;
                    fetchingQueue.RemoveAt(0);
                }
                else
                {
                    DebugUtil.Error("WWW download has error: " + workingWWW.error + " url: " + workingWWW.url);
                    //use tmp handle to clean status in resourcemanager first
                    WWWRequestHandle tmp = workingRequest;
                    workingWWW = null;
                    workingRequest = null;
                    fetchingQueue.RemoveAt(0);
                    if (tmp.errorCallback != null)
                        tmp.errorCallback(tmp,tmp.cmd);
                    //throw new System.Exception(workingWWW.error + " : " + workingWWW.url);
                    tmp.m_LoadResTimeCost.Stop();
                    System.TimeSpan cost = tmp.m_LoadResTimeCost.Elapsed;
                    string timecostdetial = string.Format("{0} Loaded,time cost: {1}", tmp.url, cost.TotalSeconds);
                    DebugUtil.Info(timecostdetial);
                }
            }
            else
            {
                if (workingWWW.isDone)
                {
                    //DebugOutPut.Log("WWW download complete: " + workingWWW.url, DebugLogLevel.LogLevel1);
                    workingRequest.IsDone = true;
                    fetchedWWWMap[workingRequest.url] = workingWWW;
                    workingWWW = null;
                    //use tmp handle to clean status in resourcemanager first
                    WWWRequestHandle tmp = workingRequest;
                    workingRequest = null;
                    fetchingQueue.RemoveAt(0);
                    if (tmp.completeCallback != null)
                        tmp.completeCallback(tmp,tmp.cmd);
                    tmp.m_LoadResTimeCost.Stop();
                    System.TimeSpan cost = tmp.m_LoadResTimeCost.Elapsed;
                    string timecostdetial = string.Format("{0} Loaded,time cost: {1}", tmp.url, cost.TotalSeconds);
                    DebugUtil.Info(timecostdetial);
                }
            }
        }
    }




    private FetchedResourceRef FindFetchedRes(string url)
    {
        if (url == null)
            return null;
        return fetchedResourceMap.ContainsKey(url) ? fetchedResourceMap[url] : null;
    }

    private WWW FindFetchedWWW(string url)
    {
        if (url == null)
            return null;
        return fetchedWWWMap.ContainsKey(url) ? fetchedWWWMap[url] : null;
    }



    /// debug function

    public void DebugPrintResourceInfo()
    {
        string output = "";
        output += "---------------\n";
        output += "fetchedResource\n";
        output += "refcount, url\n";
        foreach (System.Collections.Generic.KeyValuePair<string, FetchedResourceRef> kv in fetchedResourceMap)
        {
            output += (kv.Value.DebugGetRefCount().ToString() + "       , " + kv.Key + "\n");
        }
        output += "fetchedWWW\n";
        output += "url\n";
        foreach (System.Collections.Generic.KeyValuePair<string, WWW> kv in fetchedWWWMap)
        {
            output += (kv.Key + "\n");
        }
        output += "fetchingQueue\n";
        output += "url\n";
        foreach (WWWRequestHandle w in fetchingQueue)
        {
            output += (w.url + "\n");
        }
        output += "---------------";
        DebugUtil.Error(output);
    }


    /// <summary>
    ////////////////static 
    /// </summary>
    //wrapper of Resources.Load
    //typeinfo: used for future bookkeeping
    public static UnityEngine.Object ResourcesLoad(string res, string typeinfo)
    {
        return Resources.Load(res);
    }

    /// <summary>
    /// 获取一个2进制数据
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static byte[] GetBytes(string fileName)
    {
#if UNITY_WEBPLAYER          
            //return GetWWWResource(path).bytes;            
            DebugOutPut.Log("filename=" + fileName, DebugLogLevel.LogLevel1);
            TextAsset resource = Resources.Load(fileName) as TextAsset;            
            return resource.bytes;            
#else
        string path = GetResourceURL(fileName, "bytes", true);
        if (path != null)
        {
            //strip掉开始的file://
            string stripedpath = path.Substring(7);
            return File.ReadAllBytes(stripedpath);

        }

        DebugUtil.Error("Load " + fileName + " failed");
        return null;
#endif
    }

    /// <summary>
    /// 获取一个2进制数据的filestream，要记得自己关掉哦1！！！！！！！！！！！！！！！！
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static FileStream GetFileStream(string fileName)
    {
        //FileStream stream;
        string path = GetResourceURL(fileName, "bytes", true);

        if (path != null)
        {
            //strip掉开始的file://
            string stripedpath = path.Substring(7);
            return new FileStream(stripedpath, FileMode.Open, FileAccess.Read);
        }

        DebugUtil.Error("Load " + fileName + " failed");

        return null;
    }

    public static FileStream GetFileStream(string fileName, string extname)
    {
        //FileStream stream;
        string path = GetResourceURL(fileName, extname, true);
        Debug.Log("Static Data Path: " + path);
        if (path != null)
        {
            //strip掉开始的file://
            string stripedpath = path.Substring(7);
            return new FileStream(stripedpath, FileMode.Open, FileAccess.Read);
        }

        DebugUtil.Error("Load " + fileName + " failed");
        return null;
    }

    /// <summary>
    /// 获取一个txt数据
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetTxt(string fileName)
    {
        return GetTxt(fileName, true);
    }

    /// <summary>
    /// 获取一个txt数据
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetTxt(string fileName, bool tryCacheFirst)
    {

        string path = GetResourceURL(fileName, "txt", tryCacheFirst);
        if (path != null)
        {
#if UNITY_WEBPLAYER
            //return GetWWWResource(path).text;
            TextAsset resource = Resources.Load(fileName) as TextAsset;
            return resource.text;            
#else
            //strip掉开始的file://
            string stripedpath = path.Substring(7);
            return File.ReadAllText(stripedpath);
#endif
        }

        DebugUtil.Error("Load " + fileName + " failed");
        return null;
    }

    /// <summary>
    /// 获取一个lang数据
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static byte[] GetLanguage(string fileName)
    {
        string path = GetResourceURL(fileName, "lang", true);
        if (path != null)
        {
            string stripedpath = path.Substring(7);
            return File.ReadAllBytes(stripedpath);
        }

        DebugUtil.Error("Load " + fileName + " failed");
        return null;
    }

    /// <summary>
    /// 获取一个replay文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static byte[] GetReplay(string fileName)
    {
        string path = GetResourceURL(fileName, "replay", true);
        if (path != null)
        {
            string stripedpath = path.Substring(7);
            return File.ReadAllBytes(stripedpath);
        }

        DebugUtil.Error("Load " + fileName + " failed");
        return null;
    }

    /// <summary>
    /// 释放一个UnityObject，可以是一个GameObject，一个Component
    /// </summary>
    /// <param name="obj"></param>
    public static void DestoryObject(UnityEngine.Object obj)
    {
        Object.Destroy(obj);
        obj = null;
    }

    public static void DestoryObject(UnityEngine.Object obj, float time)
    {
        Object.Destroy(obj, time);
    }

    public static string GetResourceURL(string assetName, string extname)
    {
        return GetResourceURL(assetName, extname, true);
    }


    /// <summary>
    /// 返回资源的路径。
    /// 在PC平台下，直接返回GetAssetBundlePath()路径下的资源路径
    /// 在iOS平台下先查询iOSAssetPath下，如果资源存在，则返回这个资源，否则再查询 /myapplication.app/目录下。
    /// 外部类应该使用这个接口来获取某一个资源的路径
    /// 如果updated为true，用以上逻辑，false的情况，只从.app路径里读取
    /// </summary>
    /// <param name="assetName">文件名</param>
    /// <param name="extname">文件后缀名</param>
    /// <returns>file://文件路径</returns>
    public static string GetResourceURL(string assetName, string extname, bool tryCacheFirst)
    {
        string cachedDataPath = GamePath.GetCachedAssetBundlePath();
        string cachedDataPathFull = null;
        if (cachedDataPath != null)
        {
            cachedDataPath = string.Format("{0}{1}.{2}", cachedDataPath, assetName, extname);
            cachedDataPathFull = "file:///" + cachedDataPath;
        }
        string dataPath = string.Format("{0}{1}.{2}", GamePath.GetLocalAssetPath(), assetName, extname);
        string dataPathFull = string.Format("{0}{1}.{2}", GamePath.GetLocalAssetPathFullQualified(), assetName, extname);

        if (tryCacheFirst && cachedDataPath != null)
        {
            if (File.Exists(cachedDataPath))
            {
                Debug.Log("[GetResourceURL]dataPath = " + cachedDataPathFull);
                return cachedDataPathFull;
            }
        }

        // #if !USE_FINAL_DATA
        //         if (GamePlatform.InEditor)
        //         {
        //             string editorDataPath = string.Format("{0}{1}.{2}", Application.dataPath + "/Binaries/", assetName, extname);
        //             //editor优先使用Binary中的数据
        //             if (File.Exists(editorDataPath))
        //             {
        //                 //DebugOutPut.Log("dataPath = " + editorDataPath, DebugLogLevel.LogLevel1);
        //                 return "file://" + editorDataPath;
        //             }
        //         }
        // #endif
        if (File.Exists(dataPath))
        {
            Debug.Log("[GetResourceURL]dataPath = " + dataPathFull);
            return dataPathFull;
        }
        else
        {
            DebugUtil.Error("Can not find dataPath = " + dataPath + "!!!");
        }

        //DebugOutPut.Log("data not found : " + dataPath, DebugLogLevel.LogLevel5);
        return null;
    }

    public static void ClearnUpdateResDir()
    {
#if !UNITY_WEBPLAYER

        string toberemovedir = GamePath.GetCachedAssetBundlePath();
        if (Directory.Exists(toberemovedir))
        {
            Directory.Delete(toberemovedir, true);
        }
#endif
    }
}
