using UnityEngine;
using System.Collections;

public class Smoke_Indi : MonoBehaviour {


    private bool _changeDir = false;

    private Vector3 _changedto;

    private Vector3 _changedfrom;

    private float _timer;

    public Chimney_MakingSmoke MakingSmokeScript;

    public int MyNum;

    public System.Action<GameObject> OnFinish;

    void OnEnable () {

        //Debug.Log("  OnEnable in indi");

        transform.localPosition = Vector3.zero;

        _changeDir = false;

        _timer = 0;

        GetComponent<Tw_Act_Rep>().TweenRepeat_Scale_Position_Alpha();

        Invoke("ChangeDir", 1);

        Invoke("DeActive_BacktoPool", 5);
    }
	
    void Start()
    {
        //Debug.Log("Start in indi");

        GetComponent<Tw_Act_Rep>().TweenRepeat_Scale_Position_Alpha();
    }
	
	void ChangeDir()
    {
        _changedfrom = transform.localPosition;

        _changedto = new Vector3(Random.Range(-200, 0), Random.Range(200, 300), 0);

        _changeDir = true;
    }

    void DeActive_BacktoPool()
    {
        //MakingSmokeScript._dic_smoke_pool.Add(MyNum, gameObject);

        //Debug.Log("SetActive(false), no." + MyNum+" , Added to pool, pool left : " + MakingSmokeScript._dic_smoke_pool.Count);

        if (OnFinish != null)
            OnFinish(this.gameObject);

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (_changeDir)
        {
            _timer += Time.deltaTime;

            transform.localPosition = Vector3.Lerp(_changedfrom, _changedto, _timer/4);
        }
    }

}
