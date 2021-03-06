using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using System.Reflection;
using System.IO;

namespace MyFrameWork
{
    public class GameManager : Manager
    {
        protected static bool initialize = false;
        private List<string> downloadFiles = new List<string>();
        private bool _isServerMatched = false;
        private bool _isLoadMatchUIShowed = false;

        /// <summary>
        /// 初始化游戏管理器
        /// </summary>
        void Awake()
        {
            //dont destroy
            GameObject uiRoot = GameObject.Find("UI Root");
            DontDestroyOnLoad(uiRoot);
            DontDestroyOnLoad(this.gameObject);

            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;

            //启动manager
            APPMonoController.Instance.StartUpdate();

            //配置文件
            //ConfigDataMgr.Instance.LoadConfigData();

            //加载module
            ModuleMgr.Instance.RegisterAllModules();

            EventDispatcher.AddListener("OpponentReady", OnServerMatched);

            EventDispatcher.AddListener("MatchUIReady", OnMatchUIShowed);

            EventDispatcher.AddListener("MatchUICanceled", OnMatchUICanceled);

            EventDispatcher.AddListener("DelMatchedInfo", OnDelMatchedInfo);

        }

        /// <summary>
        /// 服务器回调Lua -> 指引到这个方法
        /// </summary>
        public void OnFindOpponentCBK()
        {
            EventDispatcher.TriggerEvent("OpponentReady");
        }
        private void OnServerMatched()
        {
            _isServerMatched = true;

            LoadFightScene();
        }

        private void OnMatchUIShowed()
        {
            _isLoadMatchUIShowed = true;

            LoadFightScene();
        }

        private void OnMatchUICanceled()
        {
            _isLoadMatchUIShowed = false;

        }


        void Start()
        {
#if FPS
			ResourceMgr.Instance.LoadAssetAndInstance("Advanced FPS Counter",null);
#endif
        }

        void Update()
        {
            BundleResMgr.Instance.Update();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            DontDestroyOnLoad(gameObject);  //防止销毁自己

            CheckExtractResource(); //释放资源
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
//            Application.targetFrameRate = AppConst.GameFrameRate;

            //dont destroy
            GameObject uiRoot = GameObject.Find("UI Root");
            DontDestroyOnLoad(uiRoot);
            DontDestroyOnLoad(this.gameObject);

            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;

            //启动manager
            APPMonoController.Instance.StartUpdate();

            //配置文件
            //			ConfigDataMgr.Instance.LoadConfigData();

        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void CheckExtractResource()
        {
            bool isExists = Directory.Exists(Util.DataPath) &&
              Directory.Exists(Util.DataPath + "lua/") && File.Exists(Util.DataPath + "files.txt");
            if(Application.isMobilePlatform == false || AppConst.UpdateMode == true)
            {
                if (isExists || AppConst.DebugMode)
                {
                    StartCoroutine(OnUpdateResource());
                    return;   //文件已经解压过了，自己可添加检查文件列表逻辑
                }
            }
            StartCoroutine(OnExtractResource());    //启动释放协成 
        }

        IEnumerator OnExtractResource()
        {
            string dataPath = Util.DataPath;  //数据目录
            string resPath = Util.AppContentPath(); //游戏包资源目录

            if (Directory.Exists(dataPath)) Directory.Delete(dataPath, true);
            Directory.CreateDirectory(dataPath);

            string infile = resPath + "files.txt";
            string outfile = dataPath + "files.txt";
            if (File.Exists(outfile)) File.Delete(outfile);

            string message = "正在解包文件:>files.txt";
            Debug.Log(message);
            //facade.SendMessageCommand(NotiConst.UPDATE_MESSAGE, message);

            if (Application.platform == RuntimePlatform.Android)
            {
                WWW www = new WWW(infile);
                yield return www;

                if (www.isDone)
                {
                    File.WriteAllBytes(outfile, www.bytes);
                }
                yield return 0;
            }
            else File.Copy(infile, outfile, true);
            yield return new WaitForEndOfFrame();
            Debug.Log("Copy Files.txt to Util.dataPath!!!!!");
            //释放所有文件到数据目录
            string[] files = File.ReadAllLines(outfile);
            Debug.Log("File.txt记录文件个数为:" + files.Length.ToString());
            int index = 0;
            foreach (var file in files)
            {
                index++;
                string[] fs = file.Split('|');
                infile = resPath + fs[0];  //
                outfile = dataPath + fs[0];

                message = "正在解包文件:>" + fs[0];
                Debug.Log("正在解包文件:>" + infile);
                //facade.SendMessageCommand(NotiConst.UPDATE_MESSAGE, message);

                string dir = Path.GetDirectoryName(outfile);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                if (Application.platform == RuntimePlatform.Android)
                {
                    WWW www = new WWW(infile);
                    yield return www;

                    if (www.isDone)
                    {
                        float per = (float)index / (float)files.Length;
                        AppFacade.Instance.GetManager<LoadingManager>(ManagerName.Loading).SetUpdateProgress(per);
                        File.WriteAllBytes(outfile, www.bytes);
                    }
                    yield return 0;
                }
                else {
                    if (File.Exists(outfile))
                    {
                        File.Delete(outfile);
                    }
                    File.Copy(infile, outfile, true);
                    Debug.Log("[Now Copy]:" + infile + "[TO]" + outfile);
                    float per = (float)index / (float)files.Length;
                    AppFacade.Instance.GetManager<LoadingManager>(ManagerName.Loading).SetUpdateProgress(per);
                }
                yield return new WaitForEndOfFrame();
            }
            message = "解包完成!!!";
            Debug.Log(message);
            //facade.SendMessageCommand(NotiConst.UPDATE_MESSAGE, message);
            yield return new WaitForSeconds(0.1f);
            message = string.Empty;

            //释放完成，开始启动更新资源
            StartCoroutine(OnUpdateResource());
        }

        /// <summary>
        /// 启动更新下载，这里只是个思路演示，此处可启动线程下载更新
        /// </summary>
        IEnumerator OnUpdateResource()
        {
            downloadFiles.Clear();

            if (!AppConst.UpdateMode  || Application.platform == RuntimePlatform.OSXEditor)//|| Application.platform == RuntimePlatform.WindowsEditor
            {
                ResManager.Init();
                OnResourceInited();
                yield break;
            }
            string dataPath = Util.DataPath;  //数据目录
            string url = Util.WebURL;
            string random = DateTime.Now.ToString("yyyymmddhhmmss");
            string listUrl = url + "files.txt?v=" + random;
            Debug.LogWarning("LoadUpdate---->>>" + listUrl);

            WWW www = new WWW(listUrl); yield return www;
            if (www.error != null)
            {
                Debug.Log("DownLoad Error:" + www.error);
                OnUpdateFailed(string.Empty);
                yield break;
            }
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
            File.WriteAllBytes(dataPath + "files.txt", www.bytes);

            string filesText = www.text;
            string[] files = filesText.Split('\n');

            string message = string.Empty;
            for (int i = 0; i < files.Length; i++)
            {
                if (string.IsNullOrEmpty(files[i])) continue;
                string[] keyValue = files[i].Split('|');
                string f = keyValue[0];
                string localfile = (dataPath + f).Trim();
                string path = Path.GetDirectoryName(localfile);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileUrl = url + keyValue[0] + "?v=" + random;
                bool canUpdate = !File.Exists(localfile);
                if (!canUpdate)
                {
                    string remoteMd5 = keyValue[1].Trim();
                    string localMd5 = Util.md5file(localfile);
                    canUpdate = !remoteMd5.Equals(localMd5);
                    if (canUpdate) File.Delete(localfile);
                    float per = (float)i / (float)files.Length;
                    AppFacade.Instance.GetManager<LoadingManager>(ManagerName.Loading).SetUpdateProgress(per);
                    Debug.Log("DownLoad Info: No Need DownLoad!");
                }
                if (canUpdate)
                {   //本地缺少文件
                    Debug.Log(fileUrl);
                    message = "downloading>>" + fileUrl;
                    //facade.SendMessageCommand(NotiConst.UPDATE_MESSAGE, message);
                    //                     www = new WWW(fileUrl); yield return www;
                    //                     if (www.error != null) {
                    //                         OnUpdateFailed(path);   //
                    //                         yield break;
                    //                     }
                    //                     File.WriteAllBytes(localfile, www.bytes);
                    //这里都是资源文件，用线程下载
                    BeginDownload(fileUrl, localfile);
                    while (!(IsDownOK(localfile)))
                    {
                        yield return new WaitForEndOfFrame();
                    }

                    Debug.Log(fileUrl + "DownLoad Over!!!");
                    float per = (float)i / (float)files.Length;
                    AppFacade.Instance.GetManager<LoadingManager>(ManagerName.Loading).SetUpdateProgress(per);
                }
            }
            yield return new WaitForEndOfFrame();
            message = "更新完成!!";
            Debug.Log(message);
            //facade.SendMessageCommand(NotiConst.UPDATE_MESSAGE, message);

            ResManager.Init();
            OnResourceInited();
        }

        /// <summary>
        /// 是否下载完成
        /// </summary>
        bool IsDownOK(string file)
        {
            return downloadFiles.Contains(file);
        }

        /// <summary>
        /// 线程下载
        /// </summary>
        void BeginDownload(string url, string file)
        {     //线程下载
            object[] param = new object[2] { url, file };

            ThreadEvent ev = new ThreadEvent();
            ev.Key = NotiConst.UPDATE_DOWNLOAD;
            ev.evParams.AddRange(param);
            ThreadManager.AddEvent(ev, OnThreadCompleted);   //线程下载
        }

        /// <summary>
        /// 线程完成
        /// </summary>
        /// <param name="data"></param>
        void OnThreadCompleted(NotiData data)
        {
            switch (data.evName)
            {
                case NotiConst.UPDATE_EXTRACT:  //解压一个完成
                                                //
                    break;
                case NotiConst.UPDATE_DOWNLOAD: //下载一个完成
                    downloadFiles.Add(data.evParam.ToString());
                    break;
            }
        }

        void OnUpdateFailed(string file)
        {
            string message = "更新失败!>" + file;
            //facade.SendMessageCommand(NotiConst.UPDATE_MESSAGE, message);
        }

        /// <summary>
        /// 资源初始化结束
        /// </summary>
        public void OnResourceInited()
        {
            LuaManager.InitStart();
            LuaManager.DoFile("Logic/Game");            //加载游戏
            LuaManager.DoFile("Logic/Network");         //加载网络                     //初始化网络

			if(GameEntrance.Instance.IsTestFight)
			{
				StartCoroutine(ToFightScene());

				//加载表格
				AppFacade.Instance.GetManager<TutorialManager>(ManagerName.Tutorial).LoadTutorialTable();
				initialize = true;                          //初始化完 
			}
			else
			{
				MyHttp.Instance.RequestVerInfo(OnVerInfoLoaded);
			}
        }

		private void OnVerInfoLoaded(bool loadOk)
		{
			if(loadOk)
			{
				Util.CallMethod("Game", "OnInitOK");          //初始化完成
				NetManager.OnInit();    //初始化网络

				//加载表格
				AppFacade.Instance.GetManager<TutorialManager>(ManagerName.Tutorial).LoadTutorialTable();
				initialize = true;                          //初始化完 
			}
		}




        public void LoadFightScene()
        {
            if (!_isLoadMatchUIShowed || !_isServerMatched)
                return;
            else
            {
                _isLoadMatchUIShowed = false;

                _isServerMatched = false;
            }


            UIMatching panel = AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI).GetUIByType(E_UIType.PanelMatching) as UIMatching; //UIMgr.Instance.GetUIByType

            if (panel == null)
            {
                DebugUtil.Error("uimatching is null");
                return;
            }
            panel.FindOpponent();

            UIMgr.Instance.DestroyUI(E_UIType.UIMainPanel);

            APPMonoController.Instance.StartCoroutine(ToFightScene());

        }
        

        private IEnumerator ToFightScene()
        {
            yield return new WaitForSeconds(3f);
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)SenceName.SENCE_FIGHT);
            FightNew.FightMgr.Instance.LoadFightScene();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        void OnDestroy()
        {
            EventDispatcher.RemoveListener("OpponentReady", OnServerMatched);
            EventDispatcher.RemoveListener("MatchUIReady", OnMatchUIShowed);
            EventDispatcher.RemoveListener("MatchUICanceled", OnMatchUICanceled);
            EventDispatcher.RemoveListener("DelMatchedInfo", OnDelMatchedInfo);

            if (NetManager != null)
            {
                NetManager.Unload();
            }
            if (LuaManager != null)
            {
                LuaManager.Close();
            }
            
			DebugUtil.Info("~GameManager was destroyed");
        }

        /// <summary>
        /// Lua脚本中调用这个方法。获取上次输入的用户ID
        /// </summary>
        /// <returns></returns>
        public string GetLastInputID()
        {
            return PlayerPrefs.GetString("PlayerID", "");
        }

        public void OnDelMatchedInfo()
        {
            _isServerMatched = false;

            Debug.Log("_isServerMatched: " + _isServerMatched);
        }
    }
}