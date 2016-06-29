using UnityEngine;
using MyFrameWork;


// 负责记录和管理资源路径
public class GamePath
{
#if UNITY_STANDALONE_WIN
    public static string UIPrefix = "UI/";
    public static string EffectPrefix = "Effect/";
#else
    public static string UIPrefix = "UI/";
    public static string EffectPrefix = "Effect/";
#endif

    public static string AndroidExternalStorageDirectory = "";

    public static string GetWebAssetPath()
    {
        return Util.WebURL;
    }
    /// <summary>
    /// 获取当前平台数据包名
    /// </summary>
    /// <returns></returns>
    public static string GetPlatformDataName()
    {
        GamePlatform.PlatformEnum platform = GamePlatform.SwitchedPlatform;
        switch (platform)
        {
            case GamePlatform.PlatformEnum.PlatformAndroid:
                if (GamePlatform.InEditor)
                {
                    return "AndroidData";
                }
                else
                {
                    return "AndroidData";
                }
            case GamePlatform.PlatformEnum.PlatformiOS:
                return "IOSData";
            case GamePlatform.PlatformEnum.PlatformMac:
                return "MacData";
            case GamePlatform.PlatformEnum.PlatformWeb:
                return "WebData";
            case GamePlatform.PlatformEnum.PlatformWin:
                return "WinData";
            case GamePlatform.PlatformEnum.PlatformWphone:
                return "WPData";
            default:
            case GamePlatform.PlatformEnum.PlatformEditor:
                return "WinData";
        }
    }

    /// <summary>
    /// 获取当前数据包的Latest数据包路径
    /// </summary>
    /// <returns></returns>
    public static string GetPlatformLatestDataPath()
    {
        return GetPlatformDataName() + "/LatestData/";
    }

    /// <summary>
    /// 根据Assets的路径，在PC或者MacOS平台下，路径为Application.dataPath/../ + ResourceManager.PlatFormDataPath
    /// 在iOS平台下，路径为var/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/myappname.app/
    /// </summary>
    /// <returns></returns>
    public static string GetLocalAssetPath()
    {
        GamePlatform.PlatformEnum platform = GamePlatform.SwitchedPlatform;
        switch (platform)
        {
            case GamePlatform.PlatformEnum.PlatformAndroid:
                if (GamePlatform.InEditor)
                {
                    return Util.DataPath + GetPlatformLatestDataPath();
                }
                else
                {
                    return Util.DataPath + GetPlatformLatestDataPath();
                }
            case GamePlatform.PlatformEnum.PlatformiOS:
                if (GamePlatform.InEditor)
                {
                    return Util.DataPath + GetPlatformLatestDataPath();
                }
                else
                {
                    // Application.dataPath returns
                    return Util.DataPath + GetPlatformLatestDataPath();
                }
            case GamePlatform.PlatformEnum.PlatformWeb:
                if (GamePlatform.WebPlayerBrowser)
                {
                    return GetWebAssetPath();
                }
                else
                {
                    return Application.dataPath + "/StreamingAssets/" + GetPlatformLatestDataPath();
                }
            case GamePlatform.PlatformEnum.PlatformMac:
            case GamePlatform.PlatformEnum.PlatformWin:
            case GamePlatform.PlatformEnum.PlatformWphone:
            default:
            case GamePlatform.PlatformEnum.PlatformEditor:
                return Application.dataPath + "/StreamingAssets/" + GetPlatformLatestDataPath();
        }
    }

    /// <summary>
    /// 获取完整的数据路径
    /// 除web平台，+"file://"
    /// </summary>
    /// <returns></returns>
    public static string GetLocalAssetPathFullQualified()
    {
        if (GamePlatform.WebPlayerBrowser)
            return GetLocalAssetPath();
        else
        {
            return "file:///" + GetLocalAssetPath();
        }    
    }

    /// <summary>
    /// 获取iOS平台上有读写权限的Documents目录绝对路径，在iOS平台上只有这个路径有写权限
    /// DocumentsPath = /var/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/Documents/
    /// </summary>
    /// <returns>绝对路径</returns>
    public static string GetiOSLibCachesPath()
    {
        //在iOS平台下datpath = /var/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/myappname.app/Data
        string sDocumentPath = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
        //sDocumentPath = sDocumentPath.Substring(0, sDocumentPath.LastIndexOf('/')) + "/Documents/";
        sDocumentPath = sDocumentPath.Substring(0, sDocumentPath.LastIndexOf('/')) + "/Library/Caches/";//ios5对document目录写入加入了限制       
        return sDocumentPath;
    }

    /// <summary>
    /// 在Documents/GloryOfIris/xxx/LastedData目录。
    /// 用于存放版本更新时候的资源
    /// </summary>
    /// <returns></returns>
    private static string GetiOSLibCachesAssetPath()
    {
        return GetiOSLibCachesPath() + "TP/" + GetPlatformLatestDataPath();
    }

    /// <summary>
    /// 获取当前平台Cach数据的路径
    /// IOS平台特殊，其他都为空
    /// </summary>
    /// <returns></returns>
    public static string GetCachedAssetBundlePath()
    {
        GamePlatform.PlatformEnum platform = GamePlatform.SwitchedPlatform;
        switch (platform)
        {
            case GamePlatform.PlatformEnum.PlatformiOS:
                if (GamePlatform.InEditor)
                {
                    return null;
                }
                else
                {
                    return GetiOSLibCachesAssetPath();
                }
            case GamePlatform.PlatformEnum.PlatformAndroid:
            case GamePlatform.PlatformEnum.PlatformMac:
            case GamePlatform.PlatformEnum.PlatformWeb:
            case GamePlatform.PlatformEnum.PlatformWin:
            case GamePlatform.PlatformEnum.PlatformWphone:
            default:
            case GamePlatform.PlatformEnum.PlatformEditor:
                return null;
        }
    }

    /// <summary>
    /// 获取下载资源存放的路径
    /// 在PC上，路径为Application.dataPath/../Download/`
    /// 在iOS上，路径为Documents/GloryOfIris/Download/
    /// 在Android上，路径为/Android/data/com.irislynx.gloryofiris/Download/
    /// </summary>
    /// <returns></returns>
    public static string GetDownloadPath()
    {
        GamePlatform.PlatformEnum platform = GamePlatform.SwitchedPlatform;
        switch (platform)
        {
            case GamePlatform.PlatformEnum.PlatformiOS:
                return GetiOSLibCachesPath() + "TP/Download/";
            case GamePlatform.PlatformEnum.PlatformAndroid:
                return AndroidExternalStorageDirectory + "/Android/data/com.ema.tp/Download/";//资源更新包保存路径
            case GamePlatform.PlatformEnum.PlatformEditor:
            case GamePlatform.PlatformEnum.PlatformMac:
            case GamePlatform.PlatformEnum.PlatformWeb:
            case GamePlatform.PlatformEnum.PlatformWin:
            case GamePlatform.PlatformEnum.PlatformWphone:
            default:
                return Application.dataPath + "/../Download/";
        }
    }

    /// <summary>
    /// 获取补丁释放的目标位置
    /// </summary>
    /// <returns></returns>
    public static string GetLocalStoragePath()
    {
        GamePlatform.PlatformEnum platform = GamePlatform.SwitchedPlatform;
        switch (platform)
        {
            case GamePlatform.PlatformEnum.PlatformiOS:
                return GetiOSLibCachesAssetPath();
            case GamePlatform.PlatformEnum.PlatformAndroid:
                // chadwichen 补丁包保存目标路径
                // ios补丁包是解压到ios Documents目录下，在读取的时候优先读取这个目录，而安卓的实则是将资源解压到sdcard AssetBundle下，覆盖原始资源
                // 读取的时候，就总是读到最新的资源，重新安装应用程序的时候，就清除所有的资源
                return GetLocalAssetPath();
            case GamePlatform.PlatformEnum.PlatformEditor:
            case GamePlatform.PlatformEnum.PlatformMac:
            case GamePlatform.PlatformEnum.PlatformWeb:
            case GamePlatform.PlatformEnum.PlatformWin:
            case GamePlatform.PlatformEnum.PlatformWphone:
            default:
                return GetLocalAssetPath();
        }
    }
}
