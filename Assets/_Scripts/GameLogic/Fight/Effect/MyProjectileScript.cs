using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace FightNew
{
	public class MyProjectileScript : MonoBehaviour 
	{
	    public GameObject impactParticle;
	    public GameObject projectileParticle;
	    public GameObject[] trailParticles;
		public PlayerBase GoTarget;

		public int TargetBossId{get;set;}
		public int CurrentHp{get;set;}
		public int Damage{get;set;}
		public bool IsLastSkill{get;set;}
		public int SkillId{get;set;}


	    [HideInInspector]
	    public Vector3 impactNormal; //Used to rotate impactparticle.
		
		void Start () 
		{
	        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
	        projectileParticle.transform.parent = transform;
		}

		void OnTriggerEnter (Collider hit) 
		{
			if(GoTarget.gameObject != hit.gameObject)
				return;
			
	        //transform.DetachChildren();
	        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
	        //Debug.DrawRay(hit.contacts[0].point, hit.contacts[0].normal * 1, Color.yellow);

//	        if (hit.gameObject.tag == "Destructible") // Projectile will destroy objects tagged as Destructible
//	        {
//	            Destroy(hit.gameObject);
//	        }


			if(!GoTarget.IsOpp)
			{
				//我方
				if(FightMgr.Instance.GameResult == (int)Result.FIGHT_WIN)
				{
					//如果我赢了，敌方对我造成的伤害不算
					CurrentHp += Damage;
					Damage = 0;
				}

				FightMgr.Instance.TakeDamageFinished(TargetBossId,CurrentHp,Damage);
			}
			else
			{
				if(FightMgr.Instance.GameResult == (int)Result.FIGHT_LOSE)
				{
					//如果我输了，我对敌方造成的伤害不算
					CurrentHp += Damage;
					FightMgr.Instance.ModuleFight.CurrentOppBoss.CurrentHp = CurrentHp;
					Damage = 0;
				}

				FightMgr.Instance.AttackFinished(TargetBossId,CurrentHp,Damage);

				if(CurrentHp == 0)
				{
					DebugUtil.Info("hp is 0,IsLastSkill = " + IsLastSkill);
				}
			}

			if(SkillId > 0)
			{
				FightMgr.Instance.PlaySkillEffect(SkillId,GoTarget.IsOpp);
			}

			if(IsLastSkill)
			{
				FightMgr.Instance.IsLastAttackTrigger = true;
				Time.timeScale = 1f;
			}

//			FightMgr.Instance.EffectCount--;
//			if(GoTarget.IsOpp)
//			{
//				Debug.Log("<color=orange>Opp hurt:</color>" + FightMgr.Instance.EffectCount);
//			}
//			else
//			{
//				Debug.Log("<color=orange>My hurt:</color>" + FightMgr.Instance.EffectCount);
//			}

			
	        //yield WaitForSeconds (0.05);
	        foreach (GameObject trail in trailParticles)
		    {
	            GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
	            curTrail.transform.parent = null;
	            Destroy(curTrail, 3f); 
		    }
	        Destroy(projectileParticle, 3f);
	        Destroy(impactParticle, 5f);
	        Destroy(gameObject);

	        //projectileParticle.Stop();

		}
	}
}