using UnityEngine;
using System.Collections;
using MyFrameWork;
/// <summary>
/// ���������������Ҫ��
/// 1.��ɫ˲����֣��׿�0.3���ڳ��֣�����ɫ���֣�
/// 2.����+���Ӵ�С���£����ȼ����֣�
/// 3.��ɫ4�Ǵ�С�������������������߶ȴ�С����������
/// 4.��Ч���֡���Ч17�ӵ���Ũ����Ч10a��С����Ȼ����ת�����ǳ���Ч18
/// 5.��ɫ���ֳ��֣���С������������ÿ�������ַ����֣������ֿ��Ƶȼ����ޡ�
///     Ȼ����ɫ���ֱ䵭���
/// 6.��ɫ������ʧ������������������ɫֵ�ı�
/// 
/// 
/// �����������ʱ�����
/// ����                      ��ʼ��           ʱ��             �Ͻ�               ��Ϸ����           �Ͻ�
/// 
/// 1.�׿򵯿�                0                0.3                                  S_Frame
/// 
/// 2.����                    0.3              0.2               S_Frame            S_LvUp
/// 
/// 2.���ֲ��ȴ�              0.5              0.2
/// 
/// 2.����+����С         0.7              0.2                                  S_LvUp
///   
/// 3.����+������             0.9              0.2+0.1           S_LvUp             S_LvUp_4Stars / S_LvUp_Lyrics
///  
/// 4.��Ч                    1.1              ѭ��              S_LvUp_4Stars      S_Eft               
/// 
/// 5.��ɫ����C1              1.5              1.5               S_Eft
/// 
/// 5.��ɫ����C2              1.8              1.5               L_S1_HP_Add��Position��
/// 
/// 5.��ɫ����C3              2.1              1.5               L_S2_HP_Add
/// 
/// 6.C1ˢ+���ָ���           3                                  L_S1_HP_Add�Ϲ�L_S1_HP_Bef�Ľű���ShowSplash()����
/// 
/// 6.C2ˢ+���ָ���           3.3                                L_S1_HP_Add�Ϲ�L_S1_HP_Bef�Ľű���ShowSplash()����
/// 
/// 6.C3ˢ+���ָ���           3.6                                L_S1_HP_Add�Ϲ�L_S1_HP_Bef�Ľű���ShowSplash()����
/// 
/// 
/// </summary>
/// 



    

public class UIPlayerLvUp : BaseUI {

    private GameObject _firework;

    private Transform _fireworkfather;
    protected override void OnInit()
    {
        base.OnInit();
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        _firework = transform.FindChild("S_Frame/Eft/S_Eft18").gameObject;

        _fireworkfather = transform.FindChild("S_Frame/Eft");
    }

   
    //protected override void OnBtnClick(GameObject go)
    //{
    //    switch (go.name)
    //    {
    //        case "Sprite_ConfirmWin":
    //            break;
    //        default:
    //            break;
    //    }
    //}

    
    public void PlayFirework()
    {
        Invoke("Firework", Random.Range(1.1f, 2.1f));

        Invoke("Firework", Random.Range(3.1f, 5.1f));

        Invoke("Firework", Random.Range(7.1f, 8.1f));
    }

    void Firework()
    {
        GameObject go = Instantiate(_firework);

        go.transform.parent = _fireworkfather;

        go.transform.localPosition = new Vector3(Random.Range(-300, 300), Random.Range(450, 550), 0);

        go.SetActive(true);

        Destroy(go, 1);
        
    }
}
