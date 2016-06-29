using UnityEngine;
using System.Collections;

namespace FightNew
{
	public abstract class IHelpCard : MonoBehaviour 
	{
		public Card chip;
		bool mMatching = false;
		protected Animation anim;
		//SpriteRenderer sprite;

		public bool matching 
		{
			set 
			{
				if (value == mMatching) return;

				mMatching = value;

				if (mMatching)
					FightMgr.Instance.matching ++;
				else
					FightMgr.Instance.matching --;
			}
			get 
			{
				return mMatching;
			}
		}

		void OnDisable () 
		{
			matching = false;

			//if (chip.id >= 0 && chip.id < 6)
			//	SessionControl.main.countOfEachTargetCount[chip.id]--;
		}

		void Awake() 
		{
			anim = GetComponent<Animation>();
			chip = GetComponent<Card>();

			OnInit();

			//sprite = GetComponent<SpriteRenderer>();
		}

		public virtual void OnInit(){}
		public virtual void OnHit(){}
		public abstract IEnumerator DestroyChipFunction();
		public virtual void ClearSide(){}
		public virtual void AddSide(Side side){}
	}
}
