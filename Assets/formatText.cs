using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class formatText : MonoBehaviour {
    [SerializeField]
    PlayerScriptableObject value;
    Text t;
    private void Awake()
    {
        t = GetComponent<Text>();
        t.text = string.Format(t.text, value.score.ToString());
    }
}
