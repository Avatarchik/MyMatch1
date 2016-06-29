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

namespace FightNew
{
	public class SlotTeleport : MonoBehaviour
	{
//		public void Reset()
//		{
//			this.slot = null;
//		}
//
		public Slot slot;//{get;set;}

		public Slot TargetSlot;
//
		public int TargetID = 0;
//
		float lastTime = -10;
		float delay = 0.15f; // delay between the generations
//
//
		void  Start ()
		{
			slot = GetComponent<Slot>();
			slot.slotTeleport = this;
		}
//
//
		public void Initialize () 
		{
			if (!enabled) return;
			int2 position = ConvertIDtoPosition (TargetID);

			TargetSlot = FightMgr.Instance.FindSlot(position.x,position.y);
			if (TargetSlot != null) 
			{
				TargetSlot.IsTeleportTarget = true;

				FightMgr.Instance.AllSlotTeleport.Add(this);
			} 
			else 
			{
				Destroy(this);
			}
		}

		public bool OnUpdate ()
		{
			if (!TargetSlot) return false; // Teleport is possible only if target is exist

			if (!FightMgr.Instance.CanIGravity ()) return false; // Teleport is possible only in case of mode "gravity"

			if (!slot.GetChip()) return false; // Teleport is possible only if slot contains chip

			if (!slot.GetChip().can_move) return false; // If chip can't be moved, then it can't be teleported

			if (TargetSlot.GetChip()) return false; // Teleport is impossible if target slot already contains chip

			if (slot.Block) return false; // Teleport is impossible, if the slot is blocked
			if (TargetSlot.Block) return false; // Teleport is impossible, if the target slot is blocked

			if (slot.GetChip().IsDrop) return true;//.transform.position != slot.transform.position) return;

			if(slot.GetChip().hitting) return false;

			if (lastTime + delay > Time.time) return true; // limit of frequency generation

			lastTime = Time.time;

			AnimationMgr.Instance.TeleportChip(slot.GetChip (), TargetSlot);

			return true;
		}
//
		public static int2 ConvertIDtoPosition(int teleportID) 
		{
			int2 position;
			position.y = Mathf.FloorToInt (1f * (teleportID - 1) / 12);
			position.x = teleportID - 1 - position.y * 12;
			position.y = Fight.LevelProfile.NewCurrentLevelProfile.Height - position.y - 1;
			return position;
		}
	}
}
