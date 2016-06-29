/*
 * 
 * 文件名(File Name)：             SlotForCard
 *
 * 作者(Author)：                  Yangzj
 *
 * 创建时间(CreateTime):           2016/04/04 13:38:42
 *
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Fight
{
	public class SlotForCard : MonoBehaviour 
	{
		public Card card;
		public Slot slot;

		public Slot this[Side index] 
		{ 	// access to neighby slots on the index
			get 
			{
				return slot.nearSlot[index];
			}
		}

		void  Awake ()
		{
			slot = GetComponent<Slot>();
		}

		public Card GetCard ()
		{
			return card;
		}

		// function of assigning chip to the slot
		public void  SetChip (Card c)
		{
			if (card) 
			{
//				DebugUtil.Debug("set paretn null22:" + card.parentSlot.slot.key);
				card.parentSlot = null;
			}

			card = c;
			card.transform.SetParent(transform,false);//.parent = transform;
			card.transform.localPosition = Vector3.zero;
			if (card.parentSlot) 
			{
				card.parentSlot.card = null;
				if(FieldAssistant.Instance.field != null)
					FieldAssistant.Instance.field.chips[card.parentSlot.slot.Row, card.parentSlot.slot.Col] = -1;
				else
					return;
			}

			card.parentSlot = this;
			FieldAssistant.Instance.field.chips[slot.Row, slot.Col] = card.id;
		}

		public void  SetChipTemp (Card c)
		{
			if (card) 
			{
//				DebugUtil.Debug("set paretn null44:" + card.parentSlot.slot.key);
				card.parentSlot = null;
			}

			card = c;
			card.transform.SetParent(transform);//.parent = transform;
			//card.transform.localPosition = Vector3.zero;
			if (card.parentSlot) {
				card.parentSlot.card = null;
				if(FieldAssistant.Instance.field != null)
					FieldAssistant.Instance.field.chips[card.parentSlot.slot.Row, card.parentSlot.slot.Col] = -1;
				else
					return;
			}

			card.parentSlot = this;
			FieldAssistant.Instance.field.chips[slot.Row, slot.Col] = card.id;
		}

		public void  CrushCard ()
		{
			card.DestroyChip();
			card = null;
		}


		public Card GetChip ()
		{
			return card;
		}

		#region ToDo
		// Analysis of chip for combination
		public SessionControl.Solution MatchAnaliz (bool checkPotential = true)
		{

			if (!GetChip()) return null;
			if (!GetChip().IsMatcheble()) return null;
			if (GetChip().id < 0) return null;


			if (GetChip().id == 10) 
			{ // multicolor
				List<SessionControl.Solution> solutions = new List<SessionControl.Solution>();
				SessionControl.Solution z;
				Card multicolorChip = GetChip();
				for (int i = 0; i < 6; i++) 
				{
					multicolorChip.id = i;
					z = MatchAnaliz();
					if (z != null)
						solutions.Add(z);
//					z = MatchSquareAnaliz();
//					if (z != null)
//						solutions.Add(z);
				}
				multicolorChip.id = 10;
				z = null;
				foreach (SessionControl.Solution sol in solutions)
					if (z == null || z.potential < sol.potential)
						z = sol;
				return z;
			}

			Slot s;
			Dictionary<Side, List<Card>> sides = new Dictionary<Side, List<Card>>();
			int count;
			string key;
			foreach (Side side in Utils.straightSides) 
			{
				count = 1;
				sides.Add(side, new List<Card>());
				while (true) 
				{
					key = string.Format(Slot.SlotKeyFormat,(slot.Row + Utils.SideOffsetX(side) * count),(slot.Col + Utils.SideOffsetY(side) * count));

					s = SlotManager.Instance.FindSlot(key);
					if(s == null)
						break;
					if (!s.GetChip())
						break;
					if (s.GetChip().id != card.id && s.GetChip().id != 10)
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
				SessionControl.Solution solution = new SessionControl.Solution();

				solution.h = h;
				solution.v = v;

				solution.chips = new List<Card>();
				solution.chips.Add(GetChip());

				if (h) {
					solution.chips.AddRange(sides[Side.Right]);
					solution.chips.AddRange(sides[Side.Left]);
				}
				if (v) {
					solution.chips.AddRange(sides[Side.Top]);
					solution.chips.AddRange(sides[Side.Bottom]);
				}

				solution.count = solution.chips.Count;

				solution.x = slot.Row;
				solution.y = slot.Col;
				solution.id = card.id;

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
//
//		public SessionAssistant.Solution MatchSquareAnaliz() {
//
//			if (!SessionAssistant.main.squareCombination)
//				return null;
//			if (!GetChip())
//				return null;
//			if (!GetChip().IsMatcheble())
//				return null;
//			if (GetChip().id < 0)
//				return null;
//
//
//			if (GetChip().id == 10) { // multicolor
//				List<SessionAssistant.Solution> solutions = new List<SessionAssistant.Solution>();
//				SessionAssistant.Solution z;
//				Chip multicolorChip = GetChip();
//				for (int i = 0; i < 6; i++) {
//					multicolorChip.id = i;
//					z = MatchSquareAnaliz();
//					if (z != null)
//						solutions.Add(z);
//				}
//				multicolorChip.id = 10;
//				z = null;
//				foreach (SessionAssistant.Solution sol in solutions)
//					if (z == null || z.potential < sol.potential)
//						z = sol;
//				return z;
//			}
//
//			List<Chip> square = new List<Chip>();
//			List<Chip> buffer = new List<Chip>();
//			Side sideR;
//			string key;
//			Slot s;
//
//
//			buffer.Clear();
//			foreach (Side side in Utils.straightSides) {
//				for (int r = 0; r <= 2; r++) {
//					sideR = Utils.RotateSide(side, r);
//					key = (slot.x + Utils.SideOffsetX(sideR)).ToString() + "_" + (slot.y + Utils.SideOffsetY(sideR)).ToString();
//					if (Slot.all.ContainsKey(key)) {
//						s = Slot.all[key];
//						if (s.GetChip() && (s.GetChip().id == chip.id || s.GetChip().id == 10) && s.GetChip().IsMatcheble())
//							buffer.Add(s.GetChip());
//						else
//							break;
//					} else
//						break;
//				}
//				if (buffer.Count == 3) {
//					foreach (Chip chip_b in buffer)
//						if (!square.Contains(chip_b))
//							square.Add(chip_b);
//				}
//				buffer.Clear();
//			}
//
//
//			bool q = square.Count >= 3;
//
//			if (q) {
//				SessionAssistant.Solution solution = new SessionAssistant.Solution();
//
//				solution.q = q;
//
//				solution.chips = new List<Chip>();
//				solution.chips.Add(GetChip());
//
//				solution.chips.AddRange(square);
//
//				solution.count = solution.chips.Count;
//
//				solution.x = slot.x;
//				solution.y = slot.y;
//				solution.id = chip.id;
//
//				foreach (Chip c in solution.chips)
//					solution.potential += c.GetPotencial();
//
//				return solution;
//			}
//			return null;
//		}

		#endregion
	}
}
