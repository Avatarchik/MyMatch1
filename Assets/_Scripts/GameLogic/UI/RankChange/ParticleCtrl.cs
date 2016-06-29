using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]-���Ҫ���Updateʹ�ã�����ֻ����һ֡
public class ParticleCtrl : MonoBehaviour {

    public int renderQueue = 3001;

    private Renderer particlerenderer;
    void Start()
    {
        particlerenderer = gameObject.GetComponent<Renderer>();

        if (particlerenderer != null && particlerenderer.sharedMaterial != null)
        {
            particlerenderer.sharedMaterial.renderQueue = renderQueue;
        }
    }

}


//����Ϊȫ������ ��ַ http://www.narkii.com/club/thread-323697-1.html
//using UnityEngine;
//using System.Collections;

//[ExecuteInEditMode]
//public class ControlParticle : MonoBehaviour
//{

//    public int renderQueue = 30000;
//    public bool runOnlyOnce = false;

//    void Start()
//    {
//        Update();
//    }

//    void Update()
//    {
//        if (renderer != null && renderer.sharedMaterial != null)
//        {
//            renderer.sharedMaterial.renderQueue = renderQueue;
//        }
//        if (runOnlyOnce && Application.isPlaying)
//        {
//            this.enabled = false;
//        }
//    }
//}

