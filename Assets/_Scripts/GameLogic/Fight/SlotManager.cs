/*
 * 
 * 文件名(File Name)：             SlotManager
 *
 * 作者(Author)：                  #AuthorName#
 *
 * 创建时间(CreateTime):           2016/03/29 10:36:03
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace Fight
{
	public class SlotManager : Singleton<SlotManager> 
	{

//		//原来的
//		private Dictionary<string,Slot> _dicSlot;
//		public Dictionary<string,Slot> DicSlot
//		{
//			get
//			{
//				return _dicSlot;
//			}
//		}


		/// <summary>
		/// 界面上的slot
		/// </summary>
		private Dictionary<string,Slot> _dicSlot1;
		private Dictionary<string,Slot> _dicSlot2;
		private Dictionary<string,Slot> _dicSlot3;

		public Dictionary<string,Slot> DicSlotNow
		{
			get
			{
				if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss1
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss1)
				{
					return _dicSlot1;
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss2
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss2)
				{
					return _dicSlot2;
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss3
					|| FightControl.Instance.EFightStatus == E_FightStatus.FightBoss3)
				{
					return _dicSlot3;
				}
				else
				{
					DebugUtil.Error("get slot fail :" + FightControl.Instance.EFightStatus);
					return null;
				}
			}
		}


//		/// <summary>
//		/// 缓存池中的slot
//		/// </summary>
//		private Queue<Slot> _queueSlot;

		#region 临时资源对象
		private GameObject _goInsSlot;
		#endregion

		public override void Init()
		{
			//_dicSlot = new Dictionary<string,Slot>();

			_dicSlot1 = new Dictionary<string,Slot>();
			_dicSlot2 = new Dictionary<string,Slot>();
			_dicSlot3 = new Dictionary<string,Slot>();

//			_queueSlot = new Queue<Slot>();

		}
			
		/// <summary>
		/// 获取一个slot
		/// </summary>
		/// <returns>The one slot.</returns>
		private Slot GetOneSlot()
		{
//			if(_queueSlot.Count > 0)
//			{
//				return _queueSlot.Dequeue();
//			}
				
			//生成一个新的
			GameObject goSlot = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(Slot.SlotEmptyPrefabName);// MonoBehaviour.Instantiate<GameObject>(_goInsSlot);
			Slot slot = goSlot.GetComponent<Slot>();
			return slot;
		}

		public Slot CreateSlot(int row,int col)
		{
			Slot slot = GetOneSlot();
			slot.SetPos(row,col);

			if(DicSlotNow.ContainsKey(slot.key))
			{
				DicSlotNow[slot.key] = slot;
			}
			else
			{
				DicSlotNow.Add(slot.key,slot);
			}
				
			//监听触摸
			TouchControl.Instance.AddTouchControl(slot);

			return slot;

		}

		public Slot FindSlot(int row,int col)
		{
			string key = string.Format(Slot.SlotKeyFormat,row,col);
			return FindSlot(key);
		}

		public Slot FindSlot(string key)
		{
			if(DicSlotNow != null && DicSlotNow.ContainsKey(key))
				return DicSlotNow[key];

			return null;
		}

//		/// <summary>
//		/// 归还一个slot到对象池
//		/// </summary>
//		/// <param name="slot">Slot.</param>
//		public void ReturnSlot(Slot slot)
//		{
//			//移除监听触摸事件
//			TouchControl.Instance.RemoveTouchControl(slot);
//
//			slot.Reset();
//
//			if(_dicSlot.ContainsKey(slot.key))
//				_dicSlot.Remove(slot.key);
//			
//			_queueSlot.Enqueue(slot);
//		}

		private Slot FindNearSlot(int x, int y, Side side) 
		{
			string key = string.Format(Slot.SlotKeyFormat,(x + Utils.SideOffsetX(side)),(y + Utils.SideOffsetY(side)));

			Slot slot = null;
			DicSlotNow.TryGetValue(key,out slot);

			return slot;
		}


		#region 交换
		public void Swap(Slot slot, Side side) 
		{
			if (slot == null || slot.GetChip() == null
				|| slot[side] == null || slot[side].GetChip() == null)
			{
				//没有相邻卡
				return;
			}

			AnimationControl.Instance.SwapTwoItem(slot.GetChip(), slot[side].GetChip(), false);
				
		}
		#endregion

		public void DestroyAllSlot()
		{
		}

		public void Initialize ()
		{
			//			foreach (Slot slot in GameObject.FindObjectsOfType<Slot>())
			//				if (!all.ContainsKey(slot.key))
			//					all.Add(slot.key, slot);

			List<Slot> listSlot = new List<Slot>(DicSlotNow.Values);
			Slot slot = null;
			//foreach (Slot slot in _dicSlot.Values) 
			for(int index = 0; index < listSlot.Count;index++)
			{
				slot = listSlot[index];
				for (int i = 0;i < Utils.allSides.Length;i++)// Filling of the nearby slots dictionary 
				{
					slot.nearSlot.Add(Utils.allSides[i], FindNearSlot(slot.Row, slot.Col, Utils.allSides[i]));
				}
				slot.nearSlot.Add(Side.Null, null);

				for (int i = 0;i < Utils.straightSides.Length;i++) // Filling of the walls dictionary
				{
					slot.wallMask.Add(Utils.straightSides[i], false);
				}
			}

			Side direction;
			SlotTeleport teleport;
			SlotGravity sgTemp;
//			foreach (Slot slot in SlotManager.Instance.DicSlot.Values) 
			for(int index = 0; index < listSlot.Count;index++)
			{	
				slot = listSlot[index];
				sgTemp = slot.slotGravity;
				direction = sgTemp.gravityDirection;
				if (slot[direction] != null) 
				{
					sgTemp = slot[direction].slotGravity;
					//来源取反
					sgTemp.fallingDirection = Utils.MirrorSide(direction);
				}

				teleport =slot.slotTeleport;

				if (teleport != null)
					teleport.Initialize();
			}
		}

		public void Clear()
		{
			_dicSlot1.Clear();
			_dicSlot2.Clear();;
			_dicSlot3.Clear();;
		}
	}
}
