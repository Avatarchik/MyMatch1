using UnityEngine;
using System.Collections;
using MyFrameWork;

namespace Fight
{
	[RequireComponent (typeof (Card))]
	public class StoneChip : MonoBehaviour 
	{
		
		public Card chip;
		bool mMatching = false;
		public bool matching 
		{
			set 
			{
				if (value == mMatching) return;
				mMatching = value;
				if (mMatching)
					SessionControl.Instance.matching ++;
				else
					SessionControl.Instance.matching --;
			}
			
			get 
			{
				return mMatching;
			}
		}
		void OnDestroy () 
		{
			matching = false;
		}
		
		void  Awake ()
		{
			chip = GetComponent<Card>();
			chip.chipType = "SimpleChip";
		}
		
		// Coroutine destruction / activation
		IEnumerator  DestroyChipFunction (){
			
			matching = true;
	        //AudioAssistant.Shot("StoneCrush");
			GetComponent<Animation>().Play("Minimizing");
			
			yield return new WaitForSeconds(0.1f);
			matching = false;
			
//	        chip.SetScore(1);

			chip.ParentRemove();

			GameObject o = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload("StoneCrush");
			o.transform.position = transform.position;


			while (GetComponent<Animation>().isPlaying) yield return 0;
			Destroy(gameObject);
		}
	}
}