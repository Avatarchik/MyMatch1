using UnityEngine;
using System.Collections;
using MyFrameWork;

public class Loading : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		ResourceMgr.Instance.PopAssetStack();
		UIMgr.Instance.ShowUIAndCloseOthers(E_UIType.UILoadingPanel,null,(ui)=>{ResourceMgr.Instance.PushAssetStack();Resources.UnloadUnusedAssets();});
	}

}
