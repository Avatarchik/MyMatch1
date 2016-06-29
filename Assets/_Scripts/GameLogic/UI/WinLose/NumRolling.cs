using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �������ֹ�������
/// ��1.2֡��������֡�ĵ�1.2��ͼƬ
/// Ȼ���ڵ�3-6��ͼƬ�й������ţ�ά��0.8��
/// Ȼ�����²��ŵ�1.2��ͼƬ,ͬʱ����������ά��0.2��
/// 
/// </summary>
public class NumRolling : MonoBehaviour {

    private UISprite mUISprite;

    private float timer; //��ʱ��

    private List<UISpriteData> only1n2 = new List<UISpriteData>();

    private List<UISpriteData> only3t6 = new List<UISpriteData>();

    public int FramesPerSecond_1_2 = 10;

    public int FramesPerSecond_3_6 = 10;

    public float AnimDuration = 0.8f;

    public GameObject Label;

    private TweenAlpha mTweenAlpha;

    void Start()
    {
        //��ȡ�ű����ڵĶ�����UISprite���
        mUISprite = gameObject.GetComponent<UISprite>();

        //1.2����ͼ�γɵ�UISpriteData������
        only1n2.Add(mUISprite.atlas.GetSprite("szsx-1"));

        only1n2.Add(mUISprite.atlas.GetSprite("szsx-2"));

        //3.4.5.6����ͼ��ɵ�UISpriteData������
        for (int i = 3; i < 7; i++)
        {
            only3t6.Add(mUISprite.atlas.GetSprite("szsx-" + i));
        }

        //�������
        mTweenAlpha = gameObject.GetComponent<TweenAlpha>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //mUISprite.spriteName = mUISprite.atlas.spriteList[0].name; //���ͼ����0��Ԫ�أ�ͨ�����ֻ��

        if (timer * FramesPerSecond_1_2 < 2)
        {
            mUISprite.spriteName = only1n2[(int)(timer * FramesPerSecond_1_2 % only1n2.Count)].name; //��1.2��Ԫ����ѭ��
        }

        else if (timer <= AnimDuration)
        {
            mUISprite.spriteName = only3t6[(int)(timer * FramesPerSecond_3_6 % only3t6.Count)].name;//��3-6��Ԫ����ѭ��
        }
        else
        {
            mTweenAlpha.enabled = true;

            mUISprite.spriteName = only1n2[(int)(timer * FramesPerSecond_1_2 % only1n2.Count)].name; //��1.2��Ԫ����ѭ��
        }


    }

    /// <summary>
    /// ������ʧ��ִ�������������ʾ���λ���ϵ����֣��������������/����
    /// </summary>
    public void ShowLabel_DestroySelf()
    {
        Label.SetActive(true);

        gameObject.SetActive(false);
    }
}
