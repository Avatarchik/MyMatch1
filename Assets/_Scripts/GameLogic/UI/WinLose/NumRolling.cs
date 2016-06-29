using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 控制数字滚动动画
/// 第1.2帧播放序列帧的第1.2张图片
/// 然后在第3-6张图片中滚动播放，维持0.8秒
/// 然后重新播放第1.2张图片,同时渐隐启动，维持0.2秒
/// 
/// </summary>
public class NumRolling : MonoBehaviour {

    private UISprite mUISprite;

    private float timer; //计时器

    private List<UISpriteData> only1n2 = new List<UISpriteData>();

    private List<UISpriteData> only3t6 = new List<UISpriteData>();

    public int FramesPerSecond_1_2 = 10;

    public int FramesPerSecond_3_6 = 10;

    public float AnimDuration = 0.8f;

    public GameObject Label;

    private TweenAlpha mTweenAlpha;

    void Start()
    {
        //获取脚本挂在的动画的UISprite组件
        mUISprite = gameObject.GetComponent<UISprite>();

        //1.2两张图形成的UISpriteData类链表
        only1n2.Add(mUISprite.atlas.GetSprite("szsx-1"));

        only1n2.Add(mUISprite.atlas.GetSprite("szsx-2"));

        //3.4.5.6四张图组成的UISpriteData类链表
        for (int i = 3; i < 7; i++)
        {
            only3t6.Add(mUISprite.atlas.GetSprite("szsx-" + i));
        }

        //渐隐组件
        mTweenAlpha = gameObject.GetComponent<TweenAlpha>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //mUISprite.spriteName = mUISprite.atlas.spriteList[0].name; //获得图集的0号元素，通过名字获得

        if (timer * FramesPerSecond_1_2 < 2)
        {
            mUISprite.spriteName = only1n2[(int)(timer * FramesPerSecond_1_2 % only1n2.Count)].name; //在1.2号元素中循环
        }

        else if (timer <= AnimDuration)
        {
            mUISprite.spriteName = only3t6[(int)(timer * FramesPerSecond_3_6 % only3t6.Count)].name;//在3-6号元素中循环
        }
        else
        {
            mTweenAlpha.enabled = true;

            mUISprite.spriteName = only1n2[(int)(timer * FramesPerSecond_1_2 % only1n2.Count)].name; //在1.2号元素中循环
        }


    }

    /// <summary>
    /// 渐变消失后，执行这个方法，显示这个位置上的数字，让这个动画冻结/休眠
    /// </summary>
    public void ShowLabel_DestroySelf()
    {
        Label.SetActive(true);

        gameObject.SetActive(false);
    }
}
