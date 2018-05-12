using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showAtZero : MonoBehaviour {

    [SerializeField]
    Slider s;
    [SerializeField]
    GameObject g;
	// Update is called once per frame
	void Update () {
            g.SetActive(s.value == 0);
	}
}
