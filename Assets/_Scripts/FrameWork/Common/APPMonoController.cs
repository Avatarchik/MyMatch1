/*******************************************************
 * 
 * 文件名(File Name)：             CoroutineController
 *
 * 作者(Author)：                  Yangzj
 *
 * 创建时间(CreateTime):           2016/02/25 17:12:38
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace MyFrameWork
{
	public class APPMonoController : DDOLSingleton<APPMonoController>
	{
		public const string SDK_LOGIN_SWITCH_CBK = "SDK_LOGIN_SWITCH_CBK";

		void Awake()
		{
			EventDispatcher.AddListener(SDK_LOGIN_SWITCH_CBK,OnSwitchCallback);
		}

		void OnDestroy()
		{
			EventDispatcher.RemoveListener(SDK_LOGIN_SWITCH_CBK,OnSwitchCallback);
		}

		public void StartUpdate()
		{
		}

		void Update()
		{
			UpdateQuit();

//			if(Input.GetKeyDown(KeyCode.A))
//			{
//				UIMgr.Instance.ShowMessageBox("确定要退出游戏?","确定",OnConfirmBtnClick,"取消",null);
//			}
//			if(Input.GetKeyDown(KeyCode.Escape))
//			{
//				OnSwitchCallback();
//			}
		}

		private void OnConfirmBtnClick(GameObject goBtn)
		{
			Application.Quit();		
		}

		/// <summary>
		/// 返回登陆界面，清除变量
		/// </summary>
		public override void ReleaseValue()
		{
            //AppFacade.Instance.GetManager<ResourceMgr>(ManagerName.Resource).OnReleaseValue();
            AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI).OnReleaseValue();
            EventDispatcher.ReleaseValue();
//			TimerManager.Instance.ReleaseValue();
		}

		void OnApplicationQuit()
		{
            //AppFacade.Instance.GetManager<ResourceMgr>(ManagerName.Resource).OnApplicationQuit();
			#if !FightTest
            AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI).OnAppQuit();
			#endif
            EventDispatcher.OnApplicationQuit();
//			TimerManager.Instance.OnApplicationQuit();
//			LayerMgr.Instance.OnApplicationQuit();
		}

		void UpdateQuit()
		{
			#if NEWEMA
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if(FightNew.FightMgr.Instance.IsInFightScene)
				{
					return;
				}

				SDKEMAController.Instance.DoQuitSdk();

				//			if (GameMgr.roleInfo == null || string.IsNullOrEmpty(GameMgr.roleInfo.Name))
				//				//.fightType != FightType.Arena && Scene.currentScene.fightType != FightType.Escort))
				//			{
				//				SDKEMAController.Instance.DoQuitSdk();
				//			}

				//			if (GameMgr.roleInfo == null || string.IsNullOrEmpty(GameMgr.roleInfo.Name)
				//			    || (GameMgr.roleInfo != null && GameMgr.roleInfo.level >= 5))
				//				//.fightType != FightType.Arena && Scene.currentScene.fightType != FightType.Escort))
				//			{
				//				SDKEMAController.Instance.DoQuitSdk();
				//			}
				//			else if(GameMgr.roleInfo != null && GameMgr.roleInfo.level < 5)
				//			{
				//				if(!isFirstQuitNote)
				//				{
				//					MessageTip.Push("大侠，再体验下吧 ^_^");
				//				}
				//				else
				//				{
				//					MessageTip.Push("不要那么心急，新手过后更精彩，你懂的...");
				//				}
				//				
				//				isFirstQuitNote = !isFirstQuitNote;
				//			}
			}
			#elif APPSTORE
//			if (Input.GetKey(KeyCode.Escape))
//			{
//			if (GameMgr.roleInfo != null)
//			//.fightType != FightType.Arena && Scene.currentScene.fightType != FightType.Escort))
//			{
//
//			if (_quitWaitTime > 0)
//			{
//			_quitWaitTime -= Time.deltaTime;
//
//			if (_quitWaitTime < 0f)
//			{
//			Application.Quit();
//			}
//			}
//			else
//			{
//			_quitWaitTime = 1f;
//
//			MessageTip.Push("长按返回键退出游戏");
//			}
//			}
//			}
//			else
//			{
//			_quitWaitTime = 0f;
//			}
			#endif


		}

		private void OnSwitchCallback()
		{
//			if(FightNew.FightMgr.Instance.IsInFightScene)
//				FightNew.FightMgr.Instance.ClearLevel();
//
//			UnityEngine.SceneManagement.SceneManager.LoadScene((int)SenceName.SENCE_LOGIN);
		}
	}
}
