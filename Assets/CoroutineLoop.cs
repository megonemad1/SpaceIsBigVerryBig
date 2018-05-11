
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class CoroutineLoop : MonoBehaviour
{
    [HideInInspector]
    public bool killDecisionLoop;
    IEnumerator loop;
    public Action onTick = () => { };
    public Action onStart = () => { };
    [SerializeField]
    UnityEvent _onTick;
    [SerializeField]
    UnityEvent _onStart;
    [SerializeField]
    float tickIntervel;
    private bool start;

    private void Start()
    {
        loop = Loop();
    }
    public void StartLoop()
    {
        start = true;
        OnEnable();
    }
    private void OnDisable()
    {
        StopCoroutine(loop);
    }

    private void OnEnable()
    {
        if (start)
        {
            if (loop == null)
                loop = Loop();
            StartCoroutine(loop);
        }
    }

    IEnumerator Loop()
    {
        onStart.Invoke();
        while (!killDecisionLoop)
        {
            onTick.Invoke();
            yield return new WaitForSeconds(tickIntervel);
        }
    }


}