using UnityEngine;
using System.Collections;

namespace FightNew
{
	public class FightStartTemp : MonoBehaviour 
	{
		void Awake()
		{
			Application.targetFrameRate = 60;
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 60;
		}

		[Range(1,30)]
		public int Level;

		private AsyncOperation _asyn;
		// Use this for initialization
		void Start () 
		{
			#if !FightTest
			Debug.LogError("playerSetting -> otherSetting中去设置：FightTest");
			#else
			_asyn = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("UIFight",UnityEngine.SceneManagement.LoadSceneMode.Additive);
			#endif

		}

		private bool _hasChangeScene = false;
		void Update()
		{
			if(!_hasChangeScene && _asyn != null && _asyn.isDone)
			{
				_hasChangeScene = true;
				FightMgr.Instance.StartLevel(Level);
				FightMgr.Instance.IsFighting = true;
			}
		}
	}
}
