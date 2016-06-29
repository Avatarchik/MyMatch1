using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace FightNew
{
	// Destroyable blocks on playing field
	public class Block : BlockInterface 
	{

		public int level = 1; // Level of block. From 1 to 3. Each "BlockCrush"-call fall level by one. If it becomes zero, this block will be destroyed.
		public string[] spritesName; // Images of blocks of different levels. The size of the array must be equal to 3
		//SpriteRenderer sr;
		private UISprite _uiSprite;
		int eventCountBorn;
	    Animation anim;
	    bool destroying = false;


		public void Initialize ()
		{
			slot.gravity = false;
			_uiSprite = GetComponent<UISprite>();
			eventCountBorn = FightMgr.Instance.eventCount;
			_uiSprite.spriteName = spritesName[level - 1];
	        anim = GetComponent<Animation>();
		}

		
		#region implemented abstract members of BlockInterface
		
		// Crush block funtion
		override public void  BlockCrush (bool force,Transform parent,bool isDirect = false,E_CardType cardType = E_CardType.None) 
		{
	        if (destroying)
	            return;
			if (eventCountBorn == FightMgr.Instance.eventCount && !force) return;

			eventCountBorn = FightMgr.Instance.eventCount;
			level --;
			FieldMgr.field.blocks[slot.Point.X, slot.Point.Y] = level;
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
				anim.Play("BlockCrush");
				#if !FightTest
				MusicManager.Instance.PlaySoundEff("Music/BlockHit");
				#endif
				_uiSprite.spriteName = spritesName[level-1];
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

			GameObject o = FightMgr.Instance.LoadAndInstantiate(FightDefine.Prefab_Particle_BlockCrush);//ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(crushEffect);
	        o.transform.position = transform.position;
//			Debug.Log("Block Pos:" + o.transform.position);

	        anim.Play("BlockDestroy");
			#if !FightTest
			MusicManager.Instance.PlaySoundEff("Music/BlockCrushMusic");
			#endif
	        while (anim.isPlaying) 
			{
	            yield return 0;
	        }

	        Destroy(gameObject);
	    }
	}
}