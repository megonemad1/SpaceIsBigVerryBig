using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skip : MonoBehaviour {

    Animator a;
    private void Awake()
    {
        a = GetComponent<Animator>();
    }
    void Update () {
		if (Input.anyKeyDown)
        {
            a.SetBool("skip", true);
        }
	}
}
