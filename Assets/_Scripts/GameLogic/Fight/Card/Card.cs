/*
 * 
 * 文件名(File Name)：             Card
 *
 * 作者(Author)：                  #AuthorName#
 *
 * 创建时间(CreateTime):           2016/03/29 11:45:29
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Fight
{
	public class Card : MonoBehaviour
	{
		public const string SimpleCard1 = "SimpleCard1";
		public const string SimpleCard2 = "SimpleCard2";
		public const string SimpleCard3 = "SimpleCard3";
		public const string SimpleCard4 = "SimpleCard4";
		public const string SimpleCard5 = "SimpleCard5";
		public const string SimpleCard6 = "SimpleCard6";

		public const string CrossBombCard1 = "CrossBombCard1";
		public const string CrossBombCard2 = "CrossBombCard2";
		public const string CrossBombCard3 = "CrossBombCard3";
		public const string CrossBombCard4 = "CrossBombCard4";
		public const string CrossBombCard5 = "CrossBombCard5";
		public const string CrossBombCard6 = "CrossBombCard6";

		public const string HLineBombCard1 = "HLineBombCard1";
		public const string HLineBombCard2 = "HLineBombCard2";
		public const string HLineBombCard3 = "HLineBombCard3";
		public const string HLineBombCard4 = "HLineBombCard4";
		public const string HLineBombCard5 = "HLineBombCard5";
		public const string HLineBombCard6 = "HLineBombCard6";

		public const string VLineBombCard1 = "VLineBombCard1";
		public const string VLineBombCard2 = "VLineBombCard2";
		public const string VLineBombCard3 = "VLineBombCard3";
		public const string VLineBombCard4 = "VLineBombCard4";
		public const string VLineBombCard5 = "VLineBombCard5";
		public const string VLineBombCard6 = "VLineBombCard6";

		public const string UltraColorBomb = "UltraColorBomb";

		public const string Card3dPrefabFormat = "card3d_{0}";
		public const string Card3d0 = "card3d_0";
		public const string Card3d1 = "card3d_1";
		public const string Card3d2 = "card3d_2";
		public const string Card3d3 = "card3d_3";
		public const string Card3d4 = "card3d_4";
		public const string Card3d5 = "card3d_5";


		// Colors for each chip color ID
		public static Color[] colors = {
			new Color(0.75f, 0.3f, 0.3f),
			new Color(0.3f, 0.75f, 0.3f),
			new Color(0.3f, 0.5f, 0.75f),
			new Color(0.75f, 0.75f, 0.3f),
			new Color(0.75f, 0.3f, 0.75f),
			new Color(0.75f, 0.6f, 0.3f)
		};

		public static string[] chipTypes = 
			{
				"Card1",
				"Card2",
				"Card3",
				"Card4",
				"Card5",
				"Card6" 
			};

		public SlotForCard parentSlot; // Slot which include this chip
		public E_CardType CardType;
		public string chipType = "None"; // Chip type name

		public int powerId; // Chip type ID

//		private int _id;
		//0~5
		public int id; // Chip color ID

//		private Slot _slot;

//		public Slot Slot;

		/// <summary>
		/// in the process of destruction
		/// </summary>
		public bool destroying = false;

		public int movementID = 0;

		public bool can_move = true;


		float velocity = 0; // current velocity
		Animation anim;
		public bool move = false; // is chip involved in the fall (SessionAssistant.main.gravity)
		bool mGravity = false;
		public bool gravity 
		{
			set 
			{
				if (value == mGravity)
					return;
				
				mGravity = value;
				if(SessionControl._instance == null)
					return;
				
				if (mGravity)
					SessionControl.Instance.gravity++;
				else
					SessionControl.Instance.gravity--;
			}

			get 
				{
				return mGravity;
			}
		}

		public bool destroyable = true;


		#region 移动相关变量
		public Vector3 impulse = Vector3.zero;
		Vector3 impulsParent = new Vector3(0,0,-1);
		Vector3 moveVector;
		float acceleration = 20f; // acceleration
		static float velocityLimit = 17f;
		Vector3 lastPosition;
		Vector3 zVector;
		#endregion

		void  Awake ()
		{
			anim = GetComponent<Animation>();
			Init();
		}

		public void Init()
		{
			velocity = 1;
			move = true;
			//			DebugUtil.Debug("card awake:" + this.gameObject.name);
			gravity = true;
		}

		public void Reset()
		{
			//Slot = null;
			//GoItem.SetActive(false);
		}

		// function of conditions of possibility of matching
		public bool IsMatcheble ()
		{
			if (id < 0) return false;
			if (destroying) return false;
			if (SessionControl.Instance.gravity == 0) return true;
			if (move) return false;
			if (transform.position != parentSlot.transform.position) return false;//在移动中
			if (velocity != 0) return false;
			if (chipType == "SugarChip") return false;

			foreach (Side side in Utils.straightSides)
				if (parentSlot[side]
					&& parentSlot[side].gravity
					&& !parentSlot[side].GetShadow()
					&& !parentSlot[side].GetChip())
					return false;

			return true;
		}

		#region 销毁card

		// Starting the process of destruction of the chips
		public void  DestroyChip ()
		{
			if (!destroyable) return;
			if (destroying) return;
			if (parentSlot && parentSlot.slot.Block != null) 
			{
				parentSlot.slot.Block.BlockCrush(false);
				return;
			}
			destroying = true;
//			DebugUtil.Debug("DestroyChip:" + parentSlot.slot.key);
			SendMessage("DestroyChipFunction", SendMessageOptions.DontRequireReceiver); // It sends a message to another component. It assumes that there is another component to the logic of a specific type of chips
		}


		// separation of the chips from the parent slot
		public void  ParentRemove ()
		{
			if (!parentSlot) return;

			parentSlot.card = null;
			parentSlot = null;
		}

		// Physically destroy the chip without activation and adding score points
		public void HideChip (bool collection)
		{
			if (destroying) return;
			destroying = true;

			ParentRemove();

			MyFrameWork.APPMonoController.Instance.StartCoroutine(HidingRoutine());

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
			yield return StartCoroutine(MinimizingRoutine());
//			CardManager.Instance.ReturnCard(this);
			Destroy(gameObject);
		}

		public void Minimize() 
		{
			StartCoroutine(MinimizingRoutine());
		}

		IEnumerator MinimizingRoutine() 
		{
			while (true) 
			{
				this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, Vector3.zero, Time.deltaTime * 6f);
				if (this.transform.localScale.x == 0) 
				{
					yield break;
				}
				yield return 0;
			}
		}

		#endregion

		public bool IsCheckMovePos = true;
		// function describing the physics of chips
		void  Update () 
		{
			if (!parentSlot) 
			{
				if (!destroying)
					DestroyChip();
				return;
			}

			if (!SessionControl.Instance.isPlaying || !FightControl.Instance.IsFighting)
			{
//				DebugUtil.Warning("cant update card,status:" + FightControl.Instance.IsFighting + "," + SessionControl.Instance.isPlaying);
				return;
			}

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
//					impulsParent = parentSlot.transform.position;
//				}
//				if (impulse.sqrMagnitude > 36)
//					impulse = impulse.normalized * 6;
//				transform.position += impulse * Time.deltaTime;
//				transform.position += (impulsParent - transform.position) * Time.deltaTime;
//				impulse -= impulse * Time.deltaTime;
//				impulse -= 3f * (transform.position - impulsParent);
//				impulse *= 1 - 6 * Time.deltaTime;
//				if ((transform.position - impulsParent).magnitude < 2 * Time.deltaTime && impulse.magnitude < 2) {
//
//					impulse = Vector3.zero;
//					transform.position = impulsParent;
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

			if (impulse != Vector3.zero && (parentSlot || impulsParent.z != -1)) 
			{
				if (impulsParent.z == -1) 
				{
					if (!parentSlot) 
					{
						impulse = Vector3.zero;
						return;
					}
					if (!move) gravity = true;
					move = true;
					impulsParent = Vector3.zero;
				}

//				bool debug = false;
////				float tempVal = 1;
//				if(keyTemp == string.Empty || keyTemp == parentSlot.slot.key)
//				{
//					keyTemp = parentSlot.slot.key;
//					debug = true;
//				}

//				if(debug)
//					DebugUtil.Info("=== " + keyTemp + " === inpulse1:" + impulse.sqrMagnitude + "," + impulse);
				
//				if (impulse.sqrMagnitude > 36)
//					impulse = impulse.normalized * 6;

//				if(debug)
//					DebugUtil.Info("inpulse2:" + impulse.sqrMagnitude + "," + impulse);

//				if(debug)
//					DebugUtil.Info("inpulse3,localpos:" + transform.localPosition);
				
				transform.localPosition += impulse * Time.deltaTime;

//				if(debug)
//					DebugUtil.Info("inpulse4:,localpos:" + transform.localPosition);
				
				transform.localPosition += (impulsParent - transform.localPosition) * Time.deltaTime;

//				if(debug)
//					DebugUtil.Info("inpulse5:,localpos:" + transform.localPosition);
				
				impulse -= impulse * Time.deltaTime;
				impulse -= 3f * (transform.localPosition - impulsParent);
				impulse *= 1 - 6 * Time.deltaTime;

//				if(debug)
//					DebugUtil.Info("inpulse6:" + impulse.sqrMagnitude + "," + impulse);
				
				if ((transform.localPosition - impulsParent).magnitude < 140 * Time.deltaTime && impulse.magnitude < 140) {

					impulse = Vector3.zero;
					transform.localPosition = impulsParent;
					impulsParent.z = -1;
					if (move) {
						gameObject.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
						gravity = false;
					}

					move = false;

				}
				return;
			}

			if (destroying) return;
			if (!SessionControl.Instance.CanIGravity() || !can_move) return;

			if (SessionControl.Instance.matching > 0 && !move && !IsCheckMovePos) return;
			moveVector.x = 0;
			moveVector.y = 0;

			if (parentSlot && transform.position != parentSlot.transform.position) {
				if (!move) 
				{
					move = true;
					gravity = true;
					velocity = 3f;
				}

				velocity += acceleration * Time.deltaTime;
				if (velocity > velocityLimit) velocity = velocityLimit;

				lastPosition = transform.position;

				if (Mathf.Abs(transform.position.x - parentSlot.transform.position.x) < velocity * Time.deltaTime) {
					zVector = transform.position;
					zVector.x = parentSlot.transform.position.x;
					transform.position = zVector;
				}
				if (Mathf.Abs(transform.position.y - parentSlot.transform.position.y) < velocity * Time.deltaTime) {
					zVector = transform.position;
					zVector.y = parentSlot.transform.position.y;
					transform.position = zVector;
				}

				if (transform.position == parentSlot.transform.position) {
					parentSlot.SendMessage("GravityReaction");
					if (transform.position != parentSlot.transform.position) 
						transform.position = lastPosition;
				}
				else
				{
					if (transform.position.x < parentSlot.transform.position.x)
						moveVector.x = 10;
					if (transform.position.x > parentSlot.transform.position.x)
						moveVector.x = -10;
					if (transform.position.y < parentSlot.transform.position.y)
						moveVector.y = 10;
					if (transform.position.y > parentSlot.transform.position.y)
						moveVector.y = -10;
					moveVector = moveVector.normalized * velocity;
					transform.position += moveVector * Time.deltaTime;
				}
			} 
			else 
			{
				if (move) 
				{
//					DebugUtil.Debug("update card:" + this.gameObject.name);
					move = false;
					velocity = 0;
					movementID = SessionControl.Instance.GetMovementID();
			
					gameObject.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
					gravity = false;
				}
			}
		}

		public List<Card> GetDangeredChips(List<Card> stack) {
			if (stack.Contains(this))
				return stack;


			if (powerId == 0)
				stack.Add(this);
//			if (powerId == 1)
//				stack = gameObject.GetComponent<SimpleBomb>().GetDangeredChips(stack);
			if (powerId == 2)
				stack = gameObject.GetComponent<CrossBomb>().GetDangeredChips(stack);
//			if (powerId == 3)
//				stack = gameObject.GetComponent<ColorBomb>().GetDangeredChips(stack);
//			if (powerId == 4)
//				stack = gameObject.GetComponent<RainbowHeart>().GetDangeredChips(stack);
//			if (powerId == 5)
//				stack = gameObject.GetComponent<Ladybird>().GetDangeredChips(stack);


			return stack;
		}


		// returns the value of the potential of the current chips. needs for estimation of solution potential.
		public int GetPotencial() 
		{
			int potential = 1;
			Slot slot;

			string key = string.Empty;
			foreach (Card c in GetDangeredChips(new List<Card>())) 
			{
				potential += GetPotencial(c.powerId);
				foreach (Side side in Utils.straightSides) 
				{
					key = string.Format(Slot.SlotKeyFormat,c.parentSlot.slot.Row + Utils.SideOffsetX(side),c.parentSlot.slot.Col + Utils.SideOffsetY(side));
					slot = SlotManager.Instance.FindSlot(key);
					if (slot && slot.Block)
						potential += 10;
				}
			}
				
			return potential;

		}

		// potential depending on powerID
		public static int GetPotencial (int i)
		{
			if (i == 0) return 1; // Simple Chip
			if (i == 1) return 7; // Simple Bomb
			if (i == 2) return 12; // Cross Bomb
			if (i == 3) return 12; // Color Bomb
			if (i == 4) return 30; // Lightning Bomb
			return 0;
		}


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
			while (eventCount == SessionControl.Instance.eventCount) yield return 0;

			if (!this) yield break;
			while (anim["Flashing"].time % anim["Flashing"].length > 0.1f)
				yield return 0;
			anim["Flashing"].time = 0;
			yield return 0;
			anim.Stop("Flashing");
		}
		#endregion

		void OnDestroy()
		{
			gravity = false;
		}


		public void OnHit() 
		{
			//AudioAssistant.Shot("ChipHit");

		}

		public void SetBorder(bool display)
		{
			if(transform == null) return;

			var trans = transform.Find("Border");

			if(trans != null)
				trans.gameObject.SetActive(display);
		}
	}
}
