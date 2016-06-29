using UnityEngine;
using System.Collections;
using MyFrameWork;

public class SplashLvup : MonoBehaviour {

    public GameObject Splash;

    UILabel mUILabel;

    public int CardSeat;

    public string Hp_or_Atk;

    private TweenScale mTweenScale;

    private TweenAlpha mTweenAlpha;

    void Start () {

        mUILabel = gameObject.GetComponent<UILabel>();

        mTweenScale = gameObject.AddComponent<TweenScale>();

        mTweenScale.enabled = false;

        mTweenScale.from = Vector3.one * 1.5f ;

        mTweenScale.to = Vector3.one;

        mTweenScale.delay = 0.2f;

        mTweenScale.duration = 0.3f;

        mTweenAlpha = gameObject.AddComponent<TweenAlpha>();

        mTweenAlpha.enabled = false;

        mTweenAlpha.from = 0;

        mTweenAlpha.to = 1;

        mTweenAlpha.delay = 0.2f;

        mTweenAlpha.duration = 0.052f;

    }
	

    //绿色字变大消失后执行的方法
    public void ShowSplash()
    {

        object[]obj= Util.CallMethod("TablePlayerAttrCtrl", "GetCardSeatAddition", CardSeat, Hp_or_Atk);

        mUILabel.alpha = 0;

        mUILabel.text = int.Parse(obj[0].ToString())+"";

        Splash.SetActive(true);

        transform.localPosition = new Vector3(transform.localPosition.x + 20, transform.localPosition.y, 0);

        mTweenScale.enabled = true;

        mTweenAlpha.enabled = true;
    }
}
