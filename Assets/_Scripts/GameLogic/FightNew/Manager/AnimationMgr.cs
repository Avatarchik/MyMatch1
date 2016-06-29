/*
 * 
 * 文件名(File Name)：             AnimationControl
 *
 * 作者(Author)：                  #AuthorName#
 *
 * 创建时间(CreateTime):           2016/03/29 15:50:00
 *
 */

using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace FightNew
{
	public class AnimationMgr : Singleton<AnimationMgr>
	{
		/// <summary>
		/// 是否还能滑动交互
		/// </summary>
		public bool Iteraction = false;

		/// <summary>
		/// 交换动画时长
		/// </summary>
		private float swapDuration = 0.2f;

		bool swaping = false; // ИTRUE when the animation plays swapping 2 chips

		// Function immediate swapping 2 chips
		public void SwapTwoItemNow (Card a, Card b) 
		{
			if (!a || !b) return;
			if (a == b) return;
			if ((a.Slot && a.Slot.Block) || (b.Slot && b.Slot.Block)) return;


			a.movementID = FightMgr.Instance.GetMovementID();
			b.movementID = FightMgr.Instance.GetMovementID();

			Slot slotA = a.Slot;
			Slot slotB = b.Slot;

			slotB.SetChip(a);
			slotA.SetChip(b);
		}

		// The function of swapping 2 chips
		public void SwapTwoItem (Card a, Card b, bool force) 
		{
			if (!FightMgr.Instance.isPlaying || !FightMgr.Instance.IsFighting) 
			{
				a.SetBorder(false);
				b.SetBorder(false);
				return;
			}

			FightMgr.Instance.StartCoroutine(SwapTwoItemRoutine(a, b, force));
		}

//		// Function immediate swapping 2 chips
//		public void SwapTwoItemNow (Card a, Card b) 
//		{
//			if (!a || !b) return;
//			if (a == b) return;
//			if (a.parentSlot.slot.Block || b.parentSlot.slot.Block) return;
//
//			//Vector3 posA = a.parentSlot.transform.position;
//			//Vector3 posB = b.parentSlot.transform.position;
//
//			//a.transform.position = posB;
//			//b.transform.position = posA;
//
//			a.movementID = SessionControl.Instance.GetMovementID();
//			b.movementID = SessionControl.Instance.GetMovementID();
//
//			SlotForCard slotA = a.parentSlot;
//			SlotForCard slotB = b.parentSlot;
//
//			slotB.SetChip(a);
//			slotA.SetChip(b);
//		}

		// Coroutine swapping 2 chips

		public bool CanTouch()
		{
			if(!Iteraction || swaping || !FightMgr.Instance.CanIAnimate() || FightMgr.Instance.timeLeft <= 0)
				return false;

			return true;
		}
		IEnumerator SwapTwoItemRoutine (Card a, Card b, bool force)
		{
			if (!Iteraction) 
			{
				a.SetBorder(false);
				b.SetBorder(false);
				yield break;
			}

			// cancellation terms
			if (swaping) 
			{
				a.SetBorder(false);
				b.SetBorder(false);
				yield break; // If the process is already running
			}

			if (!a || !b) 
			{
				if(a)
					a.SetBorder(false);

				if(b)
					b.SetBorder(false);
				
				yield break; // If one of the chips is missing
			}

			if (a.destroying || b.destroying) 
			{
				a.SetBorder(false);
				b.SetBorder(false);

				yield break;
			}

			if (a.Slot == null || a.Slot.Block != null
				|| b.Slot == null || b.Slot.Block != null) 
			{
				a.SetBorder(false);
				b.SetBorder(false);

				yield break; // If one of the chips is blocked
			}

			if (!FightMgr.Instance.CanIAnimate()) 
			{
				a.SetBorder(false);
				b.SetBorder(false);

				yield break; // If the core prohibits animation
			}

//			if (FightMgr.Instance.movesCount <= 0)
//			{
//				a.SetBorder(false);
//				b.SetBorder(false);
//
//				FightMgr.Instance.ShowMsg(FightDefine.E_NoteMsgType.NoMoves);
//				yield break; // If not enough moves
//			}

			if (FightMgr.Instance.timeLeft <= 0) 
			{
				a.SetBorder(false);
				b.SetBorder(false);

				yield break; // If not enough time
			}
		

			//SessionControl.Mix mix = SessionControl.Instance.mixes.Find(x => x.Compare(a.chipType, b.chipType));

			int move = 0; // Number of points movement which will be expend

			FightMgr.Instance.animate++;
			swaping = true;

			Vector3 posA = a.Slot.transform.position;
			Vector3 posB = b.Slot.transform.position;

			float progress = 0;

			Vector3 normal = a.Slot.Point.X == b.Slot.Point.X ? Vector3.right : Vector3.up;
			float time = 0;
			// Animation swapping 2 chips	
			while (progress < swapDuration) 
			{
				if (a == null || b == null)
				{
					FightMgr.Instance.animate--;
					swaping = false;

					if(a)
						a.SetBorder(false);

					if(b)
						b.SetBorder(false);
					
					yield break;
				}

				time = EasingFunctions.easeInOutQuad(progress / swapDuration);
				a.transform.position = Vector3.Lerp(posA, posB, time) + normal * Mathf.Sin(3.14f * time) * 0.05f;
				//if (mix == null) 
				b.transform.position = Vector3.Lerp(posB, posA, time) - normal * Mathf.Sin(3.14f * time) * 0.05f;

				progress += Time.deltaTime;

				yield return 0;
			}

			a.transform.position = posB;
			//if (SessionControl.Instance.movesCount <= 0 || mix == null) 
			b.transform.position = posA;

			a.movementID = FightMgr.Instance.GetMovementID();
			b.movementID = FightMgr.Instance.GetMovementID();

//			if (SessionControl.Instance.movesCount > 0 && mix != null) 
//			{ // Scenario mix effect
//				swaping = false;
//				SessionControl.Instance.MixChips(a, b);
//				a.SetBorder(false);
//				b.SetBorder(false);
//
//				yield return new WaitForSeconds(0.3f);
//				SessionControl.Instance.movesCount--;
//				SessionControl.Instance.animate--;
//
//
//
//				SessionControl.Instance.SuccessMoveCounter();
//				yield break;
//			}

			// Scenario the effect of swapping two chips
//			SlotForCard slotA = a.parentSlot;
//			SlotForCard slotB = b.parentSlot;
//			slotB.SetChip(a);
//			slotA.SetChip(b);
			Slot slotA = a.Slot;
			Slot slotB = b.Slot;
			slotA.SetChip(b);
			slotB.SetChip(a);

			move++;

			// searching for solutions of matching
			int count = 0; 
			Solution solution = null;

			//todo yangzj
			solution = slotA.MatchAnaliz();
			if (solution != null && solution.count > 0) 
			{
				count += solution.count;

				if(FightMgr.Instance.movesCount > 0)
					FightMgr.Instance.ListSoution.Add(solution);
			}

//			solution = slotA.MatchSquareAnaliz();
//			if (solution != null) count += solution.count;

			solution = slotB.MatchAnaliz();
			if (solution != null && solution.count > 0)
			{
				count += solution.count;

				if(FightMgr.Instance.movesCount > 0)
					FightMgr.Instance.ListSoution.Add(solution);
			}

//			solution = slotB.MatchSquareAnaliz();
//			if (solution != null) count += solution.count;

			// Scenario canceling of changing places of chips
			if (FightMgr.Instance.movesCount <= 0 || (count == 0 && !force))
			{
				if(FightMgr.Instance.movesCount <= 0)
				{
					//显示不能移动提示
					FightMgr.Instance.ShowMsg(FightDefine.E_NoteMsgType.NoMoves);
				}

//				AudioAssistant.Shot("SwapFailed");
				while (progress > 0) 
				{
					time = EasingFunctions.easeInOutQuad(progress / swapDuration);
					a.transform.position = Vector3.Lerp(posA, posB, time) - normal * Mathf.Sin(3.14f * time) * 0.05f;
					b.transform.position = Vector3.Lerp(posB, posA, time) + normal * Mathf.Sin(3.14f * time) * 0.05f;

					progress -= Time.deltaTime;

					yield return 0;
				}

				a.transform.position = posA;
				b.transform.position = posB;

				a.movementID = FightMgr.Instance.GetMovementID();
				b.movementID = FightMgr.Instance.GetMovementID();

				slotB.SetChip(b);
				slotA.SetChip(a);

				a.SetBorder(false);
				b.SetBorder(false);

				move--;
			} 
			else 
			{
//				AudioAssistant.Shot("SwapSuccess");
				FightMgr.Instance.swapEvent ++;

//				SessionControl.Instance.SuccessMoveCounter();
				FightMgr.Instance.NeedSaveFirstDamage = true;
				if(TouchMgr.Instance.AfterMoveHandler != null)
					TouchMgr.Instance.AfterMoveHandler(a.Slot);
			}

//			SessionControl.Instance.firstChipGeneration = false;

			FightMgr.Instance.movesCount -= move;
			FightMgr.Instance.EventCounter ();

			FightMgr.Instance.animate--;
//			Debug.Log("animation:" + FightMgr.Instance.animate);

			a.SetBorder(false);
			b.SetBorder(false);

			swaping = false;
		}

//		// Function of creating of explosion effect
//		public void  Explode (Vector3 center, float radius, float force){
//			Card[] chips = GameObject.FindObjectsOfType<Card>();
//			Vector3 impuls;
//			foreach(Card chip in chips) 
//			{
//				if ((chip.transform.parent.localPosition - center).sqrMagnitude > radius * radius) 
//				{
////					DebugUtil.Info("fail:" + chip.transform.parent.localPosition + "," + chip.transform.parent.name);
//					continue;
//				}
////				else
////				{
////					DebugUtil.Info("success:" + chip.transform.parent.localPosition + "," + chip.transform.parent.name);
////				}
//				impuls = (chip.transform.parent.localPosition - center) * force;
//				impuls *= Mathf.Pow((radius - (chip.transform.parent.localPosition - center).magnitude) / radius, 2);
//				chip.impulse += impuls;
//			}
//		}
//
		public void TeleportChip(Card chip, Slot target) 
		{
			FightMgr.Instance.StartCoroutine (TeleportChipRoutine (chip, target));
		}

		IEnumerator TeleportChipRoutine (Card chip, Slot target) 
		{
			if (!chip.Slot) yield break;
			if (chip.destroying) yield break;
			if (target.GetChip() || !target.gravity) yield break;

			Vector3 scale_target = Vector3.zero;
			chip.can_move = false;
			target.SetChip(chip,true);


			scale_target.z = 1;
			while (chip != null && chip.transform.localScale.x > 0) 
			{
				chip.transform.localScale = Vector3.MoveTowards(chip.transform.localScale, scale_target, Time.deltaTime * 8);
				yield return 0;
			}

			if(chip != null)
				chip.transform.localPosition = Vector3.zero;
			
			scale_target.x = 1;
			scale_target.y = 1;
			while (chip != null && chip.transform.localScale.x < 1) 
			{
				chip.transform.localScale = Vector3.MoveTowards(chip.transform.localScale, scale_target, Time.deltaTime * 12);
				yield return 0;
			}

			if(chip != null && chip.Slot != null)
			{
				chip.IsDrop = false;
				chip.can_move = true;

				//重新计算
				Solution solution = chip.Slot.MatchAnaliz();
				if(solution != null && solution.count > 0)
				{
					FightMgr.Instance.ListSoution.Add(solution);
					//						Debug.Log("end");
				}
			}
			//Chip new_chip = Instantiate(chip.gameObject).GetComponent<Chip>();
			//new_chip.parentSlot = null;
			//new_chip.transform.position = target.transform.position;
			//target.SetChip(new_chip);

			//chip.HideChip(false);
		}
	}
}
