/*
 * 
 * 文件名(File Name)：             SlotGravity
 *
 * 作者(Author)：                  Yangzj
 *
 * 创建时间(CreateTime):           2016/03/21 20:07:47
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace FightNew
{
//	public class SlotGravityManager : Singleton<SlotGravityManager>
//	{
//		private Queue<SlotGravity> _queue;
//		private List<SlotGravity> _list;
//		public List<SlotGravity> List
//		{
//			get
//			{
//				return _list;
//			}
//		}
//
//		public override void Init()
//		{
//			_queue = new Queue<SlotGravity>();
//			_list = new List<SlotGravity>();
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
//		public SlotGravity GetOne(Slot slot,Side gravityDirection)
//		{
//			SlotGravity slotGravity = null;
//
//			if(_queue.Count > 0)
//			{
//				slotGravity = _queue.Dequeue();
//				slotGravity.Slot = slot;
//				slotGravity.gravityDirection = gravityDirection;
//			}
//			else
//			{
//				slotGravity = new SlotGravity(slot,gravityDirection);
//			}
//
//			return slotGravity;
//		}
//
//		public void Return(SlotGravity slotGenerator)
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
//			SlotGravity[] arr = _list.ToArray();
//			for(int i = 0;i < arr.Length;i++)
//			{
//				Return(arr[i]);
//			}
//		}
//	}

	[RequireComponent (typeof (Slot))]
//	[RequireComponent (typeof (SlotForCard))]
	public class SlotGravity : MonoBehaviour 
	{
////		public Slot Slot{get;set;}
		public Slot slot;
//		public SlotForCard slotForChip;
		private Card chip;
//
//		public void Reset()
//		{
//			this.slot = null;
//		}
//
		private Side cw45side;
		private Side cw90side;
		private Side ccw45side;
		private Side ccw90side;

		private Side _gravityDirection = Side.Null;
		public Side gravityDirection
		{
			get
			{
				return _gravityDirection;
			}
			set
			{
				_gravityDirection = value;

				cw45side = Utils.RotateSide(_gravityDirection, 1);
				cw90side = Utils.RotateSide(_gravityDirection, 2);
				ccw45side = Utils.RotateSide(gravityDirection, -1);
				ccw90side = Utils.RotateSide(gravityDirection, -2);
			}
		}
		public Side fallingDirection = Side.Null;

		// No shadow - is a direct path from the slot up to the slot with a component SlotGenerator. Towards must have slots (without blocks and wall)
		// This concept is very important for the proper physics chips
		public bool shadow;
//
////		public SlotGravity(Slot slot,Side gravityDirection)
////		{
////			this.slot = slot;
////			this.gravityDirection = gravityDirection;
////		}
//
		void  Awake ()
		{
			slot = GetComponent<Slot>();
//			slotForChip = GetComponent<SlotForCard>(); 
		}
//

		/// <summary>
		/// 默认generator的shadow为true，出生点线路上的置为false
		/// </summary>
		// Update shadows at all slots (for example, after the blocks destruction)
		public static void Reshading () 
		{
			SlotGravity sg = null;
			List<Slot> listSlot = FightMgr.Instance.GetSlotsList();
			Slot slotItem;
			for(int i = 0;i < listSlot.Count;i++)
			{
				slotItem = listSlot[i];

				sg = slotItem.slotGravity;
				if(sg != null)
					sg.shadow = true;
			}

			Slot slot;
			List<Slot> stock = new List<Slot>();
			List<SlotGenerator> generator = FightMgr.Instance.AllSlotGenerator;
			// Gravity shading
			SlotGenerator sgen;
			for (int i = 0;i  < generator.Count;i++) 
			{
				sgen = generator[i];

				if(!sgen.IsInUse) return;

				slot = sgen.Slot;
				stock.Clear();
				while (slot && slot.Block == null && slot.slotGravity.shadow && !stock.Contains(slot)) {
					slot.slotGravity.shadow = false;
					stock.Add(slot);
					slot = slot[slot.slotGravity.gravityDirection];
				}
				sgen.Slot.slotGravity.shadow = false;
			}

		
			if (FightMgr.Instance.AllSlotTeleport.Count > 0) 
			{
//				DebugUtil.Info("SlotTeleport count: " + telePortLen);

				// Teleport shading
				for (int i = 0;i  < generator.Count;i++) 
				{
					sgen = generator[i];

					if(!sgen.IsInUse) continue;

					slot = sgen.Slot;
					stock.Clear(); 
					while (slot && slot.Block == null && !stock.Contains(slot)) 
					{
						slot.slotGravity.shadow = false;
						stock.Add(slot);
						if (slot.slotTeleport) 
							slot = slot.slotTeleport.TargetSlot;
						else
							slot = slot[slot.slotGravity.gravityDirection];
					}

					sgen.Slot.slotGravity.shadow = false;
				}
			}


			//foreach (SlotGravity s in GameObject.FindObjectsOfType<SlotGravity>())
			//    ScoreBubble.Bubbling(s.shadow ? 1 : 0, s.transform, 0);

			//Debug.Break();

		}


//		public void Update()
//		{
//			GravityReaction();
//		}

		private Slot slotGet = null;
		// Gravity iteration
		public bool GravityReaction()
		{
			if (!FightMgr.Instance.CanIGravity()) return false; // Work is possible only in "Gravity" mode



			chip = slot.GetChip();

			if (chip == null) return false; // Work is possible only with the chips, otherwise nothing will move
			if (!chip.can_move) return false;

			if (chip == null) return false;
			if (chip.IsDrop ) return true; // Work is possible only if the chip is physically clearly in the slot
			if(chip.hitting) return false;

//			return false;
			slotGet = slot[gravityDirection];
			if (slotGet == null 
				|| !slotGet.gravity)
				return false; // Work is possible only if there is another bottom slot

			// provided that bottom neighbor doesn't contains chip, give him our chip
			if (slot[gravityDirection].GetChip() == null) 
			{
				slot[gravityDirection].SetChip(chip,true);
//				chip.gameObject.SetActive(true);
				GravityReaction();
				return true;
			} 

			bool rtn = false;
			// Otherwise, we try to give it to their neighbors from the bottom-left and bottom-right side
			if (Random.value > 0.5f) 
			{ // Direction priority is random
				if(SlideLeft())
					rtn = true;
				else if(SlideRight())
					rtn = true;
			} 
			else 
			{
				if(SlideRight())
					rtn = true;
				else if(SlideLeft())
					rtn = true;
			}

			return rtn;
		}

		bool SlideLeft() 
		{
			slotGet = slot[cw45side];
			if (slotGet != null // target slot must exist
				&& slotGet.gravity // target slot must contain gravity
				&& (slotGet.slotGravity == null 
					|| (slotGet.slotGravity != null && (slotGet[slotGet.slotGravity.fallingDirection] == null || slotGet[slotGet.slotGravity.fallingDirection].Block != null || slotGet[slotGet.slotGravity.fallingDirection].GetShadow())))
				&& ((slot[gravityDirection] != null && slot[gravityDirection][cw90side] != null) 
					|| (slot[cw90side] != null && slot[cw90side][gravityDirection] != null)) // target slot should have a no-diagonal path that is either left->down or down->left
				&& slotGet.GetChip() == null // target slot should not have a chip
				&& slotGet.GetShadow() // target slot must have shadow otherwise it will be easier to fill it with a generator on top
				&& !slotGet.GetChipShadow()) 
			{ // target slot should not be shaded by another chip, otherwise it will be easier to fill it with this chip
//				chip.Slot.RemoveCard();
				if(slotGet.slotGravity != null && slotGet[slotGet.slotGravity.fallingDirection] != null && slotGet[slotGet.slotGravity.fallingDirection].Block == null 
					&& (slot[slot.slotGravity.fallingDirection] != null && slot[slot.slotGravity.fallingDirection] != null && slot[slot.slotGravity.fallingDirection].Block == null))
				{
					return false;
				}
				else
				{
					slotGet.SetChip(chip,true); // transfer chip to target slot
	//				chip.gameObject.SetActive(true);
	//				Debug.Log("<color=orange>set chip slideLeft:</color>" + slot[cw45side].name + ",localpos:" + chip.transform.localPosition + ",velocity:" + chip.velocity);
					return true;
				}
			}

			return false;
		}

		bool SlideRight() 
		{
			slotGet = slot[ccw45side];
			if (slotGet != null // target slot must exist
				&& slotGet.gravity // target slot must contain gravity
				&& (slotGet.slotGravity == null 
					|| (slotGet.slotGravity != null && (slotGet[slotGet.slotGravity.fallingDirection] == null || slotGet[slotGet.slotGravity.fallingDirection].Block != null || slotGet[slotGet.slotGravity.fallingDirection].GetShadow())))
				&& ((slot[gravityDirection] != null && slot[gravityDirection][ccw90side] != null) 
					|| (slot[ccw90side] != null && slot[ccw90side][gravityDirection] != null)) // target slot should have a no-diagonal path that is either right->down or down->right
				&& slotGet.GetChip() == null // target slot should not have a chip
				&& slotGet.GetShadow() // target slot must have shadow otherwise it will be easier to fill it with a generator on top
				&& !slotGet.GetChipShadow()) 
			{// target slot should not be shaded by another chip, otherwise it will be easier to fill it with this chip
//				chip.Slot.RemoveCard();
				if(slotGet.slotGravity != null && slotGet[slotGet.slotGravity.fallingDirection] != null && slotGet[slotGet.slotGravity.fallingDirection].Block == null 
					&& (slot[slot.slotGravity.fallingDirection] != null && slot[slot.slotGravity.fallingDirection] != null && slot[slot.slotGravity.fallingDirection].Block == null))
				{
					return false;
				}
				else
				{
					slotGet.SetChip(chip,true); // transfer chip to target slot
//				chip.gameObject.SetActive(true);
//				Debug.Log("<color=orange>set chip slide right:</color>" + slot[ccw45side].name + ",localpos:" + chip.transform.localPosition + ",velocity:" + chip.velocity);
					return true;
				}
			}

			return false;
		}
	}
}
