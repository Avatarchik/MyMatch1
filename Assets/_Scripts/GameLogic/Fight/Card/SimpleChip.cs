using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace Fight
{
	// The class is responsible for logic SimpleChip
	[RequireComponent (typeof (Card))]
	public class SimpleChip : MonoBehaviour 
	{

		public Card chip;
		bool mMatching = false;
	    Animation anim;
//	    SpriteRenderer sprite;

		public bool matching {
			set {
				if (value == mMatching) return;
				mMatching = value;
				if (mMatching)
					SessionControl.Instance.matching ++;
				else
					SessionControl.Instance.matching --;
			}
			
			get {
				return mMatching;
			}
		}
		void OnDisable () {
			matching = false;

//	        if (chip.id >= 0 && chip.id < 6)
//				SessionControl.main.countOfEachTargetCount[chip.id]--;
		}

	    void Awake() 
		{
	        anim = GetComponent<Animation>();
			chip = GetComponent<Card>();
			chip.chipType = "SimpleChip";
//	        sprite = GetComponent<SpriteRenderer>();
		}

		private bool _isPlayHitAnim = false;

		void Update()
		{
			if(_isPlayHitAnim && !anim.IsPlaying("Hit"))
			{
				_isPlayHitAnim = false;
				chip.IsCheckMovePos = true;
			}
		}

	    public void OnHit() 
		{
			anim.Play("Hit");
			_isPlayHitAnim = true;
			chip.IsCheckMovePos = false;
	    }

		// Coroutine destruction / activation
		IEnumerator  DestroyChipFunction (){

//			DebugUtil.Debug("delete chip:" + chip.parentSlot.slot.key);
			matching = true;
			//AudioAssistant.Shot("ChipCrush");

			MusicManager.Instance.PlaySoundEff("Music/match3");
	        OnHit();
			
			yield return new WaitForSeconds(0.1f);
			
			chip.ParentRemove();
			matching = false;

//	        if (chip.id >= 0 && chip.id < 6 && SessionAssistant.main.countOfEachTargetCount[chip.id] > 0) {
//	            GameObject go = GameObject.Find("ColorTargetItem" + Chip.chipTypes[chip.id]);
//
//	            if (go) {
//	                Transform target = go.transform;
//	                
//	                sprite.sortingLayerName = "UI";
//	                sprite.sortingOrder = 10;
//
//	                float time = 0;
//	                float speed = Random.Range(1f, 1.8f);
//	                Vector3 startPosition = transform.position;
//	                Vector3 targetPosition = target.position;
//
//	                while (time < 1) {
//	                    transform.position = Vector3.Lerp(startPosition, targetPosition, EasingFunctions.easeInOutQuad(time));
//	                    time += Time.unscaledDeltaTime * speed;
//	                    yield return 0;
//	                }
//
//	                transform.position = target.position;
//	            }
//	        }       
	        

	        anim.Play("Minimizing");
			ParticleControl.Instance.AddParticle(this.transform.position,chip.id);

//			GameObject goCard3d = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(string.Format(Card.Card3dPrefabFormat,chip.id));
//			Vector3 thePosition = transform.TransformPoint(Vector3.down * 35);
//			thePosition.z = 0.5f;
//			goCard3d.transform.position = thePosition;
//	
//			//中心点
//			DebugUtil.Debug("pos1:" + chip.transform.parent.transform.position);
//			Vector3 posParentOri = chip.transform.parent.localPosition;
//			posParentOri.y -= 35;
//
//			chip.transform.parent.localPosition = posParentOri;
//			DebugUtil.Debug("pos2:" + chip.transform.parent.transform.position);
//			Vector3 pos = chip.transform.parent.transform.position;
//			pos.z = 0.5f;
//			goCard3d.transform.position = pos;
//
//			//还原
//			posParentOri.y += 35;
//			chip.transform.parent.localPosition = posParentOri;

//			Animation animGoCard3d = goCard3d.GetComponent<Animation>();
//			animGoCard3d.CrossFade("bite",0.2f);


	        while (anim.isPlaying)
	            yield return 0;

//			while(animGoCard3d.isPlaying)
//				yield return 0;

//			Destroy(goCard3d);


//			NetworkManager.Instance.ClientSendFightDataMsg(33);

	        Destroy(gameObject);

		}
	}
}