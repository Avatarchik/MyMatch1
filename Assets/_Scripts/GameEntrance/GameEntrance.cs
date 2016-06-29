/*******************************************************
 * 
 * 文件名(File Name)：             TestOpenUI
 *
 * 作者(Author)：                  Yangzj
 *
 * 创建时间(CreateTime):           2016/02/29 18:18:13
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using MyFrameWork;

public class GameEntrance : MonoBehaviour 
{
	public static GameEntrance Instance;

	public bool IsTestFight = false;
	[Range(1,100)]
	public int LevelId = 1;

	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () 
	{
        AppFacade.Instance.StartUp();
		MusicManager.Instance.PlayBacksound("Music/BGM_Main",true);

//		UnityEngine.SceneManagement.SceneManager.LoadScene((int)SenceName.SENCE_LOGIN,UnityEngine.SceneManagement.LoadSceneMode.Additive);
	}

	void OnUILoaded(BaseUI ui)
	{
		DebugUtil.Debug("ui loaded:" + ui.GetUIType());
	}

}
