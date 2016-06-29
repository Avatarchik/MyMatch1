using UnityEngine;
using System.Collections;
using MyFrameWork;

public class SdkLogin : MonoBehaviour 
{
	public const string SDK_LOGIN_INI_CBK = "SDK_LOGIN_INI_CBK";
	public const string SDK_LOGIN_LOGIN_CBK = "SDK_LOGIN_LOGIN_CBK";

	private bool _hasClickLogin = false;
	private UIButton _btnLogin;
	private UIInput _inputUser;

	private void Awake()
	{
		EventDispatcher.AddListener<bool>(SDK_LOGIN_INI_CBK,OnInitCallback);
		EventDispatcher.AddListener<bool>(SDK_LOGIN_LOGIN_CBK,OnLoginCallback);
		EventDispatcher.AddListener("OnConnectSocket",OnConnectSocket);

		_hasClickLogin = false;

		#if NEWEMA
		transform.Find("Sprite_LoginBtn").gameObject.SetActive(false);
		transform.Find("Simple Input Field").gameObject.SetActive(false);

		_btnLogin = transform.Find("Sprite_LoginSdkBtn").GetComponent<UIButton>();
		_btnLogin.gameObject.SetActive(true);
		//UIEventListener listener = UIEventListener.Get(_btnLogin.gameObject);
		//listener.onClick = OnLoginSdkBtnClick;

		_inputUser = transform.Find("Simple Input Field").GetComponent<UIInput>();

		SDKEMAController.Instance.Init();
		#endif
	}

	void OnDestroy()
	{
		EventDispatcher.RemoveListener<bool>(SDK_LOGIN_INI_CBK,OnInitCallback);
		EventDispatcher.RemoveListener<bool>(SDK_LOGIN_LOGIN_CBK,OnLoginCallback);
		EventDispatcher.RemoveListener("OnConnectSocket",OnConnectSocket);
//		EventDispatcher.RemoveListener(SDK_LOGIN_SWITCH_CBK,OnSwitchCallback);
	}

	public void OnLoginSdkBtnClick()
	{
		if(_hasClickLogin)
		{
			//弹提示登录中
			Debug.Log("登录中");
		}
		else
		{
			_hasClickLogin = true;
			Debug.Log("点击登录");
			SDKEMAController.Instance.Login();
            transform.Find("C_Logining").gameObject.SetActive(true);
		}
	}

	/// <summary>
	/// 初始化回调
	/// </summary>
	/// <param name="isSuccess">If set to <c>true</c> is success.</param>
	private void OnInitCallback(bool isSuccess)
	{
		if(isSuccess)
		{
			//初始化成功

			if(_hasClickLogin)
			{
				//按过登录按钮,登录
				DebugUtil.Info("初始化成功，开始登录");
				SDKEMAController.Instance.Login();
			}
		}
		else
		{
			//初始化失败,继续初始化
			DebugUtil.Info("初始化失败");
//			SDKEMAController.Instance.Init();
		}
	}

	private bool _hasLoginSuccess = false;
	/// <summary>
	/// 登录回调
	/// </summary>
	/// <param name="isSuccess">If set to <c>true</c> is success.</param>
	private void OnLoginCallback(bool isSuccess)
	{
		if(isSuccess)
		{
			//登录成功,连接游戏服务器
			DebugUtil.Info("连接游戏服务器，userId:" + SDKEMAController.Instance.UserId + ",channelId:" + SDKEMAController.ReceivedChanelId);

			_hasLoginSuccess = true;
			NetworkManager.Instance.SendReConnect();

//			Util.CallMethod("UILoginCtrl","SendLogin",SDKEMAController.Instance.UserId,SDKEMAController.ReceivedChanelId.ToString(),string.Empty);
		}
		else
		{
			//登录失败
			DebugUtil.Error("登录失败");
			_hasClickLogin = false;
		}
	}

	private void OnConnectSocket()
	{
		#if NEWEMA
		if(_hasLoginSuccess)
			Util.CallMethod("UILoginCtrl","SendLogin",SDKEMAController.Instance.UserId,SDKEMAController.ReceivedChanelId.ToString(),string.Empty);
		#endif
	}

}
