using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 0.1秒到5秒，随机喷一次烟
/// </summary>


public class Chimney_MakingSmoke : MonoBehaviour {

    public GameObject Smoke_Prefab;

    public GameObject Chimney_Pos;

    public float Min_Emit=0.1f;

    public float Max_Emit=3f;

    private Tw_Act_Rep _twscript;

    private float _timer;

    public Dictionary<int, GameObject> _dic_smoke_pool;

    private int i;

    public bool _switch;

    private Queue<GameObject> _queueSmoke;
    public void Awake()
    {
        _twscript = GetComponent<Tw_Act_Rep>();

        _timer = Random.Range(Min_Emit, Max_Emit);

        _dic_smoke_pool = new Dictionary<int, GameObject>();

        _queueSmoke = new Queue<GameObject>();


    }

    public void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer<=0)
        {

            GameObject go = GetOneSmoke();

            //Debug.Log("--------------1");

            go.SetActive(true);

            //Debug.Log("--------------2");

            gameObject.GetComponent<Tw_Act_Rep>().TweenRepeat_Scale_Position_Alpha();

            _timer = Random.Range(Min_Emit, Max_Emit);
        }

        
    }

    private GameObject GetOneSmoke()
    {
        GameObject smoke = null;

        if (_queueSmoke.Count > 0)
        {
            smoke = _queueSmoke.Dequeue();

            //Debug.Log("Get from queue, left:"+_queueSmoke.Count);

            //smoke = _queueSmoke.Peek();//从队列中取出，但不从中删除

           // _queueSmoke.Enqueue(gameObject);// 加入队列

        }
        else
        {
            smoke = Instantiate(Smoke_Prefab) as GameObject;

            smoke.GetComponent<Smoke_Indi>().OnFinish = ReturnBackQueue;

            smoke.transform.parent = Chimney_Pos.transform;

            smoke.name = "Smoke_Prefab-" + i;

            i++;

            //Debug.Log("Created new one: "+ smoke.name);
        }
        return smoke;

    }

    private void ReturnBackQueue(GameObject go)
    {
        _queueSmoke.Enqueue(go);
    }

    private GameObject CheckPool()
    {
        if (_dic_smoke_pool.Count>0)
        {
            foreach (var item in _dic_smoke_pool.Keys)
            {
                GameObject t = _dic_smoke_pool[item];

                _dic_smoke_pool.Remove(item);

               // Debug.Log("Taken from pool, key no. is : " + item+ ". pool left: " + _dic_smoke_pool.Count);

                return t;
            }
           
        }
        else
        {

            GameObject go = Instantiate(Smoke_Prefab);

            go.GetComponent<Smoke_Indi>().MakingSmokeScript = this;

            go.transform.parent = Chimney_Pos.transform;

            //go.transform.localPosition=Vector3.zero;

            go.transform.localScale = Vector3.one;

            go.GetComponent<Smoke_Indi>().MyNum = i;

            go.name =  "Smoke_Prefab-"+i;

            i++;

           // Debug.Log("Created new, naming: " +go.name);

            return go;
        }

        return null;
    }

    void OnDestroy()
    {
        if (_dic_smoke_pool!=null)
        {
            _dic_smoke_pool.Clear();
        }
    }
}
