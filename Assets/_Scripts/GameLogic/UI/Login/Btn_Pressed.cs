using UnityEngine;
using System.Collections;

public class Btn_Pressed : MonoBehaviour {
    private UISprite mUISprite;
    private int num;

    void Start () {
        mUISprite = gameObject.GetComponent<UISprite>();
	}
	
	// Update is called once per frame
	void Update () {
   
	}

    void OnPress()
    {
        num++;
        if (num%2==1)
        {
            mUISprite.spriteName = "PLAY2";

            mUISprite.transform.localScale = Vector3.one * 1.05f;
        }
        else
        {
            mUISprite.spriteName = "PLAY1";

            mUISprite.transform.localScale = Vector3.one ;
        }
        
    }
}
