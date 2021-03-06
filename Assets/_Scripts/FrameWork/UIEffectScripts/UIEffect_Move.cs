using UnityEngine;
using System.Collections;

public class UIEffect_Move : MonoBehaviour
{
    public float animationTime = 0.2f;
    public Vector2 startPos = Vector3.one;
    public Vector2 endPos = Vector3.one;
    public AnimationCurve x;
    public AnimationCurve y;

    private Transform mTransform;
    private float pos_z;

    /// <summary>
    /// 添加脚本时给曲线赋初始值
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

    void Awake () {
        mTransform = transform;
        pos_z = mTransform.localPosition.z;
    }

    void OnEnable()
    {
        //StopCoroutine("Expand");
        StartCoroutine("Expand");
    }

    void OnDisable()
    {
        // 物体被禁用时结束协同程序
        //StopAllCoroutines();
    }

    IEnumerator Expand()
    {
        mTransform.localPosition = startPos;
        float time = 0;

        while (time < animationTime)
        {
            time += Time.deltaTime;
            float currentTime = time / animationTime;
            Vector3 pos = Vector3.Lerp(startPos, endPos, currentTime);
            float pos_x = 1 - x.Evaluate(currentTime);
            float pos_y = 1 - y.Evaluate(currentTime);
            mTransform.localPosition = new Vector3(pos.x * pos_x, pos.y * pos_y, pos_z);
            //DebugManager.NormalLog(string.Format("Pos = {0}\n localPos = {1}\n [x={2}, y={3}]", pos, transform.localPosition, pos_x, pos_y));
            yield return 0;
        }
    }
}
