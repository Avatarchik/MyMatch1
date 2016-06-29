using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FightNew
{
	public class PrizeFlying : FlyingBezier 
	{
		#region implemented abstract members of FlyingBezier

		public override string GetAnimScaleName()
		{
			return "EnergyPosSmallFallIn";
		}

		public override string GetAnimFlyName()
		{
			return "EnergySmall";
		}

		#endregion

		private System.Action onFinishPrizeFlying{get;set;}

		private bool _canMove = false;
		private bool _arrive = false;
		private float _time = 0f;
		private float _moveAddSpeed = 0.22f;

		private bool _isLeft;

		private float addSpeed = 1.8f;
		private float _targetX = 0f;
		private float _targetY = 0f;
		private float _timeAdd = 0f;

		private float _timeAddX = 0f;

		public void PlayPrize(float delay,Queue<FlyingBezier> pool,List<FlyingBezier> list,System.Action OnFinish,float delayTotal)
		{
			//			if(!FightMgr.Instance.isPlaying) return;

			if(_anim == null)
				_anim = GetComponent<Animation>();

			if(_bezier == null)
				_bezier = new Bezier();


			_canMove = false;
			_arrive = false;

			onFinishPrizeFlying = OnFinish;

			FightMgr.Instance.StartCoroutine(MovePrize(delay,delayTotal,pool,list));
		}

		public override void OnUpdate()
		{
			if(_isPlayingScale)
			{
				if(_targetY == 0f)
				{
					_targetY = this.transform.localPosition.y + 120f;

					if(_isLeft)
						_targetX = this.transform.localPosition.x + Random.Range(0f,1300f) / 10;
					else
						_targetX = this.transform.localPosition.x - Random.Range(0f,1300f) / 10;
				}


				var vec = this.transform.localPosition;
				vec.y = Mathf.Lerp(vec.y,_targetY,_timeAdd);
				vec.x = Mathf.Lerp(vec.x,_targetX,_timeAddX);

				this.transform.localPosition = vec;

				if(Mathf.Abs(this.transform.localPosition.x - _targetX) <= 5f && Mathf.Abs(this.transform.localPosition.y - _targetY) <= 5f)
				{
					_isPlayingScale = false;
				}
				else
				{
					addSpeed -= 0.2f;
					addSpeed = Mathf.Max(addSpeed,1f);

					_timeAdd += (Time.deltaTime * addSpeed);
					_timeAddX += (Time.deltaTime * addSpeed);
				}
			}

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

				if(Mathf.Abs(this.transform.localPosition.x) <= 20f && Mathf.Abs(this.transform.localPosition.y) <= 20f)
				{
					_arrive = true;
					_canMove = false;
				}

				Speed += _moveAddSpeed * Time.deltaTime;
			}
		}

		private bool _isPlayingScale = false;
		private IEnumerator MovePrize(float delay,float delayTotal,Queue<FlyingBezier> pool,List<FlyingBezier> list)
		{
			yield return new WaitForSeconds(delay + 0.1f);

			this.gameObject.SetActive(true);

			string scaleName = GetAnimScaleName();
			if(!string.IsNullOrEmpty(scaleName))
			{
				_anim.Play(scaleName);

				_isLeft = Random.Range(0,100) > 50;
				_isPlayingScale = true;

				while (_anim.isPlaying || _isPlayingScale)
					yield return 0;

				//_isPlayingScale = false;
			}


			Vector3 target = Vector3.zero;

			//int random = Mathf.Abs(this.transform.localPosition.x - target.x) <= 3 ? 1 : 0;//Random.Range(0,100) > 50 ? -1 : 1;

			float x = (this.transform.localPosition.x - target.x) * 0.43f;
			float y = (this.transform.localPosition.y - target.y) * 0.5f;

			if(Mathf.Abs(this.transform.localPosition.x - target.x) <= 3)
			{
				x = 290f;
			}

			//Debug.Log("localPos:" + this.transform.localPosition + "   pos:" + x + "," + y);
			_bezier.SetParas(this.transform.localPosition,new Vector3(x,y,0),target,target);

			//等待一起
			//			yield return new WaitForSeconds(delayTotal);

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

			if(list.Count <= 0 && onFinishPrizeFlying != null)
			{
				onFinishPrizeFlying();
			}
		}
	}
}