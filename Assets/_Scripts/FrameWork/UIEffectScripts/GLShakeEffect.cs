using UnityEngine;
using System.Collections;

public class GLShakeEffect : MonoBehaviour 
{
    float totaltimeTick = 0;
    float _totaltime;
    float _interval;
    float xshake;

    float[] ShakePosX = new float[3];
    float[] ShakePosY = new float[3];
    int index = 0;

    public void BeginShake(float xShake, float yShake,float interval)
    {
        ShakePosX[0] = -xShake;
        ShakePosX[1] = 0;
        ShakePosX[2] = xShake;

        ShakePosY[0] = -yShake;
        ShakePosY[1] = 0;
        ShakePosY[2] = yShake;

        _interval = interval;

        StartCoroutine("Shake");
    }

    IEnumerator Shake()
    {
        while (true)
        {
            int x = Random.Range(0, 3);
            int y = Random.Range(0, 3);
            gameObject.transform.localPosition = new Vector3(ShakePosX[x], ShakePosY[y], gameObject.transform.localPosition.z);   
            yield return new WaitForSeconds(_interval);
        }
    }

    public void BeginShake(float xShake, float yShake, float interval, float totaltime)
    {
        ShakePosX[0] = -xShake;
        ShakePosX[1] = 0;
        ShakePosX[2] = xShake;

        ShakePosY[0] = -yShake;
        ShakePosY[1] = 0;
        ShakePosY[2] = yShake;

        _interval = interval;

        totaltimeTick = 0;
        _totaltime = totaltime;

        StartCoroutine("ShakeTimed");
    }

    IEnumerator ShakeTimed()
    {
        while (true)
        {
            if (totaltimeTick > _totaltime)
            {
                EndShake();
                yield return 0;
            }

            int x = Random.Range(0, 3);
            int y = Random.Range(0, 3);
            gameObject.transform.localPosition = new Vector3(ShakePosX[x], ShakePosY[y], gameObject.transform.localPosition.z);
            totaltimeTick += _interval;

            yield return new WaitForSeconds(_interval);
        }
    }

    public void EndShake()
    {
        StopAllCoroutines();
        gameObject.transform.localPosition = new Vector3(0, 0, gameObject.transform.localPosition.z);
    }
}
