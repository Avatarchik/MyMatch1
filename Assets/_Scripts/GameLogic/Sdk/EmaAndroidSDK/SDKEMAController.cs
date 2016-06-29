using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

public class SDKEMAController : MonoBehaviour 
{
	public enum EnumCallMsg
	{
		InitSuccess = 0,
		InitFail = 1,
		LoginSuccess = 2,
		LoginFail = 3,
		LoginCancel = 4,
		LogoutSuccess = 5,
		LogoutFail = 6,
		PaySuccess = 7,
		PayFailed = 8,
		PayCancel = 9,
		PayOrderSubmitted = 10,
		PayArgsError = 11,
		LoginSwitch = 12,
		GameContinue = 13,
		GameExit = 14,
	}

	private bool _isInitSuccess = false;
	
	private string _statusInfo;
	public string StatusInfo
	{
		get
		{
			return _statusInfo;
		}
	}

	private string _userId;
	public string UserId
	{
		get
		{
			return _userId;//AnySDKUser.getInstance().getUserID();
		}
	}

	private SdkBase _sdkApi;

	#region Instance
	private static SDKEMAController _instance;
	
	public static SDKEMAController Instance
	{
		get
		{
			if(_instance == null)
			{
				GameObject go = new GameObject("SDKEMAController");
				_instance = go.AddComponent<SDKEMAController>();
			}
			
			return _instance;
		}
	}
	#endregion

	void Awake()
	{
		#if UNITY_EDITOR
		_sdkApi = new SdkBase();
		#elif UNITY_ANDROID
		_sdkApi = new SdkForAndroid();
		#endif
	}
	public void Init()
	{
		DebugUtil.Info("开始初始化");
		_sdkApi.Init();
	}
	
	public void Login()
	{
		_sdkApi.Login();
	}
	
	public void Logout()
	{
		_sdkApi.Logout();
	}
	
	public void GameCenter()
	{
		_sdkApi.GameCenter();
	}

	public bool isDoQuit = false;
	public void DoQuitSdk()
	{
		if(isDoQuit)
			return;

		isDoQuit = true;
		_sdkApi.DoQuitSdk();
	}

	public void OnResponseClient(string msg)
	{
		int code = System.Convert.ToInt32 (msg);

		switch (code) 
		{
			case (int)EnumCallMsg.InitSuccess://初始化SDK成功回调
				//Debug.Log ("---->>>>>kInitSuccess----->>>>> ");
				_isInitSuccess = true;
				EventDispatcher.TriggerEvent<bool>(SdkLogin.SDK_LOGIN_INI_CBK,true);
				break;
			case (int)EnumCallMsg.InitFail://初始化SDK失败回调
				//Debug.Log ("---->>>>>kInitFail----->>>>> ");
	//			MessageTip.Push ("初始化失败！");
				EventDispatcher.TriggerEvent<bool>(SdkLogin.SDK_LOGIN_INI_CBK,false);
				break;
			case (int)EnumCallMsg.LoginSuccess://登陆成功回调
				//Debug.Log ("---->>>>>kLoginSuccess----->>>>> ");
//				ConnectServerTips.Close();
//				PlatformManager.S.OnLoginSuccess (msg);
				EventDispatcher.TriggerEvent<bool>(SdkLogin.SDK_LOGIN_LOGIN_CBK,true);
				break;
			case (int)EnumCallMsg.LoginFail://登陆失败回调
			case (int)EnumCallMsg.LoginCancel://登陆取消回调
				//Debug.Log ("---->>>>>kLoginFail----->>>>> ");
//				ConnectServerTips.Close();
				EventDispatcher.TriggerEvent<bool>(SdkLogin.SDK_LOGIN_LOGIN_CBK,false);
				break;
			case (int)EnumCallMsg.LogoutSuccess://登出成功回调
			case(int)EnumCallMsg.LoginSwitch://切换账号
//				//Debug.Log ("---->>>>>kLogoutSuccess----->>>>>");
//				GlLogin.Start();
//				GlLogin.ResetStatusToLogin();
				EventDispatcher.TriggerEvent(APPMonoController.SDK_LOGIN_SWITCH_CBK);
				break;
			case (int)EnumCallMsg.LogoutFail://登出失败回调
				//Debug.Log ("---->>>>>kLogoutFail----->>>>> ");
				break;
			case (int)EnumCallMsg.PaySuccess://支付成功回调
	//			SDKController.Instance.StopCoroutine("ShowLoading");
	//			SDKController.Instance.StartCoroutine("ShowLoading");
				GetOrderId(1);
				break;
			case (int)EnumCallMsg.PayFailed://支付失败回调
				CancelPay();
	//			MessageTip.Push ("支付失败");
				GetOrderId(2);
				break;
			case (int)EnumCallMsg.PayCancel://支付取消回调
				CancelPay();
	//			MessageTip.Push ("支付取消");
				break;
			case (int)EnumCallMsg.PayOrderSubmitted://支付超时回调
	//			MessageTip.Push ("支付超时");
				CancelPay();
				break;
			case (int)EnumCallMsg.PayArgsError://支付信息不完整
	//			MessageTip.Push ("支付信息不完整");
				CancelPay();
				break;
	//			/**
	//		 * 新增加:正在进行中回调
	//		 * 支付过程中若SDK没有回调结果，就认为支付正在进行中
	//		 * 游戏开发商可让玩家去判断是否需要等待，若不等待则进行下一次的支付
	//		 */
	//		case (int)EnumCallMsg.PayOrderSubmitted:
	//			Debug.Log("===== 支付正在进行中 =======");
	//			AnySDKIAP.getInstance().resetPayState();
	//			break;
			case (int)EnumCallMsg.GameContinue:
					isDoQuit = false;
					break;
			case (int)EnumCallMsg.GameExit:
					isDoQuit = false;
					Application.Quit();
				break;
			default:
				Debug.Log("===== default: =======:" + msg);
	//			ConnectServerTips.Close();
	//			//MessageTip.Push ("支付信息不完整" + (int)PayResultCode.kPayNetworkError);
	//			CancelPay();
				break;
		}
	}

	private void CancelPay()
	{
		orderIdReceived = string.Empty;
//		PayItem.PayingID = 0;
//		PayItem.IsInPaying = false;
//		ConnectServerTips.Close();
	}

	public static bool IsTBT
	{
		get
		{
			return SDKEMAController.ReceivedChanelId == 207;
		}
	}

	#region pay
	public static int ReceivedChanelId = 199;
	public void ReceiveChanelId(string chanelId)
	{
		int plantformId = 0;
		if(int.TryParse(chanelId,out plantformId))
		{
			ReceivedChanelId = 100 + plantformId;
		}
		else
		{
			ReceivedChanelId = 199;
		}

		//EmaSDK.getInstance().getChannelId()
	}

	private string orderIdReceived = string.Empty;
	public void ReceiveOrderId(string orderId)
	{
		orderIdReceived = orderId;
	}
	/**
		 * 获取订单号
		 */
	public void GetOrderId(int result) 
	{
		DebugUtil.Info( "AnySDK@ getOrder id " + orderIdReceived ); 

		if(!string.IsNullOrEmpty(orderIdReceived))
		{
			//GameMgr.S.controlMsg.ClientReturnPayResult(GameMgr.S.controlMsg.serveReturnSerialIDMsg.SerialID,orderIdReceived,result);
		}
		//GameMgr.S.controlMsg.ClientPay((int)_PayID, _Type);
		orderIdReceived = string.Empty;
	}
	
	/**
		 * 支付
		 */
	public void PayForProduct(string price,string id,string name,string serialId) 
	{
		// android 统一字段 有的最好填写
//		JsonData j = new JsonData();
//		j["Product_Id"] = id; // 订单号
//		int priceReal = int.Parse(price) * 100;
//		j["Product_Price"] = priceReal.ToString(); // token
//		j["Product_Name"] = ReplaceName(name); // 渠道平台用户id
//		j["Server_Id"] = GameMgr.roleInfo.GetPropInt("ServerID").ToString(); // 单价
//		j["Product_Count"] = "1"; //配好商品编码的总价（方式二）
//		j["Role_Id"] = GameMgr.roleInfo.roleID;   //兑换比率
//		j["Role_Name"] = string.IsNullOrEmpty(GameMgr.roleInfo.Name.Trim()) ? "Role_Name" : GameMgr.roleInfo.Name.Trim();  //商品名称
//		j["Role_Grade"] = GameMgr.roleInfo.level.ToString(); //商品数量
//		j["Role_Balance"] = GameMgr.roleInfo.gold.ToString(); //商品编码（方式二）
//		j["EXT"] = serialId; //商品编码（方式二）
//
//		_sdkApi.Pay(j.ToJson());
	}
	
	private string ReplaceName(string name)
	{
		while(name.IndexOf('[') >= 0)
		{
			int sta = name.IndexOf('[');
			
			if(name.IndexOf(']') >= 0)
			{
				int end = name.IndexOf(']');
				string strRep = name.Substring(sta,end - sta + 1);
				name = name.Replace(strRep,"");
			}
			else
			{
				break;
			}
		}
		
		return name;
	}
	
	public void ResetPaying()
	{
		_sdkApi.ResetPayState();
	}
	#endregion

	public void SendGameInfo(int lv,string userId,string userName) 
	{
		//Debug.Log("semdGameInfo111");
		object[] o = Util.CallMethod("UILoginModule", "GetLoginisRegister");
		//Debug.Log("semdGameInfo111-1");	
		bool isNewAccount = (o[0].ToString() == "1" ? true : false);
		//Debug.Log("semdGameInfo111-2");
		// android 统一字段 有的最好填写
		JsonData j = new JsonData();
		j["roleId"] = userId;   //兑换比率
		j["roleName"] = "userName";  //商品名称
		j["roleLevel"] = lv.ToString(); //商品数量
		
		j["zoneId"] = ""; //商品数量
		j["zoneName"] = "";
		
		
		j["dataType"] = isNewAccount ? "2" : "1";
		j["ext"] = "ext"; //商品编码（方式二）
//		Debug.Log("semdGameInfo222");
		_sdkApi.GameInfo(j.ToJson());
//		Debug.Log("semdGameInfo333");
	}

	public void ReceiveUserId(string userId)
	{
		//Debug.Log(" ==== receive userId ====" + userId);
		_userId = userId;
	}

	public void ReceiveQuitInfo(string quitInfo)
	{
		//Debug.Log(" ==== ReceiveQuitInfo ====" + quitInfo);
	
		isDoQuit = false;
		if(quitInfo == "ExitByGame")
		{
			UIMgr.Instance.ShowMessageBox("确定要退出游戏?","确定",OnConfirmBtnClick,"取消",null);
		}
	}

	private void OnConfirmBtnClick(GameObject goBtn)
	{
		Application.Quit();		
	}
}
