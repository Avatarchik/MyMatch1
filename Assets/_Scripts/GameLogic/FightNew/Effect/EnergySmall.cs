using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FightNew
{
	public class EnergySmall : FlyingBezier 
	{
		#region implemented abstract members of FlyingBezier

		public override string GetAnimScaleName()
		{
			return "EnergySmallFallIn";
		}

		public override string GetAnimFlyName()
		{
			return "EnergySmallOld";
		}

		#endregion

		private System.Action<E_AddEnergyType> onFinishEnergySmall{get;set;}

		private E_AddEnergyType _addEnergyType;

		private bool _canMove = false;
		private bool _arrive = false;
		private float _time = 0f;

		public void PlayEnergySmall(float delay,Queue<FlyingBezier> pool,List<FlyingBezier> list,E_AddEnergyType addType,System.Action<E_AddEnergyType> OnFinish)
		{
			//			if(!FightMgr.Instance.isPlaying) return;

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

			onFinishEnergySmall = OnFinish;
			_addEnergyType = addType;

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
				float scale = Mathf.Lerp(this.transform.localScale.x,1f,_time);
				this.transform.localScale = new Vector3(scale,scale,0);
			}
		}

		private IEnumerator Move(float delay,Queue<FlyingBezier> pool,List<FlyingBezier> list)
		{
			yield return new WaitForSeconds(delay + 0.1f);
			this.gameObject.SetActive(true);
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

			_canMove = true;
			_arrive = false;
			_time = 0f;

			while(!_arrive)
				yield return 0;

			this.gameObject.SetActive(false);
			list.Remove(this);
			pool.Enqueue(this);

			if(list.Count <= 0 && onFinishEnergySmall != null)
			{
				onFinishEnergySmall(_addEnergyType);
			}
		}
	}
}