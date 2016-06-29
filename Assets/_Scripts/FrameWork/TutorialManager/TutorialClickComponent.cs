using UnityEngine;
using System.Collections;

public class TutorialClickComponent : MonoBehaviour
{
    public event System.Action<GameObject> Click;

    public event System.Action<GameObject> Press;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnPress()
    {
        if (Press != null) Press(gameObject);
    }

    void OnClick()
    {
        if (Click != null) Click(gameObject);
    }
}
