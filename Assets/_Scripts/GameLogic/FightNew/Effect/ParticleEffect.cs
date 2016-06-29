using UnityEngine;
using System.Collections;

namespace FightNew
{
	// The effect of the particles. Upon completion will be removed.
	public class ParticleEffect : MonoBehaviour 
	{
		
		ParticleSystem ps;

		public bool killAfterLifetime = true;
		
		void  Start ()
		{
			ps = GetComponent<ParticleSystem>();

			if (transform.parent == null)
			{
				transform.parent = FieldMgr.SlotRoot;
				transform.localScale = Vector3.one;
			}

			if (killAfterLifetime) StartCoroutine(Kill());
		}
		
		IEnumerator Kill ()
		{
			yield return new WaitForSeconds(ps.duration + ps.startLifetime);
			Destroy(gameObject);
		}
	}
}