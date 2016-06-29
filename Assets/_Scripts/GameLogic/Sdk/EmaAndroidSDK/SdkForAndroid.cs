using UnityEngine;
using System.Collections;

public class SdkForAndroid : SdkBase 
{

#if NEWEMA
	private AndroidJavaClass jc;

	public SdkForAndroid():base()
	{
		#if !UNITY_EDITOR && UNITY_ANDROID
		jc = new AndroidJavaClass("com.mius.sdmb.sdk.SdkApi");
		#endif
	}
	
	public override void Init (int flag = 0)
	{
		if(jc != null)
		{
			jc.CallStatic("Init",flag);
		}
	}

	public override void Login ()
	{
		if(jc != null)
		{
			jc.CallStatic("Login");
		}
	}

	public override void Logout ()
	{
		if(jc != null)
		{
			jc.CallStatic("Logout");
		}
	}

	public override void GameCenter ()
	{
		if(jc != null)
		{
			jc.CallStatic("GameCenter");
		}
	}

	public override void Pay (string payInfo)
	{
		if(jc != null)
		{
			jc.CallStatic("Pay",payInfo);
		}
	}

	public override void ResetPayState ()
	{
		if(jc != null)
		{
			jc.CallStatic("ResetPay");
		}
	}

	public override void GameInfo (string gameInfo)
	{
		if(jc != null)
		{
			jc.CallStatic("GameInfo",gameInfo);
		}
	}

	public override void DoQuitSdk ()
	{
		if(jc != null)
		{
			jc.CallStatic("DoQuitSdk");
		}
	}

#endif

}
