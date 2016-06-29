using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace FightNew
{
	public class MatchCountMgr : DDOLSingleton<MatchCountMgr>
	{
		private Transform _uiContainer;
		public Transform UIContainer
		{
			get
			{
				if(_uiContainer == null)
				{
					_uiContainer = FieldMgr.SlotRoot;
				}

				return _uiContainer;
			}
		}
        Dictionary<E_MatchCountMsg, GameObject> matchDic = new Dictionary<E_MatchCountMsg, GameObject>();
		public void ShowMsg(E_MatchCountMsg msg)
		{
            //����ֵ����У�ֱ��setactive���ֵ�û�У�������
            if (matchDic.ContainsKey(msg))
            {
//                Debug.Log("key already exist in dictionary");

                matchDic[msg].SetActive(true);
            }

            else
            {
//                Debug.Log("creating new");
				string msgName = FightDefine.Prefab_MatchCount_Eleven.ToString();
				if(msg == E_MatchCountMsg.MatchCount_Three)
					msgName = FightDefine.Prefab_MatchCount_Three.ToString();
				else if(msg == E_MatchCountMsg.MatchCount_Five)
					msgName = FightDefine.Prefab_MatchCount_Five.ToString();
				else if(msg == E_MatchCountMsg.MatchCount_Seven)
					msgName = FightDefine.Prefab_MatchCount_Seven.ToString();
				else if(msg == E_MatchCountMsg.MatchCount_Nine)
					msgName = FightDefine.Prefab_MatchCount_Nine.ToString();
			
				
				GameObject go = FightMgr.Instance.LoadAndInstantiate(msgName);
				go.transform.SetParent(UIContainer,false);

                matchDic.Add(msg, go);

                go.SetActive(true);
            }

#if !FightTest
			MusicManager.Instance.PlaySoundEff("Music/music_"+msg);
#endif

        }


        public void Clear()
		{
			if(matchDic != null)
				matchDic.Clear();
		}
	}
}