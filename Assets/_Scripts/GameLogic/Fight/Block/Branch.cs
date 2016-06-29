using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace Fight
{
	public class Branch : BlockInterface 
	{
		public const string crushEffect = "BranchCrush";
		public const string prefabName = "Branch";
		int eventCountBorn = 0;
		bool destroying = false;
		SlotGravity gravity;
		
		void Start () 
		{
			transform.rotation = Quaternion.Euler (0, 0, Random.Range (0f, 360f));	
		}

		#region implemented abstract members of BlockInterface

	    public override void BlockCrush(bool force) 
		{
			if (eventCountBorn == SessionControl.Instance.eventCount && !force) return;
			if (destroying) return;
			eventCountBorn = SessionControl.Instance.eventCount;
			GameObject o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(crushEffect);
			o.transform.position = transform.position;
	        slot.gravity = true;
//	        slot.SetScore(1);
			SlotGravity.Reshading();
			Destroy(gameObject);
			return;
		}

		public override bool CanBeCrushedByNearSlot () {
			return false;
		}

		#endregion

		public void Initialize () {
			gravity = slot.GetComponent<SlotGravity> ();
			slot.gravity = false;
			gravity.enabled = false;
			eventCountBorn = SessionControl.Instance.eventCount;
		}

		void OnDestroy() 
		{
			gravity.enabled = true;
		}
	}
}
