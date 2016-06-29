using UnityEngine;
using System.Collections;

namespace Fight
{
	// Destroyable blocks on playing field
	public class SpecialBlock : BlockInterface 
	{
//		public int level = 1; // Level of block. From 1 to 3. Each "BlockCrush"-call fall level by one. If it becomes zero, this block will be destroyed.
//		public string[] spritesName; 
//		private UISprite _uiSprite; // Images of blocks of different levels. The size of the array must be equal to 3
//		int eventCountBorn;
//	    Animation anim;
//	    bool destroying = false;


		public void Initialize()
		{
			slot.gravity = false;
////			_uiSprite = GetComponent<UISprite>();
//			eventCountBorn = SessionControl.Instance.eventCount;
//	//		sr.sprite = sprites[level-1];
//	        anim = GetComponent<Animation>();

//			_uiSprite.spriteName = spritesName[index];
		}

//		private Dragon _dragon;

		
		#region implemented abstract members of BlockInterface
		
		// Crush block funtion
		override public void  BlockCrush (bool force) 
		{
			return;

//	        if (destroying)
//	            return;
//			if (eventCountBorn == SessionControl.Instance.eventCount && !force) return;
//			eventCountBorn = SessionControl.Instance.eventCount;
//	//		level --;
//			//FieldAssistant.Instance.field.blocks[slot.Row, slot.Col] = level;
//
//			if (level == 0) 
//			{
//				slot.gravity = true;
////	            slot.SetScore(1);
//	            slot.SetBlock(null);
//	            SlotGravity.Reshading();
//	            StartCoroutine(DestroyingRoutine());
//				return;
//			}
//
//			if (level > 0) 
//			{
////				if(_dragon == null)
////					_dragon = FindObjectOfType<Dragon>();
////
////				if(_dragon == null) return;
////				_dragon.TakeDamage(Dragon.DamageConst);
////	//			anim.Play("BlockCrush");
////	//            AudioAssistant.Shot("BlockHit");
////				_dragon.IsPlayHitAnim = true;//PlayAnim(Dragon.E_DragonAnim.get_hit_front);
////
////	//			sr.sprite = sprites[level-1];
//
//				//MyFrameWork.NetworkManager.Instance.ClientSendFightDataMsg(33);
//				FightControl.Instance.AttackBoss();
////				if(_dragon == null)
////					_dragon = FindObjectOfType<Dragon>();
////
////				if(_dragon == null) return;
//
////				_dragon.IsPlayHitAnim = true;
////				_dragon.PlayAnim(Dragon.E_DragonAnim.get_hit_front);
//
////				//test
////				Protocol.ServerFightResult s2c = new Protocol.ServerFightResult();
////				s2c.result = Protocol.Result.FIGHT_WIN;
////				MyFrameWork.EventDispatcher.TriggerEvent<Protocol.MsgNo, Protocol.ServerFightResult>(Protocol.MsgNo.SERVER_FIGHT_RESULT.ToString(), Protocol.MsgNo.SERVER_FIGHT_RESULT, s2c);
//			}
		}

		public override bool CanBeCrushedByNearSlot () 
		{
			return false;
		}

		#endregion

//	    IEnumerator DestroyingRoutine() 
//		{
//	        destroying = true;
//
////	        GameObject o = ContentAssistant.main.GetItem(crush_effect);
////	        o.transform.position = transform.position;
//
//	        anim.Play("BlockDestroy");
////	        AudioAssistant.Shot("BlockCrush");
//	        while (anim.isPlaying) {
//	            yield return 0;
//	        }
//
//	        Destroy(gameObject);
//	    }
	}
}