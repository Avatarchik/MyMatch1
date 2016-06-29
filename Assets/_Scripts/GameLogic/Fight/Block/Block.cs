using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace Fight
{
	// Destroyable blocks on playing field
	public class Block : BlockInterface 
	{
		/// <summary>
		/// 破碎名称
		/// </summary>
		public const string crushEffect = "BlockCrush";
		public const string prefabName = "Block";

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
			eventCountBorn = SessionControl.Instance.eventCount;
			_uiSprite.spriteName = spritesName[level - 1];
	        anim = GetComponent<Animation>();
		}

		
		#region implemented abstract members of BlockInterface
		
		// Crush block funtion
		override public void  BlockCrush (bool force) 
		{
	        if (destroying)
	            return;
			if (eventCountBorn == SessionControl.Instance.eventCount && !force) return;

			eventCountBorn = SessionControl.Instance.eventCount;
			level --;
			FieldAssistant.Instance.field.blocks[slot.Row, slot.Col] = level;
			if (level == 0) 
			{
				slot.gravity = true;
//	            slot.SetScore(1);
	            slot.SetBlock(null);
	            SlotGravity.Reshading();
	            StartCoroutine(DestroyingRoutine());
				return;
			}
			if (level > 0) {
				anim.Play("BlockCrush");
//	            AudioAssistant.Shot("BlockHit");
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

			GameObject o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(crushEffect);
	        o.transform.position = transform.position;
//			Debug.Log("Block Pos:" + o.transform.position);

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