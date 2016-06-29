using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

public class AppFacade : Facade
{
    private static AppFacade _instance;

    public AppFacade() : base()
    {
    }

    public static AppFacade Instance
    {
        get{
            if (_instance == null) {
                _instance = new AppFacade();
            }
            return _instance;
        }
    }

    override protected void InitFramework()
    {
        base.InitFramework();
    }

    /// <summary>
    /// 启动框架
    /// </summary>
    public void StartUp()
    {
        GameObject obj = null;
        if (GameObject.Find("UI Root") == null )
        {
            obj = Resources.Load("UIRoot/UI Root") as GameObject;
            obj = GameObject.Instantiate(obj);
            obj.name = "UI Root";
        }

        if(GameObject.Find("GameManager") == null)
        {
            obj = Resources.Load("UIRoot/GameManager") as GameObject;
            obj = GameObject.Instantiate(obj);
            obj.name = "GameManager";
        }

        if (GameObject.Find("GameEntrance") == null)
        {
            obj = Resources.Load("UIRoot/GameEntrance") as GameObject;
            obj = GameObject.Instantiate(obj);
            obj.name = "GameEntrance";
        }

        AppFacade.Instance.AddManager<TutorialManager>(ManagerName.Tutorial);
        AppFacade.Instance.AddManager<LuaManager>(ManagerName.Lua);
        AppFacade.Instance.AddManager<UIMgr>(ManagerName.UI);
        AppFacade.Instance.AddManager<MusicManager>(ManagerName.Music);
        AppFacade.Instance.AddManager<TimerManager>(ManagerName.Timer);
        AppFacade.Instance.AddManager<NetworkManager>(ManagerName.Network);
        AppFacade.Instance.AddManager<ResourceMgr>(ManagerName.Resource);
        AppFacade.Instance.AddManager<ThreadManager>(ManagerName.Thread);
        AppFacade.Instance.AddManager<FightDataManager>(ManagerName.FightData);
        AppFacade.Instance.AddManager<LuaUtilityManager>(ManagerName.LuaUtil);
        AppFacade.Instance.AddManager<LoadingManager>(ManagerName.Loading);
        AppFacade.Instance.GetManager<LoadingManager>(ManagerName.Loading).ShowLoading();
        AppFacade.Instance.AddManager<NetConditionManager>(ManagerName.NetCondition);
        AppFacade.Instance.AddManager<GameManager>(ManagerName.Game);
        AppFacade.Instance.GetManager<GameManager>(ManagerName.Game).Init();
    }
}

