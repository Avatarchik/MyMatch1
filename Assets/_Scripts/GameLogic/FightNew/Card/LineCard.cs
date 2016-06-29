using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace FightNew
{
	// The class is responsible for logic SimpleChip
	[RequireComponent (typeof (Card))]
	public class LineCard : IHelpCard 
	{
		private List<Side> FlyOperation = new List<Side>();

		public override void AddSide(Side side)
		{
			if(!FlyOperation.Contains(side))
				FlyOperation.Add(side);
		}

		public override void ClearSide()
		{
			FlyOperation.Clear();
		}

		public override void OnInit()
		{
			chip.powerId = 0;
			chip.helpCard = this;

			//新生成的
			FightMgr.Instance.ListNewPowerUp.Add(chip);
		}

		public override void OnHit() 
		{
//			chip.hitting = true;
			if(anim.isPlaying)
			{
				transform.Find("Chip").GetComponent<UISprite>().alpha = 1f;
			}

			anim.Play("Hit");
//			if(anim.isPlaying)
//			{
//				anim.CrossFade("Hit",0.2f);
//			}
//			else
//			{
//				anim.Play("Hit");
//			}
	    }

		// Coroutine destruction / activation
		public override IEnumerator DestroyChipFunction ()
		{

//			DebugUtil.Debug("delete chip:" + chip.parentSlot.slot.key);
			matching = true;
			//AudioAssistant.Shot("ChipCrush");

			for(int i = 0;i < Utils.allSides.Length;i++)
			{
				if(!FlyOperation.Contains(Utils.allSides[i]))
				{
					Destroy(transform.FindChild(Utils.GetSideName(Utils.allSides[i])).gameObject);
				}	
			}

			anim.Play("CrossBump");

			#if !FightTest
			MusicManager.Instance.PlaySoundEff("Music/match3");
			#endif

	        //OnHit();
			
//			yield return new WaitForSeconds(0.1f);
			
//			chip.RemoveFromSlot();
			matching = false;

//	        if (chip.id >= 0 && chip.id < 6 && SessionAssistant.main.countOfEachTargetCount[chip.id] > 0) {
//	            GameObject go = GameObject.Find("ColorTargetItem" + Chip.chipTypes[chip.id]);
//
//	            if (go) {
//	                Transform target = go.transform;
//	                
//	                sprite.sortingLayerName = "UI";
//	                sprite.sortingOrder = 10;
//
//	                float time = 0;
//	                float speed = Random.Range(1f, 1.8f);
//	                Vector3 startPosition = transform.position;
//	                Vector3 targetPosition = target.position;
//
//	                while (time < 1) {
//	                    transform.position = Vector3.Lerp(startPosition, targetPosition, EasingFunctions.easeInOutQuad(time));
//	                    time += Time.unscaledDeltaTime * speed;
//	                    yield return 0;
//	                }
//
//	                transform.position = target.position;
//	            }
//	        }       
	        
//	        anim.Play("Minimizing");
//			ParticleMgr.Instance.AddParticle(this.transform.position,(int)chip.CardType);

	        while (anim.isPlaying)
	            yield return 0;

	        Destroy(gameObject);

		}
	}
}