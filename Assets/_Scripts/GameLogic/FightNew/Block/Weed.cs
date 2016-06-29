using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace FightNew
{
	public class Weed : BlockInterface 
	{
//
//
//		public static List<Weed> all1 = new List<Weed>();
//		public static List<Weed> all2 = new List<Weed>();
//		public static List<Weed> all3 = new List<Weed>();
//
//		public static void Clear()
//		{
//			all1.Clear();
//			all2.Clear();
//			all3.Clear();
//		}
//
//		public static int WeedCount
//		{
//			get
//			{
//				return all1.Count + all2.Count + all3.Count;
//			}
//		}
		public static List<Weed> all = new List<Weed>();
//		{
//			get
//			{
//				if(FightControl.Instance.EFightStatus == E_FightStatus.FightBoss1
//					|| FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss1)
//				{
//					return all1;
//				}
//				else if(FightControl.Instance.EFightStatus == E_FightStatus.FightBoss2
//					|| FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss2)
//				{
//					return all2;
//				}
//				else if(FightControl.Instance.EFightStatus == E_FightStatus.FightBoss3
//					|| FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss3)
//				{
//					return all3;
//				}
//				else
//				{
////					DebugUtil.Error("not get weed dic:" + FightControl.Instance.EFightStatus);
//					return null;
//				}
//			}
//		}
//
		int eventCountBorn;

		bool destroying = false;

	    Animation anim;

		public static int seed = 0;
	    public static int lastWeedCrush = 0;

		void Start () 
		{
	        anim = GetComponent<Animation>();
	    }
//
		public void Initialize ()
		{
			slot.gravity = false;
			eventCountBorn = FightMgr.Instance.eventCount;
			all.Add(this);
		}	

		#region implemented abstract members of BlockInterface
		
		// Crush block funtion
		override public void BlockCrush(bool force,Transform parent,bool isDirect = false,E_CardType cardType = E_CardType.None) 
		{
			if (eventCountBorn == FightMgr.Instance.eventCount && !force) return;
			if (destroying) return;

			lastWeedCrush = FightMgr.Instance.swapEvent;

			eventCountBorn = FightMgr.Instance.eventCount;

//	        slot.SetScore(1);
	        StartCoroutine(DestroyingRoutine());
		}

		public override bool CanBeCrushedByNearSlot () 
		{
			return true;
		}

		#endregion

		void OnDestroy () 
		{
			if(all != null && all.Contains(this))
				all.Remove (this);
		}

		/// <summary>
		/// 杂草增长
		/// </summary>
		public static void Grow () 
		{
			List<Slot> slots = new List<Slot> ();

			Weed weed = null;
			for(int i = 0;i < all.Count;i++)
			{
				weed = all[i];
				for(int j = 0;j < Utils.straightSides.Length;j++)
				{
					Side side = Utils.straightSides[j];

					if (weed.slot[side] && !weed.slot[side].Block)// && !(weed.slot[side].GetChip() && weed.slot[side].GetChip().chipType == "SugarChip"))
						slots.Add(weed.slot[side]);
				}
			}

	        while (seed > 0) 
			{
			    if (slots.Count == 0) return;
	            
			    Slot target = slots[Random.Range(0, slots.Count)];
	            slots.Remove(target);

			    if (target.GetChip())
				    target.GetChip().HideChip(false);

				Weed newWeed = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Weed).GetComponent<Weed>();
			    newWeed.transform.position = target.transform.position;
			    newWeed.name = "New_Weed";
				newWeed.transform.SetParent(target.transform,false);
			    target.SetBlock(newWeed);
			    newWeed.slot = target;
				#if !FightTest
				MusicManager.Instance.PlaySoundEff("Music/WeedCreate");
				#endif
	            newWeed.Initialize();

	            seed--;
	        }
		}

	    IEnumerator DestroyingRoutine() 
		{
	        destroying = true;

			GameObject o = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Particle_WeedCrush);
	        o.transform.position = transform.position;

			#if !FightTest
			MusicManager.Instance.PlaySoundEff("Music/WeedCrushMusic");
			#endif
	        anim.Play("JellyDestroy");

			slot.gravity = true;
			slot.SetBlock(null);
			SlotGravity.Reshading();

	        while (anim.isPlaying) 
			{
	            yield return 0;
	        }

	       
	        Destroy(gameObject);
	    }
	}
}