using UnityEngine;
using System.Collections;

public class FlyingItems : MonoBehaviour {

    public GameObject _flyingTo;

    private float timer;

    private float _lerpTimer;

    Rigidbody rgb;

    public int Range_X = 50;

    public int FirstUpForce = 200;

    public float SecondUpTime = 0.5f;

    public float SecondUpForce = 20f;

    public float DrawingTime = 0.7f;

    public float FlyingSpeed = 0.4f;

    public float DisappearAfterDraw = 0.6f;
    void Start () {

        rgb = gameObject.GetComponent<Rigidbody>();

        rgb.AddForce(new Vector3(Random.Range(-Range_X, Range_X+1), FirstUpForce, 0));

    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer> DrawingTime)
        {
            _lerpTimer += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, _flyingTo.transform.position, _lerpTimer * FlyingSpeed);

            if (_lerpTimer >= DisappearAfterDraw)
            {
                Destroy(gameObject);
            }
        }

        else if (timer > SecondUpTime)
        {
            rgb.AddForce(Vector3.up * SecondUpForce);
        }

    }
}
