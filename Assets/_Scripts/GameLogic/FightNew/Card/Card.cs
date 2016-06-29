using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FightNew
{
	public class Card : MonoBehaviour 
	{
		private Point _point;
		public Point Point
		{
			get
			{
				return _point;
			}
			set
			{
				_point = value;
			}
		}

		/// <summary>
		/// 卡片类型
		/// </summary>
		public E_CardType CardType;

		public bool IsLineCard
		{
			get
			{
				return CardType == E_CardType.OneLine || CardType == E_CardType.CrossLine || CardType == E_CardType.ThreeLine;
			}
		}

		/// <summary>
		/// 消除的爆炸id
		/// </summary>
		public int powerId; // Chip type ID

		public IHelpCard helpCard;

		public Animation anim;

		/// <summary>
		/// 是否正在掉落(move)
		/// </summary>
		public bool IsDrop = false; // is chip involved in the fall (SessionAssistant.main.gravity)

		public bool hitting = false;

		/// <summary>
		/// in the process of destruction
		/// </summary>
		public bool destroying = false;

		/// <summary>
		/// 是否可以销毁
		/// </summary>
		public bool destroyable = true;

		public int movementID = 0;

		public bool can_move = true;
		//开始移动
		public bool move = false;

		#region 移动相关变量
		/// <summary>
		/// 初始速度
		/// </summary>
		float iniVelocity = 300f;
		/// <summary>
		/// 加速度
		/// </summary>
		float acceleration = 4000f; // acceleration
		/// <summary>
		/// 最高速度
		/// </summary>
		static float velocityLimit = 700f;
		public float velocity = 0; // current velocity

		public Vector3 impulse = Vector3.zero;
		Vector3 impulsParent = new Vector3(0,0,-1);
		Vector3 moveVector;


		Vector3 lastPosition;
		Vector3 zVector;

		bool mGravity = false;
		public bool gravity 
		{
			set 
			{
				if (value == mGravity)
					return;

				mGravity = value;
				if(FightMgr._instance == null)
					return;

				if (mGravity)
				{
//					Debug.Log("add gravity");
					FightMgr.Instance.gravity++;
				}
				else
					FightMgr.Instance.gravity--;
			}

			get 
			{
				return mGravity;
			}
		}


		#endregion

		public Slot Slot;
		public Slot LastSlot;

		private Transform _transBorder;

		public void Init()
		{
		}

		public void Reset()
		{
		}

		void Awake()
		{
			anim = GetComponent<Animation>();
			_transBorder = transform.Find("Border");
		}

		// Update is called once per frame
		void  Update () 
		{
//			return;
			if (!Slot) 
			{
				if (!destroying)
					DestroyChip();
				return;
			}

			if(FightMgr.Instance.IsIceBlcok)
			{
				return;
			}
				
//			if (!FightMgr.Instance.isPlaying)
//			{
//				//DebugUtil.Warning("cant update card,status:" + FightControl.Instance.IsFighting + "," + SessionControl.Instance.isPlaying);
//				return;
//			}

//			if(Slot && Slot.IsGenerator && Slot.slotGravity)
//			{
//				Card card = Slot[Slot.slotGravity.gravityDirection].Card;
//				this.gameObject.SetActive(!card.IsDrop);
//			}
//
//						if (impulse != Vector3.zero && (parentSlot || impulsParent.z != -1)) 
//						{
//							if (impulsParent.z == -1) 
//							{
//								if (!parentSlot) 
//								{
//									impulse = Vector3.zero;
//									return;
//								}
//								if (!move) gravity = true;
//								move = true;
//								impulsParent = parentSlot.transform.position;
//							}
//							if (impulse.sqrMagnitude > 36)
//								impulse = impulse.normalized * 6;
//							transform.position += impulse * Time.deltaTime;
//							transform.position += (impulsParent - transform.position) * Time.deltaTime;
//							impulse -= impulse * Time.deltaTime;
//							impulse -= 3f * (transform.position - impulsParent);
//							impulse *= 1 - 6 * Time.deltaTime;
//							if ((transform.position - impulsParent).magnitude < 2 * Time.deltaTime && impulse.magnitude < 2) {
//			
//								impulse = Vector3.zero;
//								transform.position = impulsParent;
//								impulsParent.z = -1;
//								if (move) {
//									gameObject.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
//									gravity = false;
//								}
//			
//								move = false;
//			
//							}
//							return;
//						}

//			if (impulse != Vector3.zero && (parentSlot || impulsParent.z != -1)) 
//			{
//				if (impulsParent.z == -1) 
//				{
//					if (!parentSlot) 
//					{
//						impulse = Vector3.zero;
//						return;
//					}
//					if (!move) gravity = true;
//					move = true;
//					impulsParent = Vector3.zero;
//				}
//
//				transform.localPosition += impulse * Time.deltaTime;
//
//				transform.localPosition += (impulsParent - transform.localPosition) * Time.deltaTime;
//
//				impulse -= impulse * Time.deltaTime;
//				impulse -= 3f * (transform.localPosition - impulsParent);
//				impulse *= 1 - 6 * Time.deltaTime;
//
//				if ((transform.localPosition - impulsParent).magnitude < 140 * Time.deltaTime && impulse.magnitude < 140) {
//
//					impulse = Vector3.zero;
//					transform.localPosition = impulsParent;
//					impulsParent.z = -1;
//					if (move) {
//						gameObject.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
//						gravity = false;
//					}
//
//					move = false;
//
//				}
//				return;
//			}

			if (destroying) return;

			if (!FightMgr.Instance.CanIGravity() || !can_move) 
			{
//				if(IsDrop && move)
//				{
//					//DebugUtil.Debug("update card:" + this.gameObject.name);
//					IsDrop = false;
//					move = false;
//					velocity = 0;
//					movementID = FightMgr.Instance.GetMovementID();
//
//					gravity = false;
//
//					if(helpCard != null)
//						helpCard.OnHit();
//
//					//再计算
//					Solution solution = Slot.MatchAnaliz();
//					if(solution != null && solution.count > 0)
//					{
//						FightMgr.Instance.ListSoution.Add(solution);
//					}
//				}

				return;
			}

			if (FightMgr.Instance.matching > 0 || !IsDrop) return;

			moveVector.x = 0;
			moveVector.y = 0;

			if (Slot && transform.localPosition != Vector3.zero) 
			{
				if (!move) 
				{
					move = true;
					gravity = true;
					velocity = iniVelocity;
				}

				velocity += acceleration * Time.deltaTime;
				if (velocity > velocityLimit) velocity = velocityLimit;

				lastPosition = transform.position;

				if (Mathf.Abs(transform.localPosition.x) < velocity * Time.deltaTime) {
					zVector = transform.localPosition;
					zVector.x = 0;
					transform.localPosition = zVector;
				}
				if (Mathf.Abs(transform.localPosition.y) < velocity * Time.deltaTime) {
					zVector = transform.localPosition;
					zVector.y = 0;
					transform.localPosition = zVector;
				}

				if (transform.localPosition == Vector3.zero) 
				{
					IsDrop = false;
					if(Slot.slotGravity)
						Slot.slotGravity.GravityReaction();

					IsDrop = true;

					if (transform.localPosition != Vector3.zero)
					{
						//还需要移动，还原
						transform.position = lastPosition;
					}
					else
					{
						velocity = 0;
					}

				}
				else
				{
					if (transform.localPosition.x < 0)
						moveVector.x = 10;
					if (transform.localPosition.x > 0)
						moveVector.x = -10;
					if (transform.localPosition.y < 0)
						moveVector.y = 10;
					if (transform.localPosition.y > 0)
						moveVector.y = -10;
					moveVector = moveVector.normalized * velocity;
					transform.localPosition += moveVector * Time.deltaTime;
				}
			} 
			else 
			{
				IsDrop = false;
				//Debug.Log("At zero pos:" + Slot.key);
				if (move) 
				{
					//DebugUtil.Debug("update card:" + this.gameObject.name);
//					DebugUtil.Debug("set isDrop = false:" + Slot.key);
					move = false;
					velocity = 0;
					movementID = FightMgr.Instance.GetMovementID();

					gravity = false;

					if(helpCard != null)
					{
						helpCard.OnHit();
					}

//					Debug.Log("begin");
					//再计算
					Solution solution = Slot.MatchAnaliz();
					if(solution != null && solution.count > 0)
					{
						FightMgr.Instance.ListSoution.Add(solution);
						//						Debug.Log("end");
					}

				}
			}
		}
			

		void OnDestroy()
		{
			gravity = false;
		}

		/// <summary>
		/// 设置边框可见
		/// </summary>
		/// <param name="display">If set to <c>true</c> display.</param>
		public void SetBorder(bool display)
		{
			if(transform == null || _transBorder == null) 
				return;

			_transBorder.gameObject.SetActive(display);
		}

		/// <summary>
		/// 是否可以消除
		/// </summary>
		/// <returns><c>true</c> 
		public bool IsMatcheble (bool isCheckLine = false)
		{
			if(isCheckLine)
			{
				if (CardType != E_CardType.Fight_SimpleCard1
					&& CardType != E_CardType.Fight_SimpleCard2
					&& CardType != E_CardType.Fight_SimpleCard3
					&& CardType != E_CardType.Fight_SimpleCard4
					&& CardType != E_CardType.Fight_SimpleCard5
					&& CardType != E_CardType.Fight_SimpleCard6
					&& CardType != E_CardType.OneLine
					&& CardType != E_CardType.CrossLine
					&& CardType != E_CardType.ThreeLine) return false;
			}
			else
			{
				//只有普通卡能匹配
				if (CardType != E_CardType.Fight_SimpleCard1
					&& CardType != E_CardType.Fight_SimpleCard2
					&& CardType != E_CardType.Fight_SimpleCard3
					&& CardType != E_CardType.Fight_SimpleCard4
					&& CardType != E_CardType.Fight_SimpleCard5
					&& CardType != E_CardType.Fight_SimpleCard6) return false;
			}
			
			if (destroying) return false;

			//if (SessionControl.Instance.gravity == 0) return true;
			if (IsDrop || hitting) return false;

			//没有父级
			if(Slot == null) return false;

//			if (transform.position != parentSlot.transform.position) return false;//在移动中
//			if (velocity != 0) return false;
//			if (chipType == "SugarChip") return false;

//			foreach (Side side in Utils.straightSides)
//				if (parentSlot[side]
//					&& parentSlot[side].gravity
//					&& !parentSlot[side].GetShadow()
//					&& !parentSlot[side].GetChip())
//					return false;


//			//上方有正在掉落的
//			if(Slot[Side.Left] != null && Slot[Side.Left].Card != null && 
//			if(Slot[Side.Top] != null && Slot[Side.Top].Card.IsDrop)
//				return false;

			return true;
		}


		#region 计算效果值
		/// <summary>
		/// 当前card爆炸后的效果值
		/// </summary>
		/// <returns>The potencial.</returns>
		public int GetPotencial() 
		{
			int potential = 1;
			Slot slot;

			string key = string.Empty;
			List<Card> listCards = GetDangeredChips(new List<Card>());
			Card c;
			Side side;
			for (int i = 0;i < listCards.Count;i++)
			{
				c = listCards[i];

				potential += GetPotencial(c.powerId);
				for(int sideIndex = 0;sideIndex < Utils.straightSides.Length;sideIndex++) 
				{
					side = Utils.straightSides[sideIndex];

					key = string.Format(FightDefine.Format_SlotKey,c.Slot.Point.X + Utils.SideOffsetX(side),c.Slot.Point.Y + Utils.SideOffsetY(side));
					slot = FightMgr.Instance.FindSlot(key);
					if (slot && slot.Block)
						//炸到障碍
						potential += 10;
				}
			}

			return potential;

		}

		public List<Card> GetDangeredChips(List<Card> stack) 
		{
			if (stack.Contains(this))
				return stack;

			if (powerId == 0)
				stack.Add(this);
			//			if (powerId == 1)
			//				stack = gameObject.GetComponent<SimpleBomb>().GetDangeredChips(stack);
//			if (powerId == 2)
//				stack = gameObject.GetComponent<CrossBomb>().GetDangeredChips(stack);
			//			if (powerId == 3)
			//				stack = gameObject.GetComponent<ColorBomb>().GetDangeredChips(stack);
			//			if (powerId == 4)
			//				stack = gameObject.GetComponent<RainbowHeart>().GetDangeredChips(stack);
			//			if (powerId == 5)
			//				stack = gameObject.GetComponent<Ladybird>().GetDangeredChips(stack);


			return stack;
		}

		// potential depending on powerID
		public static int GetPotencial (int i)
		{
			if (i == 0) return 1; // Simple Chip
			if (i == 1) return 7; // line
			if (i == 2) return 12; // cross Line
			if (i == 3) return 30; // three line
			return 0;
		}
		#endregion

		#region 销毁card

		// Starting the process of destruction of the chips
		public void  DestroyChip ()
		{
			if (!destroyable) return;
			if (destroying) return;
//			if (Slot && Slot.Block != null) 
//			{
//				Slot.Block.BlockCrush(false);
//				return;
//			}
			destroying = true;
			//			DebugUtil.Debug("DestroyChip:" + parentSlot.slot.key);

			RemoveFromSlot();

			if(helpCard)
				FightMgr.Instance.StartCoroutine(helpCard.DestroyChipFunction());


//			SendMessage("DestroyChipFunction", SendMessageOptions.DontRequireReceiver); // It sends a message to another component. It assumes that there is another component to the logic of a specific type of chips



		}

		// Physically destroy the chip without activation and adding score points
		public void HideChip (bool collection)
		{
			if (destroying) return;
			destroying = true;

			RemoveFromSlot();

			FightMgr.Instance.StartCoroutine(HidingRoutine());

			//			if(this.gameObject.activeSelf)
			//			{
			//				
			//			}
			//			else
			//			{
			//				DebugUtil.Info("gameObj is not active:" + this.gameObject.name);
			//				Destroy(gameObject);
			//			}
		}

		IEnumerator HidingRoutine() 
		{
			while (true) 
			{
				this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, Vector3.zero, Time.deltaTime * 6f);
				if (this.transform.localScale.x <= 0.01f) 
				{
					Destroy(gameObject);
					yield break;
				}

				yield return 0;
			}

			//CardManager.Instance.ReturnCard(this);
//			Destroy(gameObject);
		}
			

		public void RemoveFromSlot()
		{
			if(Slot)
			{
				Slot slot = FightMgr.Instance.FindSlot(Slot.key);
				//todo temp
				if(slot)
				{
					LastSlot = Slot;

					slot.Card = null;
					Slot = null;
				}
			}
		}

		#endregion

		#region 闪动动画
		// To begin the process of flashing (for hints - SessionAssistant.main.ShowHint)
		public void Flashing (int eventCount)
		{
			StartCoroutine (FlashingUntil (eventCount));
		}

		// Coroutinr of flashing chip until a specified count of events
		IEnumerator  FlashingUntil (int eventCount)
		{
			anim.Play("Flashing");
			//不停播放动画，直到移动过
			while (eventCount == FightMgr.Instance.eventCount && !FightMgr.Instance.IsDoSkillToSlot) yield return 0;

			if (!this) yield break;
			while (anim["Flashing"].time % anim["Flashing"].length > 0.1f)
				yield return 0;
			anim["Flashing"].time = 0;
			yield return 0;
			anim.Stop("Flashing");
		}
		#endregion


		public void OnHitAnimOver()
		{
			hitting = false;
		}
	}
}
