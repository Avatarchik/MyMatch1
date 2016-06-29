/*
 * 
 * 文件名(File Name)：             SlotTeleport
 *
 * 作者(Author)：                  Yangzj
 *
 * 创建时间(CreateTime):           2016/04/01 15:06:09
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace Fight
{
//	public class SlotTeleportManager : Singleton<SlotTeleportManager>
//	{
//		private Queue<SlotTeleport> _queue;
//		private List<SlotTeleport> _list;
//		public List<SlotTeleport> List
//		{
//			get
//			{
//				return _list;
//			}
//		}
//
//		public override void Init()
//		{
//			_queue = new Queue<SlotTeleport>();
//			_list = new List<SlotTeleport>();
//		}
//
//		protected override void OnReleaseValue()
//		{
//			_queue.Clear();
//			_list.Clear();
//		}
//
//		protected override void OnAppQuit()
//		{
//			_queue = null;
//			_list = null;
//		}
//
//		public SlotTeleport GetOne(Slot slot)
//		{
//			SlotTeleport slotGenerator = null;
//
//			if(_queue.Count > 0)
//			{
//				slotGenerator = _queue.Dequeue();
//				slotGenerator.Slot = slot;
//			}
//			else
//			{
//				slotGenerator = new SlotTeleport(slot);
//			}
//
//			return slotGenerator;
//		}
//
//		public void Return(SlotTeleport slotGenerator)
//		{
//			slotGenerator.Reset();
//
//			if(_list.Contains(slotGenerator))
//				_list.Remove(slotGenerator);
//
//			_queue.Enqueue(slotGenerator);
//		}
//
//		public void ReturnAll()
//		{
//			SlotTeleport[] arr = _list.ToArray();
//			for(int i = 0;i < arr.Length;i++)
//			{
//				Return(arr[i]);
//			}
//		}
//	}

	public class SlotTeleport : MonoBehaviour
	{
		public void Reset()
		{
			this.slot = null;
		}

		public Slot slot;//{get;set;}

		public SlotTeleport(Slot ownerSlot)
		{
			this.slot = ownerSlot;
		}
			
		public Slot TargetSlot;

		public int TargetID = 0;

		float lastTime = -10;
		float delay = 0.15f; // delay between the generations


		void  Start ()
		{
			slot = GetComponent<Slot>();
			slot.slotTeleport = this;
		}


		public void Initialize () 
		{
			if (!enabled) return;
			int2 position = ConvertIDtoPosition (TargetID);

			TargetSlot = SlotManager.Instance.FindSlot(position.x,position.y);
			if (TargetSlot != null) 
			{
				TargetSlot.IsTeleportTarget = true;
			} 
			else 
			{
				Destroy(this);
//				DebugUtil.Error("not found slot:" + position.ToString());
			}
		}

		void  Update ()
		{
			if (!TargetSlot) return; // Teleport is possible only if target is exist

			if (!SessionControl.Instance.CanIGravity ()) return; // Teleport is possible only in case of mode "gravity"

			if (!slot.GetChip()) return; // Teleport is possible only if slot contains chip

			if (!slot.GetChip().can_move) return; // If chip can't be moved, then it can't be teleported

			if (TargetSlot.GetChip()) return; // Teleport is impossible if target slot already contains chip

			if (slot.Block) return; // Teleport is impossible, if the slot is blocked
			if (TargetSlot.Block) return; // Teleport is impossible, if the target slot is blocked

			if (slot.GetChip().transform.position != slot.transform.position) return;

			if (lastTime + delay > Time.time) return; // limit of frequency generation
			lastTime = Time.time;

			AnimationControl.Instance.TeleportChip (slot.GetChip (), TargetSlot);
		}

		public static int2 ConvertIDtoPosition(int teleportID) 
		{
			int2 position;
			position.y = Mathf.FloorToInt (1f * (teleportID - 1) / 12);
			position.x = teleportID - 1 - position.y * 12;
			position.y = LevelProfile.CurrentLevelProfile.Height - position.y - 1;
			return position;
		}
	}
}
