using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using MyFrameWork;

namespace MyFrameWork
{

    enum Result
    {
        OK = 0,
        ERROR = 1,

        FIGHT_WIN = 101,
        FIGHT_LOSE = 102,
        FIGHT_DRAW = 103,
    }
    public class NetworkManager : Manager
    {
		public static NetworkManager Instance
		{
			get
			{
				return AppFacade.Instance.GetManager<NetworkManager>(ManagerName.Network);
			}
		}

        private SocketClient socket;
        static Queue<KeyValuePair<int, ByteBuffer>> sEvents = new Queue<KeyValuePair<int, ByteBuffer>>();
        bool bStartHeartBeat = false;
        private SimpleTimer BadNetTimer = new SimpleTimer(10f);
        private SimpleTimer ReLoginTimer = new SimpleTimer(20f);

        SocketClient SocketClient
        {
            get
            {
                if (socket == null)
                    socket = new SocketClient();
                return socket;
            }
        }

        void Awake()
        {
            Init();
        }

        void Init()
        {
            SocketClient.OnRegister();
        }

        public void StartHeartBeat()
        {
            bStartHeartBeat = true;
            BadNetTimer.tick += BadNetCondition;
            BadNetTimer.Restart();
            ReLoginTimer.tick += ReturnToLogin;
            ReLoginTimer.Restart();
        }

        public void StopHeartBeat(GameObject obj)
        {
            bStartHeartBeat = false;
            BadNetTimer.tick -= BadNetCondition;
            BadNetTimer.Stop();
            ReLoginTimer.tick -= ReturnToLogin;
            ReLoginTimer.Stop();
            FightNew.FightMgr.Instance.ClearLevel();
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
            SendReConnect();
            SetReConnect(true);
        }

        public void ReceiveHeartBeat()
        {
            ReLoginTimer.Restart();
            BadNetTimer.Restart();
            AppFacade.Instance.GetManager<NetConditionManager>(ManagerName.NetCondition).HideNetCondition();
            Util.CallMethod("Network", "ClientHeartBeat");
        }

        public void BadNetCondition()
        {
            AppFacade.Instance.GetManager<NetConditionManager>(ManagerName.NetCondition).ShowNetCondition();
        }

        public void ReturnToLogin()
        {
            UIMgr.Instance.ShowMessageBox("网络连接超时", "确定", StopHeartBeat);
        }

        public void OnInit()
        {
            CallMethod("Start");
        }

        public void Unload()
        {
            CallMethod("Unload");
        }

        /// <summary>
        /// ִ��Lua����
        /// </summary>
        public object[] CallMethod(string func, params object[] args)
        {
            return Util.CallMethod("Network", func, args);
        }

        ///------------------------------------------------------------------------------------
        public static void AddEvent(int _event, ByteBuffer data)
        {
            sEvents.Enqueue(new KeyValuePair<int, ByteBuffer>(_event, data));
        }

        public  void Execute(object data)
        {
            if (data == null) return;
            KeyValuePair<int, ByteBuffer> buffer = (KeyValuePair<int, ByteBuffer>)data;
            switch (buffer.Key)
            {
                default: Util.CallMethod("Network", "OnSocket", buffer.Key, buffer.Value); break;
            }
        }

        /// <summary>
        /// ����Command�����ﲻ�����ķ���˭��
        /// </summary>
        void Update()
        {
            if (sEvents.Count > 0)
            {
                while (sEvents.Count > 0)
                {
                    KeyValuePair<int, ByteBuffer> _event = sEvents.Dequeue();
                    Execute(_event);
                }
            }

            if(bStartHeartBeat)
            {
                if (BadNetTimer != null)
                    BadNetTimer.Update(Time.deltaTime);

                if (ReLoginTimer != null)
                    ReLoginTimer.Update(Time.deltaTime);
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public void SendConnect()
        {
            SocketClient.SendConnect();
        }

        public void SendReConnect()
        {
            SocketClient.SendReConnect();
        }

        public void SetReConnect(bool bNeed)
        {
            SocketClient.bNeedReConnect = bNeed;
        }

        /// <summary>
        /// ����SOCKET��Ϣ
        /// </summary>
        public void SendMessage(ByteBuffer buffer)
        {
            SocketClient.SendMessage(buffer);
        }

        /// <summary>
        /// ��������
        /// </summary>
        void OnDestroy()
        {
            SocketClient.OnRemove();
			DebugUtil.Info("~NetworkManager was destroy");
        }
    }
}