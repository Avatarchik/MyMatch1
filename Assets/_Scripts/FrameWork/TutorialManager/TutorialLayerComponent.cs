using UnityEngine;
using System.Collections;

public class TutorialLayerComponent : MonoBehaviour
{
    private int _orgLayer = 0;
    private bool _hasPanel = false;

    // Use this for initialization
    void Start()
    {
        var layer = 10;
        _orgLayer = gameObject.layer;
        UIPanel panel = gameObject.GetComponent<UIPanel>();
        if (panel != null)
        {
            _hasPanel = true;
            
        }

        if (!_hasPanel)
        {
            panel = gameObject.AddComponent<UIPanel>();
            panel.depth = 1;
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }

        NGUITools.SetLayer(gameObject, layer);
    }

    public void Reset()
    {
        if (!_hasPanel)
        {
            Destroy(gameObject.GetComponent<UIPanel>());
        }

        NGUITools.SetLayer(gameObject, _orgLayer);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
