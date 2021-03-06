using UnityEngine;
using System.Collections;

public class UIAlpha_Expand : MonoBehaviour
{
    public float animationTime = 3.0f;
    public AnimationCurve a;
    public UISprite _Sprite;
    public event System.Action OnEndEvent;    

    /// <summary>
    /// 添加脚本时给曲线赋初始值.(该方法只有在编辑器下有用)
    /// </summary>
    void Reset()
    {
//        GL.DebugManager.NormalLog("Reset");
        Keyframe[] ks = new Keyframe[3];
        ks[0] = new Keyframe(0, 0);
        ks[1] = new Keyframe(0.7f, 1.2f);
        ks[2] = new Keyframe(1, 1);
        a = new AnimationCurve(ks);
    }

    void Awake ()
    {
        _Sprite = GetComponent<UISprite>();
    }

    void OnEnable()
    {
        
    }

    public void DoAnimation(float anitime)
    {
        animationTime = anitime;
        StartCoroutine("Expand");
    }

    IEnumerator Expand()
    {
        float time = 0;
        while (time < animationTime)
        {
            time += Time.deltaTime;
            float currentTime = time / animationTime;
            float alpha = a.Evaluate(currentTime);
            _Sprite.color = new Color(_Sprite.color.r, _Sprite.color.g, _Sprite.color.b,alpha);
            yield return 0;
        }

        if (OnEndEvent != null)
            OnEndEvent.Invoke();
    }
}
