using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace Fight
{
	public class Weed : BlockInterface 
	{
		/// <summary>
		/// 破碎名称
		/// </summary>
		public const string crushEffect = "WeedCrush";
		public const string prefabName = "Weed";

		public static List<Weed> all1 = new List<Weed>();
		public static List<Weed> all2 = new List<Weed>();
		public static List<Weed> all3 = new List<Weed>();

		public static void Clear()
		{
			all1.Clear();
			all2.Clear();
			all3.Clear();
		}

		public static int WeedCount
		{
			get
			{
				return all1.Count + all2.Count + all3.Count;
			}
		}
		public static List<Weed> all
		{
			get
			{
				if(FightControl.Instance.EFightStatus == E_FightStatus.FightBoss1
					|| FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss1)
				{
					return all1;
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.FightBoss2
					|| FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss2)
				{
					return all2;
				}
				else if(FightControl.Instance.EFightStatus == E_FightStatus.FightBoss3
					|| FightControl.Instance.EFightStatus == E_FightStatus.LoadingBoss3)
				{
					return all3;
				}
				else
				{
//					DebugUtil.Error("not get weed dic:" + FightControl.Instance.EFightStatus);
					return null;
				}
			}
		}

		int eventCountBorn;

		bool destroying = false;

	    Animation anim;

		public static int seed = 0;
	    public static int lastWeedCrush = 0;

		void Start () {
	        anim = GetComponent<Animation>();
	    }

		public void Initialize ()
		{
			slot.gravity = false;
			eventCountBorn = SessionControl.Instance.eventCount;
			all.Add (this);
		}	

		#region implemented abstract members of BlockInterface
		
		// Crush block funtion
	    override public void BlockCrush(bool force) 
		{
			if (eventCountBorn == SessionControl.Instance.eventCount && !force) return;
			if (destroying) return;

			lastWeedCrush = SessionControl.Instance.swapEvent;

			eventCountBorn = SessionControl.Instance.eventCount;

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

		public static void Grow () 
		{
			List<Slot> slots = new List<Slot> ();

			foreach (Weed weed in all)
				foreach (Side side in Utils.straightSides)
					if (weed.slot[side] && !weed.slot[side].Block && !(weed.slot[side].GetChip() && weed.slot[side].GetChip().chipType == "SugarChip"))
						slots.Add(weed.slot[side]);

	        while (seed > 0) 
			{
			    if (slots.Count == 0) return;
	            
			    Slot target = slots[Random.Range(0, slots.Count)];
	            slots.Remove(target);

			    if (target.GetChip())
				    target.GetChip().HideChip(false);

				Weed newWeed = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(prefabName).GetComponent<Weed>();
			    newWeed.transform.position = target.transform.position;
			    newWeed.name = "New_Weed";
				newWeed.transform.SetParent(target.transform,false);
			    target.SetBlock(newWeed);
			    newWeed.slot = target;
//	            AudioAssistant.Shot("WeedCreate");
	            newWeed.Initialize();

	            seed--;
	        }
		}

	    IEnumerator DestroyingRoutine() 
		{
	        destroying = true;

			GameObject o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(crushEffect);
	        o.transform.position = transform.position;

//	        AudioAssistant.Shot("WeedCrush");
	        anim.Play("JellyDestroy");
	        while (anim.isPlaying) 
			{
	            yield return 0;
	        }

	        slot.gravity = true;
	        slot.SetBlock(null);
	        SlotGravity.Reshading();
	        Destroy(gameObject);
	    }
	}
}