using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skip : MonoBehaviour {

    Animator a;
    [SerializeField]
    PlayerScriptableObject playerdata;
    private void Awake()
    {
        a = GetComponent<Animator>();
    }
    void Update () {
		if (Input.anyKeyDown)
        {
            a.SetBool("skip", playerdata.seenInto);
        }
	}
}
