using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace FightNew
{
	// The class is responsible for logic SimpleChip
	[RequireComponent (typeof (Card))]
	public class SimpleCard : IHelpCard 
	{
		public override void OnInit()
		{
			chip.powerId = 0;
			chip.helpCard = this;
		}

		public override void OnHit() 
		{
//			chip.hitting = true;
//			if(anim.isPlaying)
//			{
//				anim.CrossFade("Hit",0.2f);
//			}
//			else
//			{
//				anim.Play("Hit");
//			}
			anim.Play("Hit");
	    }

		// Coroutine destruction / activation
		public override IEnumerator DestroyChipFunction ()
		{

//			DebugUtil.Debug("delete chip:" + chip.parentSlot.slot.key);
			matching = true;
			//AudioAssistant.Shot("ChipCrush");

			#if !FightTest
			MusicManager.Instance.PlaySoundEff("Music/match3");
			#endif

	        //OnHit();
			
			yield return new WaitForSeconds(0.02f);
			
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
	        
	        anim.Play("Minimizing");
			ParticleMgr.Instance.AddParticle(this.transform.position,(int)chip.CardType);

	        while (anim.isPlaying)
	            yield return 0;

	        Destroy(gameObject);

		}
	}
}