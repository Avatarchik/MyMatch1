using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Fight
{
	// The class is responsible for logic ColorBomb
	[RequireComponent(typeof(Card))]
	public class UltraColorBomb : IBomb 
	{

		Card chip;
	    int birth; // Event count at the time of birth SessionAssistant.main.eventCount
	    Animation anim;
	    bool mMatching = false;
	    bool matching {
	        set {
	            if (value == mMatching)
	                return;
	            mMatching = value;
	            if (mMatching)
					SessionControl.Instance.matching++;
	            else
					SessionControl.Instance.matching--;
	        }

	        get {
	            return mMatching;
	        }
	    }
	    
	    void OnDestroy() {
	        matching = false;
	    }

	    void Awake() 
		{
			chip = GetComponent<Card>();
	        chip.chipType = "UltraColorBomb";
	        anim = GetComponent<Animation>();
			birth = SessionControl.Instance.eventCount;
	        //AudioAssistant.Shot("CreateColorBomb");
	    }

	    // Coroutine destruction / activation
	    IEnumerator DestroyChipFunction() 
		{
			if (birth == SessionControl.Instance.eventCount) {
	            chip.destroying = false;
	            yield break;
	        }


	        SimpleChip[] chips = FindObjectsOfType<SimpleChip>();
	        if (chips.Length == 0)
	            yield break;
	        chip.id = chips[Random.Range(0, chips.Length)].chip.id;
	        chip.chipType = "SimpleChip";
	        yield return StartCoroutine(UltraColorMixRoutine(chip));
	    }

		public List<Card> GetDangeredChips(List<Card> stack) {
	        if (stack.Contains(chip))
	            return stack;

	        stack.Add(chip);

			int sx = chip.parentSlot.slot.Row;
			int sy = chip.parentSlot.slot.Col;

	        Slot s;

			for (int x = 0; x < FieldAssistant.Instance.field.width; x++) {
				for (int y = 0; y < FieldAssistant.Instance.field.height; y++) {
	                if (y == sy && x == sx)
	                    continue;
					s = SlotManager.Instance.FindSlot(x, y);
	                if (s && s.GetChip() && s.GetChip().id == chip.id) {
	                    stack = s.GetChip().GetDangeredChips(stack);
	                }
	            }
	        }
	        return stack;
	    }

	    #region Mixes
	    void UltraColorMix(Card secondary) {
	        StartCoroutine(UltraColorMixRoutine(secondary));
	    }

		IEnumerator UltraColorMixRoutine(Card secondary) 
		{
	        matching = true;
	        chip.destroyable = false;

	        anim.Play("UltraColorBump");
//	        AudioAssistant.Shot("ColorBombCrush");

			int width = FieldAssistant.Instance.field.width;
			int height = FieldAssistant.Instance.field.height;

			int sx = chip.parentSlot.slot.Row;
			int sy = chip.parentSlot.slot.Col;

	        Slot s;

//			FieldAssistant.Instance.JellyCrush(sx, sy);

	        Color color = Color.black;
	        if (secondary.id == Mathf.Clamp(0, 5, secondary.id))
				color = Card.colors[secondary.id];


			List<Card> target = new List<Card>();
	        for (int x = 0; x < width; x++) {
	            for (int y = 0; y < height; y++) {
	                if (y == sy && x == sx)
	                    continue;
					s = SlotManager.Instance.FindSlot(x, y);
	                if (s == null || s.GetChip() == null || s.GetChip() == secondary || s.GetChip().chipType != "SimpleChip")
	                    continue;
	                if (secondary.chipType == "UltraColorBomb" || s.GetChip().id == secondary.id) 
					{
	                    if (secondary.chipType != "SimpleChip" && secondary.chipType != "UltraColorBomb") 
						{
							Card pu = FieldAssistant.Instance.AddPowerup(x, y, secondary.chipType);
	                        pu.can_move = false;
	                    }
	                    yield return new WaitForSeconds(0.02f);
	                    if (s.GetChip()) {
	                        Lightning.CreateLightning(3, transform, s.GetChip().transform, color != Color.black ? color : Card.colors[s.GetChip().id]);
	                        target.Add(s.GetChip());
	                    }
	                }
	            }
	        }

	        yield return new WaitForSeconds(0.1f);

			SessionControl.Instance.EventCounter();
	        foreach(Card t in target) {
	            if (t.destroying)
	                continue;
//	            t.SetScore(0.3f);
				FieldAssistant.Instance.BlockCrush(t.parentSlot.slot.Row, t.parentSlot.slot.Col, true);
//				FieldAssistant.Instance.JellyCrush(t.parentSlot.slot.Row, t.parentSlot.slot.Col);
	            t.DestroyChip();
	            yield return new WaitForSeconds(0.02f);
	        }

	        yield return new WaitForSeconds(0.1f);

//			FieldAssistant.Instance.JellyCrush(sx, sy);

	        while (anim.isPlaying)
	            yield return 0;

	        matching = false;

	        chip.ParentRemove();
	        chip.HideChip(false);
	    }
	    #endregion
	}
}