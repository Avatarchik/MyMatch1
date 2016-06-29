using UnityEngine;
using System.Collections;

public class WaveLine : MonoBehaviour {

     Transform[] spritesTransArray;

    public float ChangeTime;

    public float RepeatTime;

    public GameObject[] Cards;

    void Awake()
    {
        spritesTransArray = this.GetComponentsInChildren<Transform>();
        
        
    }
	
	void Start () {
        InvokeRepeating("WaveRepeat", 0, RepeatTime);
	}

    void WaveRepeat()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(Wave());
        }
        
    }
	
    IEnumerator Wave()
    {

            for (int num = 0; num < spritesTransArray.Length + 4; num++)
            {
                for (int i = 1; i < spritesTransArray.Length; i++)
                {
                    if (i == num)
                    {
                        Vector3 temp = spritesTransArray[i].transform.localPosition;
                        spritesTransArray[i].transform.localPosition = new Vector3(temp.x, temp.y + 15, temp.z);
                    int cardnum=100;
                    switch (i)
                    {
                        case 2: cardnum = 0;
                            break;
                        case 5: cardnum = 1;
                            break;
                        case 8 :cardnum =2;
                            break;
                    }
                    if (cardnum!=100)
                    {
                        Vector3 tempcard = Cards[cardnum].transform.localPosition;
                        Cards[cardnum].transform.localPosition = new Vector3(tempcard.x, tempcard.y + 30, tempcard.z);
                        cardnum = 100;
                    }

                }

                    else if (i == num - 1)
                    {
                        Vector3 temp = spritesTransArray[i].transform.localPosition;
                        spritesTransArray[i].transform.localPosition = new Vector3(temp.x, temp.y + 15, temp.z);
                    }

                    else if (i == num - 3)
                    {
                        Vector3 temp = spritesTransArray[i].transform.localPosition;
                        spritesTransArray[i].transform.localPosition = new Vector3(temp.x, temp.y - 15, temp.z);

                    int cardnum = 100;
                    switch (i)
                    {
                        case 3:
                            cardnum = 0;
                            break;
                        case 6:
                            cardnum = 1;
                            break;
                        case 9:
                            cardnum = 2;
                            break;
                    }
                    if (cardnum != 100)
                    {
                        Vector3 tempcard = Cards[cardnum].transform.localPosition;
                        Cards[cardnum].transform.localPosition = new Vector3(tempcard.x, tempcard.y - 30, tempcard.z);
                        cardnum = 100;
                    }
                }

                    else if (i == num - 4)
                    {
                        Vector3 temp = spritesTransArray[i].transform.localPosition;
                        spritesTransArray[i].transform.localPosition = new Vector3(temp.x, temp.y - 15, temp.z);
                    }

                }

                yield return new WaitForSeconds(ChangeTime);

            }
    }
}
