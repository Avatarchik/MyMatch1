/*******************************************************
 * 
 * 文件名(File Name)：             Dragon
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/04/16 17:48:40
 *
 *******************************************************/

using UnityEngine;
using System.Collections;

namespace Fight
{
	public class Dragon : MonoBehaviour 
	{
		public enum E_DragonAnim
		{
			idle,
			panic,//摇晃
			get_hit_front,//受击
			loop_da_loop,//翻滚
			die,
			stirr_on_ground,
			laugh,
			attack_reapeatedly
		}

		private Animation _anim;
		void Awake()
		{
			_anim = GetComponent<Animation>();
		}

		[HideInInspector]
		public bool IsPlayHitAnim = false;
		public bool IsDead = false;

		void Update()
		{
			if(_anim != null && !_anim.isPlaying)
			{
				if(!IsDead)
				{
					if(_anim != null)
						//继续待机状态
						_anim.Play(E_DragonAnim.idle.ToString());
				}
			}

			//		if(IsPlayHitAnim && Time.timeSinceLevelLoad - lastHitTime > 0.7f) //if 1
			//		{
			//			IsPlayHitAnim = false;
			//			PlayAnim1();
			//			lastHitTime = Time.timeSinceLevelLoad;
			//			//PlayAnim(E_DragonAnim.get_hit_front);
			//		}

	//		if(!IsDead && IsPlayHitAnim) //if 1
	//		{
	//			IsPlayHitAnim = false;
	//			PlayAnim(E_DragonAnim.get_hit_front);
	//			//			lastHitTime = Time.timeSinceLevelLoad;
	//			//PlayAnim(E_DragonAnim.get_hit_front);
	//		}

			//		if(Input.GetKeyDown(KeyCode.A)) //if 2
			//		{
			//			PlayAnim1();
			//		}
		}

		public void PlayAnim(E_DragonAnim type)
		{
			string anim = type.ToString();
			float fadeInTime = 0.2f;

			if(_anim != null && _anim.IsPlaying(anim))
			{
				_anim.CrossFadeQueued(anim,fadeInTime,QueueMode.PlayNow);
			}
			else
			{
				_anim.CrossFade(anim,fadeInTime);
			}

		}

	//	public IEnumerator Appear()
	//	{
	//		float t = 0;
	//
	//		Vector3 targetPosition = _oriPosition;
	//		targetPosition.z = -1;
	//
	//		while (t < 1f) 
	//		{
	//			//			t += (-Mathf.Abs(0.5f - t) + 0.5f + 0.05f) * Time.unscaledDeltaTime * 6;
	//
	//			t += Time.unscaledDeltaTime * 0.1f;// * 0.1f;
	//			transform.position = Vector3.Lerp(transform.position, targetPosition, t);
	//			yield return 0;
	//			if(Mathf.Abs(transform.position.z - targetPosition.z) <= 0.03f)
	//			{
	//				transform.position = targetPosition;
	//				break;
	//			}
	//		}
	//
	//		_anim.Play(Dragon.E_DragonAnim.loop_da_loop.ToString());
	//	}
	}
}