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

namespace Fight
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
	[RequireComponent (typeof (SlotForCard))]
	public class SlotGravity : MonoBehaviour 
	{
//		public Slot Slot{get;set;}
		public Slot slot;
		public SlotForCard slotForChip;
		public Card chip;

		public void Reset()
		{
			this.slot = null;
		}
			
		public Side gravityDirection = Side.Null;
		public Side fallingDirection = Side.Null;

		// No shadow - is a direct path from the slot up to the slot with a component SlotGenerator. Towards must have slots (without blocks and wall)
		// This concept is very important for the proper physics chips
		public bool shadow;

//		public SlotGravity(Slot slot,Side gravityDirection)
//		{
//			this.slot = slot;
//			this.gravityDirection = gravityDirection;
//		}

		void  Awake ()
		{
			slot = GetComponent<Slot>();
			slotForChip = GetComponent<SlotForCard>(); 
		}

		// Update shadows at all slots (for example, after the blocks destruction)
		public static void  Reshading () 
		{
			SlotGravity sg = null;
			foreach (var slotItem in SlotManager.Instance.DicSlotNow.Values)
			{
				sg = slotItem.slotGravity;

				if(sg != null)
					sg.shadow = true;
			}

			Slot slot;
			List<Slot> stock = new List<Slot>();
			List<SlotGenerator> generator = new List<SlotGenerator>(GameObject.FindObjectsOfType<SlotGenerator>());
			// Gravity shading
			foreach (SlotGenerator sgen in generator) 
			{
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

			int telePortLen = GameObject.FindObjectsOfType<SlotTeleport>().Length;
			if (telePortLen > 0) 
			{
				DebugUtil.Info("SlotTeleport count: " + telePortLen);

				// Teleport shading
				foreach (SlotGenerator sgen in generator) 
				{
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


		public void Update()
		{
			GravityReaction();
		}

		// Gravity iteration
		public void  GravityReaction ()
		{
			if (!SessionControl.Instance.CanIGravity()) return; // Work is possible only in "Gravity" mode


			chip = slotForChip.GetCard();

			if (chip == null) return; // Work is possible only with the chips, otherwise nothing will move
			if (!chip.can_move) return;

			if (chip == null || slot.transform.position != chip.transform.position) return; // Work is possible only if the chip is physically clearly in the slot

			if (slot[gravityDirection] == null 
				|| !slot[gravityDirection].gravity)
				return; // Work is possible only if there is another bottom slot

			// provided that bottom neighbor doesn't contains chip, give him our chip
			if (slot[gravityDirection].GetChip() == null) 
			{
				slot[gravityDirection].SetChipTemp(chip);
				GravityReaction();
				return;
			} 

			// Otherwise, we try to give it to their neighbors from the bottom-left and bottom-right side
			if (Random.value > 0.5f) { // Direction priority is random
				SlideLeft();
				SlideRight();
			} else {
				SlideRight();
				SlideLeft();	
			}
		}

		void SlideLeft() 
		{
			Side cw45side = Utils.RotateSide(gravityDirection, 1);
			Side cw90side = Utils.RotateSide(gravityDirection, 2);

			if (slot[cw45side] != null // target slot must exist
				&& slot[cw45side].gravity // target slot must contain gravity
				&& ((slot[gravityDirection] != null && slot[gravityDirection][cw90side] != null) 
					|| (slot[cw90side] != null && slot[cw90side][gravityDirection] != null)) // target slot should have a no-diagonal path that is either left->down or down->left
				&& slot[cw45side].GetChip() == null // target slot should not have a chip
				&& slot[cw45side].GetShadow() // target slot must have shadow otherwise it will be easier to fill it with a generator on top
				&& !slot[cw45side].GetChipShadow()) { // target slot should not be shaded by another chip, otherwise it will be easier to fill it with this chip
				slot[cw45side].SetChip(chip); // transfer chip to target slot
			}
		}

		void SlideRight() 
		{
			Side ccw45side = Utils.RotateSide(gravityDirection, -1);
			Side ccw90side = Utils.RotateSide(gravityDirection, -2);

			if (slot[ccw45side] != null // target slot must exist
				&& slot[ccw45side].gravity // target slot must contain gravity
				&& ((slot[gravityDirection] != null && slot[gravityDirection][ccw90side] != null) 
					|| (slot[ccw90side] != null && slot[ccw90side][gravityDirection] != null)) // target slot should have a no-diagonal path that is either right->down or down->right
				&& slot[ccw45side].GetChip() == null // target slot should not have a chip
				&& slot[ccw45side].GetShadow() // target slot must have shadow otherwise it will be easier to fill it with a generator on top
				&& !slot[ccw45side].GetChipShadow()) {// target slot should not be shaded by another chip, otherwise it will be easier to fill it with this chip
				slot[ccw45side].SetChip(chip); // transfer chip to target slot
			}
		}
	}
}
