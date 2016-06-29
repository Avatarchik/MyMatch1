using UnityEngine;
using System.Collections;

public class MatchCountResume : MonoBehaviour {

    private TweenScale[] mTweenScale;

    private TweenPosition []mTweenPosition;

    private TweenAlpha mTweenAlpha;

    private GameObject[] mGameObject;

    // Use this for initialization
    void Start () {

        mTweenScale = gameObject.GetComponentsInChildren<TweenScale>();

        mTweenPosition = gameObject.GetComponentsInChildren<TweenPosition>();//°üº¬×Ô¼º

        mTweenAlpha = gameObject.GetComponent<TweenAlpha>();

    }
	
    public void afterEffect()
    {
        gameObject.SetActive(false);
       
        foreach (var item in mTweenScale)
        {
            item.ResetToBeginning();

            item.enabled = true;
        }

        foreach (var item in mTweenPosition)
        {
            item.ResetToBeginning();

            item.enabled = true;
        }

        mTweenAlpha.ResetToBeginning();

        mTweenAlpha.enabled = true;

        mTweenScale[6].gameObject.transform.localScale=Vector3.zero;
    }


}
