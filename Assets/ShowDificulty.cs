using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDificulty : MonoBehaviour {

    [SerializeField]
    Text t;
    private void Awake()
    {
        t = GetComponent<Text>();
    }
    void Update () {
        t.text = ((int)(DificultyManager.Instance.currentDificultyModifyer * 50)).ToString() + "%";
	}
}
