using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FightNew
{
	public class BlockFlyToOpp : FlyingBezier 
	{
		#region implemented abstract members of FlyingBezier

		public override string GetAnimScaleName()
		{
			return "EnergyPosSmallFallIn";
		}

		public override string GetAnimFlyName()
		{
			return "";
		}

		#endregion

		private System.Action onFinishAll{get;set;}
		private System.Action<Slot> onFinishOne{get;set;}

		//private E_AddEnergyType _addEnergyType;

		private bool _canMove = false;
		private bool _arrive = false;
		private float _time = 0f;
		private bool _isFlyToOpp = false;
		private Slot _flyTarget = null;

		public void Play(float delay,Queue<FlyingBezier> pool,List<FlyingBezier> list,bool isFlyToOpp,System.Action OnAllFinish,Slot FlyTarget,System.Action<Slot> OnOneFinish)
		{
			//			if(!FightMgr.Instance.isPlaying) return;

			_isFlyToOpp = isFlyToOpp;

			if(_anim == null)
				_anim = GetComponent<Animation>();

			if(_bezier == null)
				_bezier = new Bezier();

			Vector3 target = Vector3.zero;
			float x = (this.transform.localPosition.x - target.x) * 0.33f;
			float y = (this.transform.localPosition.y - target.y) * 0.5f;

			_bezier.SetParas(this.transform.localPosition,new Vector3(x,y,0),target,target);
			_canMove = false;
			_arrive = false;

			_flyTarget = FlyTarget;
			onFinishAll = OnAllFinish;
			onFinishOne = OnOneFinish;

			Vector3 oriScale = _isFlyToOpp ? new Vector3(1,1,0) : new Vector3(0.2f,0.2f,0);
			this.transform.localScale = oriScale;

			FightMgr.Instance.StartCoroutine(Move(delay,pool,list));
		}

		public override void OnUpdate()
		{
			if(_canMove)
			{
				_time += Time.deltaTime * Speed;
				if(_time > 1f)
				{
					_time = 1f;

					_arrive = true;
					_canMove = false;
				}

				this.transform.localPosition = _bezier.GetPointAtTime(_time);
				float scale = Mathf.Lerp(this.transform.localScale.x,_isFlyToOpp ? 0f : 1f,_time * 0.06f);
				this.transform.localScale = new Vector3(scale,scale,0);
			}
		}

		private IEnumerator Move(float delay,Queue<FlyingBezier> pool,List<FlyingBezier> list)
		{
			yield return new WaitForSeconds(delay);
			this.gameObject.SetActive(true);

			if(_isFlyToOpp)
			{
				string scaleName = GetAnimScaleName();
				if(!string.IsNullOrEmpty(scaleName))
				{
					_anim.Play(scaleName);
					while (_anim.isPlaying)
						yield return 0;
				}

				string flyName = GetAnimFlyName();
				if(!string.IsNullOrEmpty(flyName))
					_anim.Play(flyName);
			}

			_canMove = true;
			_arrive = false;
			_time = 0f;

			while(!_arrive)
				yield return 0;

			this.gameObject.SetActive(false);
			list.Remove(this);
			pool.Enqueue(this);

			if(list.Count <= 0 && onFinishAll != null)
			{
				onFinishAll();
			}

			if(onFinishOne != null)
			{
				onFinishOne(_flyTarget);
			}
		}
	}
}