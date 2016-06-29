using UnityEngine;
using System.Collections;

public class SplashChange : MonoBehaviour {

    UISprite mUISprite;

    UISpriteAnimation mUISpriteAnimation;
 
    void Start () {

        mUISprite =gameObject.GetComponent<UISprite>();

        mUISpriteAnimation = gameObject.GetComponent<UISpriteAnimation>();
    }
	
	void Update () {

        if (mUISprite.spriteName=="effect-16f")
        {
            gameObject.SetActive(false);

            mUISprite.spriteName = "effect-16a";

            mUISpriteAnimation.ResetToBeginning();
        }
    }
}
