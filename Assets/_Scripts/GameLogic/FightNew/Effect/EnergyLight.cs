using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

namespace FightNew
{
	public class EnergyLight : MonoBehaviour 
	{
		private const float PerLightMax = 1f;

		private float _energy = 0f;

		private UISlider _slider;

		private UISprite _spriteLight;

		private int _lightCnt = 0;

		private bool isStart = false;
		private bool isLight = false;
		private float target = 1f;

		void Awake()
		{
			_slider = GetComponent<UISlider>();
			_spriteLight = transform.Find("SpriteChild").GetComponent<UISprite>();
		}

		void Update()
		{
			if(_lightCnt > 0 && FightMgr.Instance.isPlaying)
			{
				if(!isStart) 
				{
					isStart = true;
					//先变暗
					isLight = false;

					target = 0f;
				}

				_spriteLight.alpha = Mathf.Lerp(_spriteLight.alpha,target,Time.deltaTime * 12f);
				if(Mathf.Abs(_spriteLight.alpha - target) <= 0.1f)
				{
					if(isLight)
					{
						//完成一次了
						_lightCnt--;
						isStart = false;
					}
					else
					{
						//再变亮
						isLight = true;
						target = 1f;
					}
				}

//				_spriteLight.alpha = target;
//				if(isLight)
//				{
//					//完成一次了
//					_lightCnt--;
//					isStart = false;
//				}
//				else
//				{
//					//再变亮
//					isLight = true;
//					target = 1f;
//				}
			}
		}


		//闪烁一次
		public float Light(float addEnergy)
		{
//			if(_energy >= 1f)
//				return addEnergy;

			addEnergy = addEnergy - _energy;
			if(_energy >= PerLightMax)
			{
				_lightCnt++;
				return addEnergy;
			}
			else
			{
				float canAdd = Mathf.Min(PerLightMax - _energy,addEnergy);

				if(canAdd > 0f)
				{
					_energy += canAdd;

					
					_slider.value = (PerLightMax - _energy) / PerLightMax;
				}

				_lightCnt++;
				return addEnergy - canAdd;
			}
		}

		public float UnLight(float addEnergy)
		{
			_energy = Mathf.Min(PerLightMax,addEnergy);
			_slider.value = (PerLightMax - _energy) / PerLightMax;

			return addEnergy - _energy;
		}
	}
}