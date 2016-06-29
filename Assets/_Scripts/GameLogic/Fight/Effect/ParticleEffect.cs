using UnityEngine;
using System.Collections;

namespace Fight
{
	// The effect of the particles. Upon completion will be removed.
	public class ParticleEffect : MonoBehaviour {
		
		ParticleSystem ps;
	//	public string sortingLayer;
	//	public int sortingOrder;
		public bool killAfterLifetime = true;
		
		void  Start ()
		{
			ps = GetComponent<ParticleSystem>();
	//		ps.GetComponent<Renderer>().sortingLayerName = sortingLayer;
	//		ps.GetComponent<Renderer>().sortingOrder = sortingOrder;
			if (killAfterLifetime) StartCoroutine("Kill");

	        if (transform.parent == null)
			{
				transform.parent = GameObject.Find("SlotAll").transform;
				transform.localScale = Vector3.one;
//				Debug.Log("Block Pos11:" + transform.position);
			}
		}
		
		IEnumerator Kill (){
			yield return new WaitForSeconds(ps.duration + ps.startLifetime);
			Destroy(gameObject);
		}
	}
}