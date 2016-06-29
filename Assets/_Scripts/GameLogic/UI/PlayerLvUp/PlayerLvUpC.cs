using UnityEngine;
using System.Collections;

public class PlayerLvUpC : MonoBehaviour {

    private GameObject _firework;

    private Transform _fireworkfather;


    protected  void Awake()
    {
        _firework = transform.FindChild("S_Frame/Eft/S_Eft18").gameObject;

        _fireworkfather = transform.FindChild("S_Frame/Eft");
    }



    public void PlayFirework()
    {
        Invoke("Firework", Random.Range(0.1f, 1.1f));

        Invoke("Firework", Random.Range(0.1f, 1.1f));

        Invoke("Firework", Random.Range(0.1f, 1.1f));

        Invoke("Firework", Random.Range(0.1f, 1.1f));

        Invoke("Firework", Random.Range(0.1f, 1.1f));

        Invoke("Firework", Random.Range(0.1f, 1.1f));
        Invoke("Firework", Random.Range(0f, 1.1f));

        Invoke("Firework", Random.Range(1.1f, 8.1f));
        Invoke("Firework", Random.Range(1.1f, 8.1f));

        Invoke("Firework", Random.Range(1.1f, 8.1f));
        Invoke("Firework", Random.Range(1.1f, 8.1f));

        Invoke("Firework", Random.Range(1.1f, 8.1f));
        Invoke("Firework", Random.Range(1.1f, 8.1f));

        Invoke("Firework", Random.Range(1.1f, 8.1f));
        Invoke("Firework", Random.Range(1.1f, 8.1f));

        Invoke("Firework", Random.Range(1.1f, 8.1f));
    }

    void Firework()
    {
        GameObject go = Instantiate(_firework);

        go.transform.parent = _fireworkfather;

        go.transform.localPosition = new Vector3(Random.Range(-300, 300), Random.Range(450, 550), 0);

        go.SetActive(true);

        Destroy(go, 1);

    }
}
