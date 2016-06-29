using UnityEngine;


public class GamePlatform
{
	public static bool InEditor //
	{
		get
		{
			return 	Application.platform == RuntimePlatform.WindowsEditor ||
					Application.platform == RuntimePlatform.OSXEditor;
		}
	}

    public enum PlatformEnum
    {
        PlatformiOS = 1,
        PlatformAndroid = 2,
        PlatformWphone = 3,
        PlatformWeb = 4,
        PlatformWin = 5,
        PlatformMac = 6,
        PlatformEditor = 7
    }	
	
	public static PlatformEnum SwitchedPlatform
	{
		get
		{
#if UNITY_STANDALONE_OSX
			return PlatformEnum.PlatformMac;
#elif UNITY_STANDALONE_WIN
			return PlatformEnum.PlatformWin;
#elif UNITY_WEBPLAYER
			return PlatformEnum.PlatformWeb;
#elif UNITY_IPHONE
			return PlatformEnum.PlatformiOS;
#elif UNITY_ANDROID
			return PlatformEnum.PlatformAndroid;
#else
			return PlatformEnum.PlatformEditor;
#endif
		}
	}

    public static PlatformEnum PlatformType
    {
        get
        {
            if (InEditor)
            {
                return PlatformEnum.PlatformEditor;
            }
			else
			{
				return SwitchedPlatform;
			}
        }
    }
	
	
	public static bool WebPlayerBrowser 
	{
		get
		{
			//return true;
			//return false;
			return SwitchedPlatform == PlatformEnum.PlatformWeb &&
					(!InEditor);
		}
	}
}

