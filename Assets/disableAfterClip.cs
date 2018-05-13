using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableAfterClip : MonoBehaviour {

    AudioSource a;
    private void Awake()
    {
        a = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        StartCoroutine(disableAfter(a.clip.length));
    }
    IEnumerator disableAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        this.gameObject.SetActive(false);
    }
}
