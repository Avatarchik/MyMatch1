/*******************************************************
 * 
 * 文件名(File Name)：             UIMgr
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/02/29 15:16:59
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;
using LuaInterface;
using UnityEngine.SceneManagement;

namespace MyFrameWork
{
    public enum SenceName
    {
        SENCE_LOGIN = 0,
        SENCE_MAIN = 1,
        SENCE_FIGHT = 2,
    }
    public class UIMgr : Manager
    {

        public static UIMgr Instance
        {
            get
            {
                return AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI);
            }
        }


        /// <summary>
        /// 已加载的界面
        /// </summary>
        private Dictionary<E_UIType, BaseUI> _dicUI = null;
        protected Stack<BaseUI> _backSequence;
        protected BaseUI _curUI = null;
        /// <summary>
        /// 命令集合
        /// </summary>
        public List<Command> _listCmd = null;

        private Transform _uiContainer;
        /// <summary>
        /// UI节点
        /// </summary>
        /// <value>The user interface container.</value>
        public Transform UIContainer
        {
            get
            {
                if (null == _uiContainer)
                {
                    GameObject prefabObj = GameObject.Find("UI Root");
                    if (prefabObj == null)
                        //Debugger.LogError("not find UIRoot when adding ui !");
                        DebugUtil.Error("not find UIRoot when adding ui !");

                    _uiContainer = prefabObj.transform.Find("Camera/UIContainer");
                }
                return _uiContainer;
            }
        }

        /// <summary>
        /// 当前uiPanel的层次
        /// </summary>
        private int _layerCurrentIndex = 0;





        void Awake()
        {
            Init();
        }

        public void Init()
        {
            _dicUI = new Dictionary<E_UIType, BaseUI>();
            _listCmd = new List<Command>();
            if (_backSequence == null)
                _backSequence = new Stack<BaseUI>();
            else
                _backSequence.Clear();

            PreLoadCommonUI();
        }

        public void OnReleaseValue()
        {
            _dicUI.Clear();
            _listCmd.Clear();
        }

        public void OnAppQuit()
        {
            _dicUI = null;
            _listCmd = null;
        }

        protected void PreLoadCommonUI()
        {
            CreateUI(E_UIType.PanelMessageBox, typeof(UIMessageBox), null);
        }

        /// <summary>
        /// 获取所有可见的ui界面
        /// </summary>
        /// <returns>The current open U.</returns>
        public List<E_UIType> GetCurrentOpenUI()
        {
            List<E_UIType> list = new List<E_UIType>();
            foreach (BaseUI ui in _dicUI.Values)
            {
                if (ui.CachedGameObject.activeInHierarchy)
                {
                    list.Add(ui.GetUIType());
                }
            }
            return list;
        }

        #region 创建UI
        /// <summary>
        /// 创建ui，不显示
        /// </summary>
        /// <param name="uiType">User interface type.</param>
        /// <param name="type">Type.</param>
        /// <param name="listener">Listener.</param>
        public void CreateUI(E_UIType uiType, System.Type monoType, System.Action<BaseUI> listener)
        {
            _listCmd.Add(Command.CreateCmd(monoType, uiType, listener));
        }

        public void CreateUI_LUA(int uiType, LuaFunction listener = null)
        {
            _listCmd.Add(Command.CreateCmd_LUA(typeof(BaseUI), (E_UIType)uiType, listener));
        }

        private void _Create(Command cmd)
        {
            BaseUI ui = null;
            _dicUI.TryGetValue(cmd.UIType, out ui);
            if (ui != null)
            {
                //已加载
                if (cmd.Listener != null)
                    cmd.Listener(ui);

                if (cmd.lua_fun != null)
                    cmd.lua_fun.Call(ui.gameObject);

                return;
            }
            else
            {
                if (AppConst.PrefabBundleMode && cmd.UIType != E_UIType.Fight 
                    && cmd.UIType != E_UIType.UILoadingPanel
                    && cmd.UIType != E_UIType.PanelMessageBox
                    && cmd.UIType != E_UIType.UILoginPanel)
                {
                    string PrefabUrl = BundleResMgr.GetResourceURL(cmd.UIType.ToString(), "unity3d");
                    if (PrefabUrl == null)
                    {
                        DebugUtil.Error("Atlas [" + cmd.UIType.ToString() + "] is null!!!");
                    }
                    WWWRequestHandle handle = BundleResMgr.Instance.RequestWWW(PrefabUrl,cmd, OnLoadUIFinished,null);
                }
                else
                    AppFacade.Instance.GetManager<ResourceMgr>(ManagerName.Resource).LoadAssetAndInstance(cmd.UIType.ToString(), (asset) => { OnLoadUIFinished(asset, cmd); });
            }
        }

        private void OnLoadUIFinished(object asset, Command cmd)
        {
            if (asset != null && cmd != null)
            {
                GameObject go = null;
                if (AppConst.PrefabBundleMode && cmd.UIType != E_UIType.Fight
                    && cmd.UIType != E_UIType.UILoadingPanel
                    && cmd.UIType != E_UIType.PanelMessageBox
                    && cmd.UIType != E_UIType.UILoginPanel)
                {
                    WWWRequestHandle handle = asset as WWWRequestHandle;
                    AssetBundle bundle = handle.RetainAsAssetBundle();
                    GameObject LoadedRes = bundle.LoadAsset(cmd.UIType.ToString()) as GameObject;
                    if (LoadedRes != null)
                    {
                        go = GameObject.Instantiate(LoadedRes) as GameObject;
                    }
                }
                else
                    go = asset as GameObject;
                go.SetActive(false);
                BaseUI ui = go.AddComponent(cmd.MonoType) as BaseUI;
                if (cmd.bLuaBehaviour)
                    go.AddComponent<LuaBehaviour>().OnInit(null);
                ui.mUIType = cmd.UIType;
                go.name = ui.GetUIType().ToString();
                _curUI = ui;
                if (ui.mUIStyle == E_UIStyle.BackClose)
                    _backSequence.Push(ui);
                AddUI(ui);

                if (ui.mUIType == E_UIType.UILoadingPanel)
                {
                    GameObject.DontDestroyOnLoad(go);
                }

                if (cmd.CommandType == Command.CmdType.CreateAndShow)
                {
                    if (cmd.lua_fun != null)
                        ShowUI_LUA(cmd.UIType, cmd.lua_fun, cmd.IsCloseOtherUIs, cmd.CreateCanCall, cmd.Params);
                    else
                        ShowUI(cmd.UIType, cmd.MonoType, cmd.Listener, cmd.IsCloseOtherUIs, cmd.CreateCanCall, cmd.Params);
                }
                else if (cmd.CreateCanCall && cmd.Listener != null)
                {
                    cmd.Listener(ui);
                }
                else if (cmd.CreateCanCall && cmd.lua_fun != null)
                {
                    cmd.lua_fun.Call(ui.gameObject);
                }
                //ui.CachedTransform.SetParent(UIContainer,false);

                ui.UIInit();

                AddColliderBgForWindow(ui);

                LayerMgr.Instance.SetLayer(ui);
            }
        }
        #endregion

        #region 显示/隐藏 UI
        /// <summary>
        /// 打开ui，并关闭其他所有
        /// </summary>
        /// <param name="uiType">UI类型</param>
        /// <param name="typeMono">绑定mono脚本类型</param>
        /// <param name="listener">回调监听</param>
        /// <param name="createCanCall">只是创建ui时是否回调</param>
        /// <param name="param">参数</param>
        public void ShowUIAndCloseOthers(E_UIType uiType, System.Type typeMono, System.Action<BaseUI> listener = null, bool createCanCall = false, params object[] param)
        {
            ShowUI(uiType, typeMono, listener, true, createCanCall = false, param);
            _layerCurrentIndex = 0;
        }

        public void ShowUIAndCloseOthers_LUA(E_UIType uiType, LuaFunction func = null,params object[] param)
        {
            ShowUI_LUA(uiType, func, true, true, param);
            _layerCurrentIndex = 0;
        }

        public BaseUI GetUIByType(E_UIType uiType)
        {
            //关闭其他ui
            if (_dicUI.ContainsKey(uiType))
                return _dicUI[uiType];

            return null;
        }

        /// <summary>
        /// 显示UI
        /// </summary>
        /// <param name="uiType">UI类型</param>
        /// <param name="typeMono">绑定mono脚本类型</param>
        /// <param name="listener">回调监听</param>
        /// <param name="isCloseOthers">是否关闭其他界面</param>
        /// <param name="createCanCall">只是创建ui时是否回调</param>
        /// <param name="param">参数</param>
        public void ShowUI(E_UIType uiType, System.Type typeMono, System.Action<BaseUI> listener = null, bool isCloseOthers = false, bool createCanCall = false, params object[] param)
        {
            if (isCloseOthers)
            {
                //关闭其他ui
                var listOpenUis = GetCurrentOpenUI();
                for (int i = 0; i < listOpenUis.Count; i++)
                {
                    if (listOpenUis[i] != uiType && listOpenUis[i] != E_UIType.UILoadingPanel)
                        _listCmd.Add(Command.DestroyCmd(listOpenUis[i]));
                }
            }

            BaseUI ui = null;
            _dicUI.TryGetValue(uiType, out ui);
            if (ui == null)
            {
                _listCmd.Add(Command.CreateAndShowCmd(uiType, typeMono, listener, isCloseOthers, createCanCall, param));
                //CreateUI(uiName,type,listener);
            }
            else
            {
                _listCmd.Add(Command.ShowCmd(uiType, listener, isCloseOthers, createCanCall, param));
            }
        }

        public void ShowUI_LUA(E_UIType uiType, LuaFunction listener = null, bool isCloseOthers = false, bool createCanCall = false, params object[] param)
        {
            if (isCloseOthers)
            {
                //关闭其他ui
                var listOpenUis = GetCurrentOpenUI();
                for (int i = 0; i < listOpenUis.Count; i++)
                {
                    if (listOpenUis[i] != uiType && listOpenUis[i] != E_UIType.UILoadingPanel)
                        _listCmd.Add(Command.DestroyCmd(listOpenUis[i]));
                }
            }

            BaseUI ui = null;
            _dicUI.TryGetValue(uiType, out ui);
            if (ui == null)
            {
                _listCmd.Add(Command.CreateAndShowCmd_LUA(uiType, typeof(BaseUI), listener, isCloseOthers, createCanCall, param));
                //CreateUI(uiName,type,listener);
            }
            else
            {
                _listCmd.Add(Command.ShowCmd_LUA(uiType, listener, isCloseOthers, createCanCall, param));
            }
        }

        private void _ShowUI(Command cmd)
        {
            BaseUI ui = null;
            _dicUI.TryGetValue(cmd.UIType, out ui);
            if (ui != null)
            {
                ui.Show(cmd.Params);

                _curUI = ui;
                if (ui.mUIStyle == E_UIStyle.BackClose)
                    _backSequence.Push(ui);

                if (cmd.Listener != null)
                {
                    cmd.Listener(ui);
                }

                if (cmd.lua_fun != null)
                {
                    cmd.lua_fun.Call(ui.gameObject);
                }
            }
        }

        public void HideUI(E_UIType uiType)
        {
            _listCmd.Add(Command.HideCmd(uiType));
        }

        private void _HideUI(Command cmd)
        {
            BaseUI ui = null;
            _dicUI.TryGetValue(cmd.UIType, out ui);
            if (ui != null)
            {
                ui.Hide();
            }
        }
        #endregion

        #region 删除UI
        public void DestroyUI(E_UIType uiType)
        {
            _listCmd.Add(Command.DestroyCmd(uiType));
        }

        private void _DestroyUI(Command cmd)
        {
            BaseUI ui = null;
            _dicUI.TryGetValue(cmd.UIType, out ui);
            if (ui != null)
            {
                _dicUI.Remove(cmd.UIType);

                ui.Release();
            }
        }
        #endregion

        private void AddUI(BaseUI ui)
        {
            E_UIType uiType = ui.GetUIType();

            if (_dicUI.ContainsKey(uiType))
                _dicUI[uiType] = ui;
            else
                _dicUI.Add(uiType, ui);
        }

        private void RemoveUI(E_UIType uiType)
        {
            if (_dicUI.ContainsKey(uiType))
            {
                _dicUI[uiType].Release();
                _dicUI.Remove(uiType);
            }
        }

        private void RemoveUI(BaseUI ui)
        {
            if (ui != null && _dicUI.ContainsKey(ui.GetUIType()))
            {
                _dicUI.Remove(ui.GetUIType());

                ui.Release();
            }
        }


        // Update is called once per frame
        public void Update()
        {

			if (_listCmd !=null && _listCmd.Count > 0)
            {
                Command _cmdCurrent = null;

                _cmdCurrent = _listCmd[0];

                if (_cmdCurrent != null)
                {
                    switch (_cmdCurrent.CommandType)
                    {
                        case Command.CmdType.Create:
                            _Create(_cmdCurrent);
                            break;
                        case Command.CmdType.Hide:
                            _HideUI(_cmdCurrent);
                            break;
                        case Command.CmdType.Show:
                            _ShowUI(_cmdCurrent);
                            break;
                        case Command.CmdType.Destroy:
                            _DestroyUI(_cmdCurrent);
                            break;
                        case Command.CmdType.CreateAndShow:
                            _Create(_cmdCurrent);
                            break;
                        default:
                            break;
                    }
                }

                _listCmd.RemoveAt(0);
            }
        }

        /// <summary>
        /// 设置ngui的uipanel
        /// </summary>
        /// <param name="ui">User interface.</param>
        /// <param name="isShow">If set to <c>true</c> is show.</param>
        public void SetPanelDepth(BaseUI ui, bool isShow)
        {
            UIPanel uiPanel = null;
            if (isShow)
            {
                _layerCurrentIndex++;
                uiPanel = ui.CachedTransform.GetOrAddComponent<UIPanel>();
                uiPanel.depth = _layerCurrentIndex;
                if (ui.mUIType == E_UIType.UILoadingPanel)
                    uiPanel.depth = 99;
            }
            else
            {

                uiPanel = ui.CachedTransform.GetComponent<UIPanel>();
                if (uiPanel != null)
                {
                    if (uiPanel.depth != 0 && uiPanel.depth == _layerCurrentIndex)
                    {
                        //最上层的关闭或者隐藏了
                        _layerCurrentIndex--;
                    }
                }
            }
        }

        public void OnTopReturn()
        {
            if (_backSequence.Count != 0)
            {
                BaseUI ui = _backSequence.Peek();
                if (ui != null && ui.mUIStyle == E_UIStyle.BackClose)
                {
                    DestroyUI(ui.GetUIType());
                    _backSequence.Pop();
                }
            }
        }

        /// <summary>
        /// 窗口背景碰撞体处理
        /// </summary>
        private void AddColliderBgForWindow(BaseUI baseUI)
        {
            if (baseUI.mUIStyle == E_UIStyle.PopUp)//baseUI.mUIStyle == E_UIStyle.BackClose || 
            {
                GameUtility.AddColliderBgToTarget(baseUI.gameObject);
            }
        }

        /// <summary>
        /// MessageBox弹出
        /// </summary>
        /// 
        public void ShowMessageBox(string msg)
        {
            BaseUI msgWindow = GetUIByType(E_UIType.PanelMessageBox);
            if (msgWindow != null)
            {
                ((UIMessageBox)msgWindow).SetMsg(msg);
                ((UIMessageBox)msgWindow).ResetWindow();
                ShowUI(E_UIType.PanelMessageBox, typeof(UIMessageBox), null);
            }
        }

        public void ShowMessageBox(string msg, string centerStr, UIEventListener.VoidDelegate callBack)
        {
            BaseUI msgWindow = GetUIByType(E_UIType.PanelMessageBox);
            if (msgWindow != null)
            {
                UIMessageBox messageBoxWindow = ((UIMessageBox)msgWindow);
                ((UIMessageBox)msgWindow).ResetWindow();
                messageBoxWindow.SetMsg(msg);
                messageBoxWindow.SetCenterBtnCallBack(centerStr, callBack);
                ShowUI(E_UIType.PanelMessageBox, typeof(UIMessageBox), null);
            }
        }

        public void ShowMessageBox(string msg, string leftStr, UIEventListener.VoidDelegate leftCallBack, string rightStr, UIEventListener.VoidDelegate rightCallBack)
        {
            BaseUI msgWindow = GetUIByType(E_UIType.PanelMessageBox);
            if (msgWindow != null)
            {
                UIMessageBox messageBoxWindow = ((UIMessageBox)msgWindow);
                ((UIMessageBox)msgWindow).ResetWindow();
                messageBoxWindow.SetMsg(msg);
                messageBoxWindow.SetRightBtnCallBack(rightStr, rightCallBack);
                messageBoxWindow.SetLeftBtnCallBack(leftStr, leftCallBack);
                ShowUI(E_UIType.PanelMessageBox, typeof(UIMessageBox), null);
            }
        }

        public void CloseMessageBox()
        {
            HideUI(E_UIType.PanelMessageBox);
        }

        public void ChangeLevel(int i)
        {
            LoadingManager mgr = AppFacade.Instance.GetManager<LoadingManager>(ManagerName.Loading);
            AsyncOperation op = SceneManager.LoadSceneAsync(i);
            if (mgr != null)
                mgr.CurLoadingScene = op;
        }

        public string GetInput()
        {
            PlayerPrefs.SetString("PlayerID", GameObject.FindObjectOfType<UIInput>().value);

            return GameObject.FindObjectOfType<UIInput>().value;
        }

        /// <summary>
        /// 本方法已经弃用，在Lua中写了
        /// </summary>
        /// <returns></returns>
        public string GetNamingInput()
        {
            return GameObject.FindObjectOfType<UIInput>().value;
        }
    }

    /// <summary>
    /// 操作UI指令集
    /// </summary>
    public class Command
    {
        public enum CmdType
        {
            /// <summary>
            /// 创建并显示
            /// </summary>
            CreateAndShow,
            /// <summary>
            /// 创建
            /// </summary>
            Create,
            /// <summary>
            /// 显示
            /// </summary>
            Show,
            /// <summary>
            /// 隐藏
            /// </summary>
            Hide,
            /// <summary>
            /// 销毁
            /// </summary>
            Destroy,
        }

        /// <summary>
        /// ui类型
        /// </summary>
        public E_UIType UIType;
        /// <summary>
        /// mono脚本类型
        /// </summary>
        public System.Type MonoType;
        /// <summary>
        /// 加载回调
        /// </summary>
        public System.Action<BaseUI> Listener;
        /// <summary>
		/// 是否lua脚本控制
		/// </summary>
		public bool bLuaBehaviour;
        /// <summary>
		/// lua回调
		/// </summary>
		public LuaFunction lua_fun;
        /// <summary>
        /// 界面参数
        /// </summary>
        public object[] Params;
        /// <summary>
        /// 命令类型
        /// </summary>
        public CmdType CommandType;
        /// <summary>
        /// 是否创建时回调
        /// </summary>
        public bool CreateCanCall = true;
        /// <summary>
        /// 是否关闭其他UI
        /// </summary>
        public bool IsCloseOtherUIs = false;

        public static Command CreateAndShowCmd(E_UIType uiType, System.Type type, System.Action<BaseUI> listener, bool isCloseOtherUIs, bool createCanCall, params object[] param)
        {
            Command cmd = new Command(Command.CmdType.CreateAndShow, uiType, param);
            cmd.Listener = listener;
            cmd.MonoType = type;
            cmd.CreateCanCall = createCanCall;
            cmd.IsCloseOtherUIs = isCloseOtherUIs;
            cmd.bLuaBehaviour = false;
            return cmd;
        }

        public static Command CreateAndShowCmd_LUA(E_UIType uiType, System.Type type, LuaFunction lua_fun, bool isCloseOtherUIs, bool createCanCall, params object[] param)
        {
            Command cmd = new Command(Command.CmdType.CreateAndShow, uiType, param);
            cmd.lua_fun = lua_fun;
            cmd.MonoType = type;
            cmd.CreateCanCall = createCanCall;
            cmd.IsCloseOtherUIs = isCloseOtherUIs;
            cmd.bLuaBehaviour = true;
            return cmd;
        }

        public static Command ShowCmd(E_UIType _uiType, System.Action<BaseUI> listener, bool _isCloseOtherUIs, bool _createCanCall, params object[] _param)
        {
            Command cmd = new Command(Command.CmdType.Show, _uiType, _param);
            cmd.CreateCanCall = _createCanCall;
            cmd.Listener = listener;
            cmd.IsCloseOtherUIs = _isCloseOtherUIs;

            return cmd;
        }

        public static Command ShowCmd_LUA(E_UIType _uiType, LuaFunction lua_fun, bool _isCloseOtherUIs, bool _createCanCall, params object[] _param)
        {
            Command cmd = new Command(Command.CmdType.Show, _uiType, _param);
            cmd.CreateCanCall = _createCanCall;
            cmd.lua_fun = lua_fun;
            cmd.IsCloseOtherUIs = _isCloseOtherUIs;

            return cmd;
        }

        public static Command CreateCmd(System.Type _type, E_UIType _uiType, System.Action<BaseUI> _listener)
        {
            Command cmd = new Command(Command.CmdType.Create, _uiType, _type, _listener);

            return cmd;
        }

        public static Command CreateCmd_LUA(System.Type _type, E_UIType _uiType, LuaFunction _lua_fun)
        {
            Command cmd = new Command(Command.CmdType.Create, _uiType, _type, _lua_fun);

            return cmd;
        }

        public static Command HideCmd(E_UIType _uiType)
        {
            Command cmd = new Command(Command.CmdType.Hide, _uiType);

            return cmd;
        }

        public static Command DestroyCmd(E_UIType _uiType)
        {
            Command cmd = new Command(Command.CmdType.Destroy, _uiType);

            return cmd;
        }


        private Command(CmdType _cmdType, E_UIType _uiType, params object[] _params)
        {
            CommandType = _cmdType;
            UIType = _uiType;
            Params = _params;
        }

        private Command(CmdType _cmdType, E_UIType _uiType, System.Type _type, System.Action<BaseUI> _listener)
        {
            CommandType = _cmdType;
            UIType = _uiType;
            MonoType = _type;
            Listener = _listener;
            bLuaBehaviour = false;
        }

        private Command(CmdType _cmdType, E_UIType _uiType, System.Type _type, LuaFunction _lua_fun)
        {
            CommandType = _cmdType;
            UIType = _uiType;
            MonoType = _type;
            lua_fun = _lua_fun;
            bLuaBehaviour = true;
        }

    }
}