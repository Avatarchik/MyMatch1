using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FightNew
{
	public abstract class FlyingBezier : MonoBehaviour 
	{
		public abstract string GetAnimScaleName();
		public abstract string GetAnimFlyName();

		protected Animation _anim;

		protected Bezier _bezier;

		public float Speed{get;set;}

		public virtual void OnUpdate(){}

		void Update()
		{
			OnUpdate();
		}

	}
}