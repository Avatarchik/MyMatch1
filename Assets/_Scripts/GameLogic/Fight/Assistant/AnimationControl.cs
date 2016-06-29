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

namespace Fight
{
	public class AnimationControl : Singleton<AnimationControl>
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

		// The function of swapping 2 chips
		public void SwapTwoItem (Card a, Card b, bool force) 
		{
			if (!SessionControl.Instance.isPlaying
				|| !FightControl.Instance.IsFighting
			) return;

			CoroutineControl.Instance.StartCoroutine(SwapTwoItemRoutine(a, b, force));
		}

		// Function immediate swapping 2 chips
		public void SwapTwoItemNow (Card a, Card b) 
		{
			if (!a || !b) return;
			if (a == b) return;
			if (a.parentSlot.slot.Block || b.parentSlot.slot.Block) return;

			//Vector3 posA = a.parentSlot.transform.position;
			//Vector3 posB = b.parentSlot.transform.position;

			//a.transform.position = posB;
			//b.transform.position = posA;

			a.movementID = SessionControl.Instance.GetMovementID();
			b.movementID = SessionControl.Instance.GetMovementID();

			SlotForCard slotA = a.parentSlot;
			SlotForCard slotB = b.parentSlot;

			slotB.SetChip(a);
			slotA.SetChip(b);
		}

		// Coroutine swapping 2 chips
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

			if (a.parentSlot.slot.Block != null|| b.parentSlot.slot.Block != null) 
			{
				a.SetBorder(false);
				b.SetBorder(false);

				yield break; // If one of the chips is blocked
			}

			if (!SessionControl.Instance.CanIAnimate()) 
			{
				a.SetBorder(false);
				b.SetBorder(false);

				yield break; // If the core prohibits animation
			}

			if (SessionControl.Instance.movesCount <= 0)
			{
				a.SetBorder(false);
				b.SetBorder(false);

				FightControl.Instance.ShowMsg(FightDefine.E_NoteMsgType.NoMoves);
				yield break; // If not enough moves
			}
			if (SessionControl.Instance.timeLeft <= 0) 
			{
				a.SetBorder(false);
				b.SetBorder(false);

				yield break; // If not enough time
			}
		

			SessionControl.Mix mix = SessionControl.Instance.mixes.Find(x => x.Compare(a.chipType, b.chipType));

			int move = 0; // Number of points movement which will be expend

			SessionControl.Instance.animate++;
			swaping = true;

			Vector3 posA = a.parentSlot.transform.position;
			Vector3 posB = b.parentSlot.transform.position;

			float progress = 0;

			Vector3 normal = a.parentSlot.slot.Row == b.parentSlot.slot.Row ? Vector3.right : Vector3.up;
			float time = 0;
			// Animation swapping 2 chips	
			while (progress < swapDuration) 
			{
				if (a == null || b == null)
				{
					SessionControl.Instance.animate--;
					swaping = false;

					if(a)
						a.SetBorder(false);

					if(b)
						b.SetBorder(false);
					
					yield break;
				}

				time = EasingFunctions.easeInOutQuad(progress / swapDuration);
				a.transform.position = Vector3.Lerp(posA, posB, time) + normal * Mathf.Sin(3.14f * time) * 0.05f;
				if (mix == null) b.transform.position = Vector3.Lerp(posB, posA, time) - normal * Mathf.Sin(3.14f * time) * 0.05f;

				progress += Time.deltaTime;

				yield return 0;
			}

			a.transform.position = posB;
			if (SessionControl.Instance.movesCount <= 0 || mix == null) b.transform.position = posA;

			a.movementID = SessionControl.Instance.GetMovementID();
			b.movementID = SessionControl.Instance.GetMovementID();

			if (SessionControl.Instance.movesCount > 0 && mix != null) 
			{ // Scenario mix effect
				swaping = false;
				SessionControl.Instance.MixChips(a, b);
				a.SetBorder(false);
				b.SetBorder(false);

				yield return new WaitForSeconds(0.3f);
				SessionControl.Instance.movesCount--;
				SessionControl.Instance.animate--;



				SessionControl.Instance.SuccessMoveCounter();
				yield break;
			}

			// Scenario the effect of swapping two chips
			SlotForCard slotA = a.parentSlot;
			SlotForCard slotB = b.parentSlot;
			slotB.SetChip(a);
			slotA.SetChip(b);

			move++;

			// searching for solutions of matching
			int count = 0; 
			SessionControl.Solution solution;

			//todo yangzj
			solution = slotA.MatchAnaliz(false);
			if (solution != null) count += solution.count;

//			solution = slotA.MatchSquareAnaliz();
//			if (solution != null) count += solution.count;

			solution = slotB.MatchAnaliz(false);
			if (solution != null) count += solution.count;

//			solution = slotB.MatchSquareAnaliz();
//			if (solution != null) count += solution.count;

			// Scenario canceling of changing places of chips
			if (SessionControl.Instance.movesCount <= 0 || (count == 0 && !force))
			{
				if(SessionControl.Instance.movesCount <= 0)
				{
					//显示不能移动提示
					FightControl.Instance.ShowMsg(FightDefine.E_NoteMsgType.NoMoves);
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

				a.movementID = SessionControl.Instance.GetMovementID();
				b.movementID = SessionControl.Instance.GetMovementID();

				slotB.SetChip(b);
				slotA.SetChip(a);

				a.transform.Find("Border").gameObject.SetActive(false);
				b.transform.Find("Border").gameObject.SetActive(false);

				move--;
			} 
			else 
			{
//				AudioAssistant.Shot("SwapSuccess");
				SessionControl.Instance.swapEvent ++;

				SessionControl.Instance.SuccessMoveCounter();
			}

//			SessionControl.Instance.firstChipGeneration = false;

			SessionControl.Instance.movesCount -= move;
			SessionControl.Instance.EventCounter ();

			SessionControl.Instance.animate--;

			a.SetBorder(false);
			b.SetBorder(false);

			swaping = false;
		}

		// Function of creating of explosion effect
		public void  Explode (Vector3 center, float radius, float force){
			Card[] chips = GameObject.FindObjectsOfType<Card>();
			Vector3 impuls;
			foreach(Card chip in chips) 
			{
				if ((chip.transform.parent.localPosition - center).sqrMagnitude > radius * radius) 
				{
//					DebugUtil.Info("fail:" + chip.transform.parent.localPosition + "," + chip.transform.parent.name);
					continue;
				}
//				else
//				{
//					DebugUtil.Info("success:" + chip.transform.parent.localPosition + "," + chip.transform.parent.name);
//				}
				impuls = (chip.transform.parent.localPosition - center) * force;
				impuls *= Mathf.Pow((radius - (chip.transform.parent.localPosition - center).magnitude) / radius, 2);
				chip.impulse += impuls;
			}
		}

		public void TeleportChip(Card chip, Slot target) 
		{
			CoroutineControl.Instance.StartCoroutine (TeleportChipRoutine (chip, target));
		}

		IEnumerator TeleportChipRoutine (Card chip, Slot target) {
			if (!chip.parentSlot) yield break;
			if (chip.destroying) yield break;
			if (target.GetChip() || !target.gravity) yield break;

			Vector3 scale_target = Vector3.zero;
			chip.can_move = false;
			target.SetChip(chip);


			scale_target.z = 1;
			while (chip != null && chip.transform.localScale.x > 0) {
				chip.transform.localScale = Vector3.MoveTowards(chip.transform.localScale, scale_target, Time.deltaTime * 8);
				yield return 0;
			}

			chip.transform.localPosition = Vector3.zero;
			scale_target.x = 1;
			scale_target.y = 1;
			while (chip != null && chip.transform.localScale.x < 1) {
				chip.transform.localScale = Vector3.MoveTowards(chip.transform.localScale, scale_target, Time.deltaTime * 12);
				yield return 0;
			}

			if(chip != null)
				chip.can_move = true;
			//Chip new_chip = Instantiate(chip.gameObject).GetComponent<Chip>();
			//new_chip.parentSlot = null;
			//new_chip.transform.position = target.transform.position;
			//target.SetChip(new_chip);

			//chip.HideChip(false);
		}
	}
}
