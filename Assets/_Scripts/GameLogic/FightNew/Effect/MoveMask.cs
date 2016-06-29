using UnityEngine;
using System.Collections;

namespace FightNew
{
	public class MoveMask : MonoBehaviour 
	{
		private float _leftTime;
		private float _leftOriTime;
		private UITexture _sprite;

		void Awake()
		{
			_sprite = GetComponent<UITexture>();
		}

		public void Show(float leftTime)
		{
			_leftTime = leftTime;
			_leftOriTime = leftTime;
		}

		private void Update()
		{
			if(_leftTime > 0f)
			{
				_sprite.fillAmount = _leftTime / _leftOriTime;

				_leftTime -= Time.deltaTime;
			}
			else
			{
				_sprite.fillAmount = 0;
			}
		}
	}
}