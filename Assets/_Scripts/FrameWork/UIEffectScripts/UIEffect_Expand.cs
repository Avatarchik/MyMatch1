using UnityEngine;
using System.Collections;

public class UIEffect_Expand : MonoBehaviour
{
    public float animationTime = 0.2f;
    public AnimationCurve x;
    public AnimationCurve y;

	[SerializeField]
	private int xPara = 1;
	[SerializeField]
	private int yPara = 1;

    private Transform mTransform;

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
        x = new AnimationCurve(ks);
        y = x;
    }

    void Awake ()
    {
        mTransform = transform;
    }

    void OnEnable()
    {
        StartCoroutine("Expand");
    }

    IEnumerator Expand()
    {
        float time = 0;
        while (time < animationTime)
        {
            time += Time.deltaTime;
            float currentTime = time / animationTime;
            float pos_x = x.Evaluate(currentTime);
            float pos_y = y.Evaluate(currentTime);
			mTransform.localScale = new Vector3(pos_x * xPara, pos_y * yPara, 1);
            //DebugManager.NormalLog(mTransform.localScale);
            yield return 0;
        }
    }
}
