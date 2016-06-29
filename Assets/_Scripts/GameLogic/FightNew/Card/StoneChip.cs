using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace FightNew
{
	[RequireComponent (typeof (Card))]
	public class StoneChip : IHelpCard 
	{

		public override void OnInit()
		{
			chip.powerId = 0;
			chip.helpCard = this;
		}

		public override void OnHit() 
		{
			//			chip.hitting = true;
			anim.Play("Hit");
//			if(anim.isPlaying)
//			{
//				anim.CrossFade("Hit",0.2f);
//			}
//			else
//			{
//				anim.Play("Hit");
//			}

			//新生成的
			FightMgr.Instance.ListNewPowerUp.Add(chip);
		}

		// Coroutine destruction / activation
		public override IEnumerator DestroyChipFunction ()
		{
			matching = true;

			#if !FightTest
			MusicManager.Instance.PlaySoundEff("Music/StoneCrush");
			#endif
			anim.Play("Minimizing");

//			yield return new WaitForSeconds(0.1f);
			matching = false;

//			chip.ParentRemove();

			//特效
			GameObject o = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Particle_Stone);
			o.transform.position = transform.position;

			//加能量
			#if !FightTest
//			FightMgr.Instance.AddEnergy(E_AddEnergyType.Stone);
			EnergyMgr.Instance.AddEnergySmall(chip.LastSlot.transform,E_AddEnergyType.Stone);
			#endif

			while (anim.isPlaying) 
				yield return 0;
			
			Destroy(gameObject);
		}
	}
}