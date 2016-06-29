using UnityEngine;
using System.Collections;

public class MyParticleAddForce : MonoBehaviour 
{
	public GameObject Particle;

	public GameObject GoFrom;
	public GameObject GoTo;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			GameObject projectile = Instantiate(Particle, GoFrom.transform.position, Quaternion.identity) as GameObject;
			var vecDiff = GoTo.transform.position - GoFrom.transform.position;
			var roatation = Quaternion.FromToRotation(GoFrom.transform.up, vecDiff);
			projectile.transform.rotation = roatation;

			var pos = projectile.transform.position;
			pos.z = -2f;
			projectile.transform.position = pos;

			//projectile.transform.LookAt(GoTo.transform.position);
			projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.up * 700);
			projectile.GetComponent<FightNew.MyProjectileScript>().impactNormal = GoTo.transform.position.normalized;
		}
	}
}
