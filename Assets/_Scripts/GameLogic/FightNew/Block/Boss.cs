using UnityEngine;
using System.Collections;

namespace FightNew
{
	// Destroyable blocks on playing field
	public class Boss : BlockInterface 
	{
		public int level = 1; // Level of block. From 1 to 3. Each "BlockCrush"-call fall level by one. If it becomes zero, this block will be destroyed.
		public string[] spritesName; 
		private UISprite _uiSprite; // Images of blocks of different levels. The size of the array must be equal to 3
		int eventCountBorn;
	    Animation anim;
	    bool destroying = false;
		public const string prefabName = "Boss";

		public void Initialize(int index)
		{
			slot.gravity = false;
			_uiSprite = GetComponent<UISprite>();
			eventCountBorn = FightMgr.Instance.eventCount;
	//		sr.sprite = sprites[level-1];
	        anim = GetComponent<Animation>();

			_uiSprite.spriteName = spritesName[index];
		}

		
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

			if (level == 0) 
			{
				slot.gravity = true;
//	            slot.SetScore(1);
	            slot.SetBlock(null);
	            SlotGravity.Reshading();
	            StartCoroutine(DestroyingRoutine());
				return;
			}

			if (level > 0) 
			{
//				#if !FightTest
//				FightMgr.Instance.AttackBoss();
//				#endif

				#if !FightTest
				E_AddEnergyType addType = isDirect ? E_AddEnergyType.Powup : E_AddEnergyType.Normal;
//				FightMgr.Instance.AddEnergy(addType);
				EnergyMgr.Instance.AddEnergySmall(parent,addType);
				#endif
			}
		}

		public override bool CanBeCrushedByNearSlot () 
		{
			return true;
		}

		#endregion

	    IEnumerator DestroyingRoutine() 
		{
	        destroying = true;

//	        GameObject o = ContentAssistant.main.GetItem(crush_effect);
//	        o.transform.position = transform.position;

	        anim.Play("BlockDestroy");
//	        AudioAssistant.Shot("BlockCrush");
	        while (anim.isPlaying) 
			{
	            yield return 0;
	        }

	        Destroy(gameObject);
	    }
	}
}