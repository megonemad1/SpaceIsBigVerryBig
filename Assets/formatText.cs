using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class formatText : MonoBehaviour {
    [SerializeField]
    PlayerScriptableObject value;
    Text t;
    string tvalue;
    private void Awake()
    {
        t = GetComponent<Text>();
        tvalue = t.text;
    }

    public void Update()
    {
        t.text = string.Format(tvalue, value.score.ToString());
    }
}
