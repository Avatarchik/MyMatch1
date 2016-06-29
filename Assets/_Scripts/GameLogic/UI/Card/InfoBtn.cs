using UnityEngine;
using System.Collections;

public class InfoBtn : MonoBehaviour {

	void OnClick()
    {
        DebugUtil.Info(gameObject.transform.parent.parent.name);
    }
}
