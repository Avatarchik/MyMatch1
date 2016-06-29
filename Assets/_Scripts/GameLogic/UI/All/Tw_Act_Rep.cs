using UnityEngine;
using System.Collections;

public class Tw_Act_Rep : MonoBehaviour {

    public GameObject []ToActivateAry;

    public GameObject[] ToDeactivateAry;

    private TweenScale mTweenScale;

    private TweenPosition mTweenPosition;

    private TweenAlpha mTweenAlpha;




    public void Awake()
    {
        mTweenScale = gameObject.GetComponent<TweenScale>();

        mTweenPosition = gameObject.GetComponent<TweenPosition>();

        mTweenAlpha = gameObject.GetComponent<TweenAlpha>();
        
    }



    public void Activate_N_Deactivate()
    {
        if (ToActivateAry.Length>0)
        {
            foreach (var item in ToActivateAry)
            {
                item.SetActive(true);
            }
        }

        if (ToDeactivateAry.Length>0)
        {
            foreach (var item in ToDeactivateAry)
            {
                item.SetActive(true);
            }
        }
    }

    

    public void TweenRepeat_Scale_Position_Alpha()
    {
        if (mTweenScale)
        {
            mTweenScale.enabled = false;
            mTweenScale.ResetToBeginning();
            mTweenScale.enabled = true;
            //Debug.Log("mTweenScale");
        }
        if (mTweenPosition)
        {
            mTweenPosition.enabled = false;
            mTweenPosition.ResetToBeginning();
            mTweenPosition.enabled = true;
            //Debug.Log("mTweenPosition");
        }
        if (mTweenAlpha)
        {
            mTweenAlpha.enabled = false;
            mTweenAlpha.ResetToBeginning();
            mTweenAlpha.enabled = true;
            //Debug.Log("mTweenAlpha");
        }
        //Debug.Log("done TweenRepeat_Scale_Position_Alpha");
    }

    void OnDisable() //ø…“‘÷¥––
    {
        //Debug.Log("OnDisable............................." + gameObject.name);
    }

    public void TweenBackScale()
    {
        if (mTweenScale)
        {
            mTweenScale.enabled = false;
            mTweenScale.ResetToBeginning();
            mTweenScale.enabled = true;
        }
    }

    public void TweenBackPosition()
    {
        if (mTweenPosition)
        {
            mTweenPosition.enabled = false;
            mTweenPosition.ResetToBeginning();
            mTweenPosition.enabled = true;
        }
    }

    public void TweenBackAlpha()
    {
        if (mTweenAlpha)
        {
            mTweenAlpha.enabled = false;
            mTweenAlpha.ResetToBeginning();
            mTweenAlpha.enabled = true;
        }
    }



}
