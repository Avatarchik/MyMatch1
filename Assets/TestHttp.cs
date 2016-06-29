using UnityEngine;
using System.Collections;
using MyFrameWork;

public class TestHttp : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		MyHttp.Instance.RequestVerInfo(null);
	}
	

}
