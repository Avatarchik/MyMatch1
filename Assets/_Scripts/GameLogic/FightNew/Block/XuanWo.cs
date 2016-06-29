using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace FightNew
{
	// Destroyable blocks on playing field
	public class XuanWo : BlockInterface 
	{
		public int level = 1; // Level of block. From 1 to 3. Each "BlockCrush"-call fall level by one. If it becomes zero, this block will be destroyed.
		public Particle _parNormal;
		public Particle _parEffect;


		int eventCountBorn;
	    Animation anim;
	    bool destroying = false;

		public void Initialize(int level)
		{
			slot.gravity = false;

			eventCountBorn = FightMgr.Instance.eventCount;
	        anim = GetComponent<Animation>();

//			OnBeginStartTime();
		}

		private Transform _nodeTrans;

		private bool _isActive = false;

		/// <summary>
		/// 经过的时间
		/// </summary>
		private uint _passedTime;

		#region implemented abstract members of BlockInterface
		
		// Crush block funtion
		override public void  BlockCrush (bool force,Transform parent,bool isDirect = false,E_CardType cardType = E_CardType.None) 
		{
	        if (destroying)
	            return;
			if (eventCountBorn == FightMgr.Instance.eventCount && !force) return;
			eventCountBorn = FightMgr.Instance.eventCount;
	//		level --;
			//FieldAssistant.Instance.field.blocks[slot.Row, slot.Col] = level;

//			if (level == 0) 
//			{
//				slot.gravity = true;
////	            slot.SetScore(1);
//	            slot.SetBlock(null);
//	            SlotGravity.Reshading();
//	            StartCoroutine(DestroyingRoutine());
//				return;
//			}



//				#if !FightTest
//				FightMgr.Instance.AttackBoss();
//				#endif

		}

		private float _timerSpeed = 0f;
		private int _targetAngle = 0;


		public override bool CanBeCrushedByNearSlot () 
		{
			return false;
		}

		#endregion

	}
}