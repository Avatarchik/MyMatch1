using UnityEngine;
using System.Collections;

public class SdkBase 
{
	public virtual void Init(int flag = 0){}

	public virtual void Login(){}

	public virtual void Logout(){}

	public virtual void Pay(string payInfo){}

	public virtual void GameCenter(){}

	public virtual void ResetPayState(){}

	public virtual void GameInfo(string gameInfo){}

	public virtual void DoQuitSdk(){}

}
