using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class forcePointFilter : MonoBehaviour
{

    Text t;
    private void Awake()
    {
        t = GetComponent<Text>();
        if (t)
            t.mainTexture.filterMode = FilterMode.Point;

    }
}
