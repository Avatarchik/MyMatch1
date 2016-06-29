/*
 * 
 * 文件名(File Name)：             Slot
 *
 * 作者(Author)：                  #AuthorName#
 *
 * 创建时间(CreateTime):           2016/03/29 10:41:26
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Fight
{
	public class Slot : MonoBehaviour 
	{
		public const string SlotEmptyPrefabName = "SlotEmpty";
		public const string WallPrefabName = "Wall";

		private int _row;
		public int Row
		{
			get
			{
				return _row;
			}
		}

		private int _col;
		public int Col
		{
			get
			{
				return _col;
			}
		}

		public const string SlotKeyFormat = "Slot_{0}x{1}";
		private string _key = string.Empty;
		public string key 
		{
			get 
			{
				if(string.IsNullOrEmpty(_key))
				{
					_key = string.Format(SlotKeyFormat,_row,_col);
				}

				return _key;
			}
		}

		SlotForCard slotForChip;
		public SlotGravity slotGravity;
		public SlotTeleport slotTeleport;

		/// <summary>
		/// 是否障碍
		/// </summary>
		public BlockInterface Block{get;set;} // Block for this slot

//
//
//		/// <summary>
//		/// 产生
//		/// </summary>
//		/// <value>The slot generator.</value>
//		public SlotGenerator SlotGenerator{get;set;}
//
//		/// <summary>
//		/// 传送
//		/// </summary>
//		/// <value>The slot teleport.</value>
//		public SlotTeleport SlotTeleport{get;set;}
//
//		/// <summary>
//		/// 重力
//		/// </summary>
//		/// <value>The slot gravity.</value>
//		public SlotGravity SlotGravity{get;set;}


		private Dictionary<Side, Slot> _nearSlot = new Dictionary<Side, Slot> ();
		public Dictionary<Side, Slot> nearSlot
		{
			get
			{
				return _nearSlot;
			}
		}

		/// <summary>
		/// 相邻的卡片
		/// </summary>
		/// <param name="index">Index.</param>
		public Slot this[Side index] 
		{
			get 
			{
				return _nearSlot[index];
			}
		}

		/// <summary>
		/// 是否受重力影响
		/// </summary>
		public bool gravity = true;

		/// <summary>
		/// 是否传送目标
		/// </summary>
		public bool IsTeleportTarget = false;

		/// <summary>
		/// 是否产生卡牌
		/// </summary>
		public bool IsGenerator = false;

		private Animation _animIceBlock;
		private Animation _animIceGloss;

		void  Awake ()
		{
			slotForChip = GetComponent<SlotForCard>();
			slotGravity = GetComponent<SlotGravity>();
			slotTeleport = GetComponent<SlotTeleport>();
		}


//		// Update is called once per frame
//		void Update () 
//		{
//			if(SlotGenerator != null)
//				SlotGenerator.Update();
//		}

		public void SetPos(int row,int col)
		{
			_row = row;
			_col = col;

//			this.transform.SetParent(FightControl.GoUIRoot.transform,false);
			float x = -70 * (0.5f * (FieldAssistant.Instance.field.width - 1) - row);
			float y = -70 * (0.5f * (FieldAssistant.Instance.field.height - 1) - col);
			this.transform.localPosition = new Vector3(x,y,0);
			this.name = key;
		}


//		public void Reset()
//		{
//			
//		}

		public void SetChip(Card card)
		{
//			Card = card;
//			Card.transform.SetParent(this.transform,false);
////			Card.SetSlot(this);
//
			if (slotForChip) 
			{
				FieldAssistant.Instance.field.chips[_row, _col] = card.id;
				FieldAssistant.Instance.field.powerUps[_row, _col] = card.powerId;
				slotForChip.SetChip(card);

			}
		}

		public void SetChipTemp(Card card)
		{
			//			Card = card;
			//			Card.transform.SetParent(this.transform,false);
			////			Card.SetSlot(this);
			//
			if (slotForChip) 
			{
				FieldAssistant.Instance.field.chips[_row, _col] = card.id;
				FieldAssistant.Instance.field.powerUps[_row, _col] = card.powerId;
				slotForChip.SetChipTemp(card);

			}
		}

		public Card GetChip ()
		{
			if (slotForChip) return slotForChip.GetCard();
			return null;
		}

		public void SetNearBySlot(Side side,Slot nearbySlot)
		{
			_nearSlot.Add(side,nearbySlot);
		}



		// Check for the presence of the "shadow" in the slot. No shadow - is a direct path from the slot up to the slot with a component SlotGenerator. Towards must have slots (without blocks and wall)
		// This concept is very important for the proper physics chips
		public bool GetShadow()
		{
			if (slotGravity != null) return slotGravity.shadow;
			else return false;
		}

		// Shadow can also discard the other chips - it's a different kind of shadow.
		public bool GetChipShadow ()
		{
			Side direction = slotGravity.fallingDirection;
			Slot s = nearSlot[direction];
			for (int i = 0; i < 40; i ++) 
			{
				if (!s) return false;
				if (!s.gravity)  return false;
				if (!s.GetChip() || s.slotGravity.gravityDirection != direction) 
				{
					direction = s.slotGravity.fallingDirection;
					s = s.nearSlot[direction];
				} 
				else 
				{
					return true;
				}
			}

			return false;
		}


		/// <summary>
		/// Dictionary walls - blocks the movement of chips in certain directions.
		/// </summary>
		public Dictionary<Side, bool> wallMask = new Dictionary<Side, bool> ();


		// creating a wall between it and neighboring slot
		public void  SetWall (Side side){

			wallMask[side] = true;

			foreach (Side s in Utils.straightSides)
				if (wallMask[s]) 
				{
					//设置各自旁边的格子是null
					if (nearSlot[s] != null) 
						nearSlot[s].nearSlot[Utils.MirrorSide(s)] = null;

					nearSlot[s] = null;
				}

			foreach (Side s in Utils.slantedSides)
				if (wallMask[Utils.SideHorizontal(s)] && wallMask[Utils.SideVertical(s)]) {
					if (nearSlot[s] != null) 
						nearSlot[s].nearSlot[Utils.MirrorSide(s)] = null;

					nearSlot[s] = null;
				}

		}

		public void  SetBlock (BlockInterface b)
		{
			Block = b;
		}
	}
}
