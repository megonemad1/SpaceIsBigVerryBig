using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class delayAction : MonoBehaviour
{
    [SerializeField]
    UnityEvent action;
    [SerializeField]
    float after;
    private void Awake()
    {
        StartCoroutine(run());
    }
    private IEnumerator run()
    {
        yield return new WaitForSeconds(after);
        action.Invoke();
        yield break;
    }
}
