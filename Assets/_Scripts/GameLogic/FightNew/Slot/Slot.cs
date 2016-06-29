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

namespace FightNew
{
	public class Slot : MonoBehaviour 
	{
		/// <summary>
		/// 特殊效果，引爆次序
		/// </summary>
		/// <value>The index of the remove.</value>
		public int RemoveIndex{get;set;}

		private Point _point;
		public Point Point
		{
			set
			{
				_point = value;

				SetPoint();
			}
			get
			{
				return _point;
			}
		}

		public string key;

//		SlotForCard slotForChip;
		public SlotGravity slotGravity;
		public SlotTeleport slotTeleport;

		/// <summary>
		/// 是否障碍
		/// </summary>
		public BlockInterface Block{get;set;} // Block for this slot

		private Slot[] _nearSlot = new Slot[9];// new Dictionary<Side, Slot> ();
		public Slot[] nearSlot
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
				return _nearSlot[(int)index];
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

		public Card Card;

		/// <summary>
		/// 是否boss位置
		/// </summary>
		/// <value><c>true</c> if this instance is boss; otherwise, <c>false</c>.</value>
		public bool IsBoss{get;set;}

		private Animation _animIceBlock;
		private Animation _animIceGloss;

		void  Awake ()
		{
//			slotForChip = GetComponent<SlotForCard>();
			slotGravity = GetComponent<SlotGravity>();
			slotTeleport = GetComponent<SlotTeleport>();

			_animIceBlock = transform.Find("IceBlock").GetComponent<Animation>();
			_animIceGloss = _animIceBlock.transform.Find("Gloss").GetComponent<Animation>();
		}

		private void SetPoint()
		{
			key = string.Format(FightDefine.Format_SlotKey,_point.X,_point.Y);
			this.name = key;

			float x = (Point.X - FightMgr.Instance.Width * 0.5f + 0.5f) * 70;
			float y = (Point.Y - FightMgr.Instance.Height * 0.5f + 0.5f) * 70;

			this.transform.localPosition = new Vector3(x,y,0);
		}

//		public void RemoveCard()
//		{
//			Card = null;
//
//			if(slotGravity)
//				slotGravity.chip = null;
//		}
//
		public void SetChip(Card card,bool isWorldPosStays = false)
		{
//			if (slotForChip) 
//			{
//				FieldAssistant.Instance.field.chips[_row, _col] = card.id;
//				FieldAssistant.Instance.field.powerUps[_row, _col] = card.powerId;
//				slotForChip.SetChip(card);
//
//			}


//			if(card.Slot)
//			{
//				Slot oriSlot = FightMgr.Instance.FindSlot(card.Slot.key);
//				oriSlot.Card = null;
//
//				if(oriSlot.slotGravity)
//					oriSlot.slotGravity.chip = null;
//			}

			Card cardTemp = card;

			if(card.Slot && card.Slot.Card == card)
				card.Slot.Card = null;
			
			card = cardTemp;
			Card = card;
			card.Slot = this;

			if(!isWorldPosStays)
			{
				card.transform.SetParent(this.transform,false);
				card.transform.localPosition = Vector3.zero;

				//Debug.Log("set slot:" + this.gameObject.name);
			}
			else
			{
				if(card.anim.IsPlaying("Hit"))
					card.anim.Stop();

				card.transform.SetParent(this.transform);
//				DebugUtil.Debug("set isDrop = true:" + card.Slot.key);
				card.IsDrop = true;
			}

			FieldMgr.field.chips[Point.X,Point.Y] = (int)card.CardType;
			FieldMgr.field.powerUps[Point.X,Point.Y] = card.powerId;
		}
//
//		public void SetChipTemp(Card card)
//		{
//			//			Card = card;
//			//			Card.transform.SetParent(this.transform,false);
//			////			Card.SetSlot(this);
//			//
//			if (slotForChip) 
//			{
//				FieldAssistant.Instance.field.chips[_row, _col] = card.id;
//				FieldAssistant.Instance.field.powerUps[_row, _col] = card.powerId;
//				slotForChip.SetChipTemp(card);
//
//			}
//		}
//
		public Card GetChip ()
		{
//			if (slotForChip) return slotForChip.GetCard();
//			return null;

			return Card;
//			if(Card != null && !Card.IsDrop)
//				return Card;
//
//			return null;
		}
//
//		public void SetNearBySlot(Side side,Slot nearbySlot)
//		{
//			_nearSlot.Add(side,nearbySlot);
//		}
//
//
//
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
			Slot s = nearSlot[(int)direction];
			for (int i = 0; i < 40; i ++) 
			{
				if (!s) return false;
				if (!s.gravity)  return false;
				if (!s.GetChip() || s.slotGravity.gravityDirection != direction) 
				{
					direction = s.slotGravity.fallingDirection;
					s = s.nearSlot[(int)direction];
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


		/// <summary>
		/// 隔绝邻近的格子
		/// </summary>
		/// <param name="side">Side.</param>
		public void  SetWall (Side side)
		{

			wallMask[side] = true;

			foreach (Side s in Utils.straightSides)
				if (wallMask[s]) 
				{
					//设置各自旁边的格子是null
					if (nearSlot[(int)s] != null) 
						nearSlot[(int)s].nearSlot[(int)Utils.MirrorSide(s)] = null;

					nearSlot[(int)s] = null;
				}

			foreach (Side s in Utils.slantedSides)
				if (wallMask[Utils.SideHorizontal(s)] && wallMask[Utils.SideVertical(s)]) {
					if (nearSlot[(int)s] != null) 
						nearSlot[(int)s].nearSlot[(int)Utils.MirrorSide(s)] = null;

					nearSlot[(int)s] = null;
				}

		}

		public void  SetBlock (BlockInterface b)
		{
			Block = b;
		}


		// Analysis of chip for combination
		public Solution MatchAnaliz (bool checkPotential = true)
		{
			if (!GetChip() || !GetChip().IsMatcheble()) return null;

//			if (GetChip().id == 10) 
//			{ // multicolor
//				List<SessionControl.Solution> solutions = new List<SessionControl.Solution>();
//				SessionControl.Solution z;
//				Card multicolorChip = GetChip();
//				for (int i = 0; i < 6; i++) 
//				{
//					multicolorChip.id = i;
//					z = MatchAnaliz();
//					if (z != null)
//						solutions.Add(z);
//					//					z = MatchSquareAnaliz();
//					//					if (z != null)
//					//						solutions.Add(z);
//				}
//				multicolorChip.id = 10;
//				z = null;
//				foreach (SessionControl.Solution sol in solutions)
//					if (z == null || z.potential < sol.potential)
//						z = sol;
//				return z;
//			}

			Slot s;
			Dictionary<Side, List<Card>> sides = new Dictionary<Side, List<Card>>();
			int count;
			string key;

			Side side;
			for (int i = 0;i < Utils.straightSides.Length;i++)
			{
				side = Utils.straightSides[i];

				count = 1;
				sides.Add(side, new List<Card>());
				while (true) 
				{
					key = string.Format(FightDefine.Format_SlotKey,(Point.X + Utils.SideOffsetX(side) * count),(Point.Y + Utils.SideOffsetY(side) * count));

					s = FightMgr.Instance.FindSlot(key);
					if(s == null)
						break;
					if (!s.GetChip())
						break;
					if (s.GetChip().CardType != Card.CardType)// && s.GetChip().id != 10)
						break;
					if (!s.GetChip().IsMatcheble())
						break;
					sides[side].Add(s.GetChip());
					count++;
				}
			}
				
			bool h = sides[Side.Right].Count + sides[Side.Left].Count >= 2;
			bool v = sides[Side.Top].Count + sides[Side.Bottom].Count >= 2;

			if (h || v) 
			{
				Solution solution = new Solution();

				solution.h = h;
				solution.v = v;
				solution.hCount = 0;
				solution.vCount = 0;

				solution.chips = new List<Card>();
				solution.chips.Add(GetChip());

				if (h) {
					solution.chips.AddRange(sides[Side.Right]);
					solution.chips.AddRange(sides[Side.Left]);
					solution.hCount = sides[Side.Right].Count + sides[Side.Left].Count + 1;
				}
				if (v) {
					solution.chips.AddRange(sides[Side.Top]);
					solution.chips.AddRange(sides[Side.Bottom]);
					solution.vCount = sides[Side.Top].Count + sides[Side.Bottom].Count + 1;
				}

				solution.count = solution.chips.Count;

				solution.x = Point.X;
				solution.y = Point.Y;
				solution.id = (int)Card.CardType;

				if(checkPotential)
				{
					foreach (Card c in solution.chips)
					{
						solution.potential += c.GetPotencial();
					}
				}

				return solution;
			}
			return null;
		}


		private string[] _iceSprites = new string[]{"wbg-6a","wbg-6b","wbg-6c"};
		public void PlayIceBlock(bool isShow)
		{
			if(isShow)
			{
				
				_animIceBlock.GetComponent<UISprite>().spriteName = _iceSprites[Random.Range(0,3)];

				_animIceBlock.gameObject.SetActive(true);

				_animIceBlock.Play("IceBlockFallIn");

				if(Random.Range(0,100) >= 80)
					_animIceGloss.Play("Gloss");

				//停止动画
				if(Card != null && Card.IsLineCard)
				{
					Card.transform.Find("Chip").GetComponent<UISpriteAnimation>().enabled = false;
				}
			}
			else
			{
				_animIceBlock.gameObject.SetActive(false);
				_animIceBlock.Stop();
				_animIceGloss.Stop();

				//停止动画
				if(Card != null && Card.IsLineCard)
				{
					Card.transform.Find("Chip").GetComponent<UISpriteAnimation>().enabled = true;
				}
			}

		}
	}
}
