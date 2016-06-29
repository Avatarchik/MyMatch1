using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using LuaInterface;
using System;
using MyFrameWork;

namespace MyFrameWork {
    public static class LuaHelper {

        /// <summary>
        /// getType
        /// </summary>
        /// <param name="classname"></param>
        /// <returns></returns>
        public static System.Type GetType(string classname) {
            Assembly assb = Assembly.GetExecutingAssembly();  //.GetOnInitOK()ExecutingAssembly();
            System.Type t = null;
            t = assb.GetType(classname); ;
            if (t == null) {
                t = assb.GetType(classname);
            }
            return t;
        }

        /// <summary>
        /// 游戏管理器
        /// </summary>
        public static GameManager GetGameManager()
        {
            return AppFacade.Instance.GetManager<GameManager>(ManagerName.Game);
        }

        /// <summary>
        /// 面板管理器
        /// </summary>
        public static UIMgr GetPanelManager() {
            return AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI);
        }


        /// <summary>
        /// 资源管理器
        /// </summary>
        public static ResourceMgr GetResManager() {
            return AppFacade.Instance.GetManager<ResourceMgr>(ManagerName.Resource);
        }

        /// <summary>
        /// 网络管理器
        /// </summary>
        public static NetworkManager GetNetManager() {
            return AppFacade.Instance.GetManager<NetworkManager>(ManagerName.Network);
        }

        /// <summary>
        /// 音乐管理器
        /// </summary>
        public static MusicManager GetMusicManager() {
            return AppFacade.Instance.GetManager<MusicManager>(ManagerName.Music);
        }

        public static FightDataManager GetFightDataManager()
        {
            return AppFacade.Instance.GetManager<FightDataManager>(ManagerName.FightData);
        }

        public static LoadingManager GetLoadingManager()
        {
            return AppFacade.Instance.GetManager<LoadingManager>(ManagerName.Loading);
        }

        public static LuaUtilityManager GetLuaUtilityManager()
        {
            return AppFacade.Instance.GetManager<LuaUtilityManager>(ManagerName.LuaUtil);
        }
        public static Action Action(LuaFunction func) {
            Action action = () => {
                func.Call();
            };
            return action;
        }

        public static UIEventListener.VoidDelegate VoidDelegate(LuaFunction func) {
            UIEventListener.VoidDelegate action = (go) => {
                func.Call(go);
            };
            return action;
        }

        /// <summary>
        /// pbc/pblua函数回调
        /// </summary>
        /// <param name="func"></param>
        public static void OnCallLuaFunc(LuaByteBuffer data, LuaFunction func) {
            if (func != null) func.Call(data);
            Debug.LogWarning("OnCallLuaFunc length:>>" + data.buffer.Length);
        }

        /// <summary>
        /// cjson函数回调
        /// </summary>
        /// <param name="data"></param>
        /// <param name="func"></param>
        public static void OnJsonCallFunc(string data, LuaFunction func) {
            Debug.LogWarning("OnJsonCallback data:>>" + data + " lenght:>>" + data.Length);
            if (func != null) func.Call(data);
        }
    }
}