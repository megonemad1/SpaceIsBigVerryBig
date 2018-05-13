using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Extensions;

public class StatSetter : MonoBehaviour
{
    [SerializeField]
    statusBar bar;
    [SerializeField]
    PlayerScriptableObject playerdata;
    [SerializeField]
    Vector3 allotment;
    [SerializeField]
    UnityEvent updated;
    private void Awake()
    {
        if (bar == null)
        {
            bar = GetComponent<statusBar>();
            bar.status = 1;
        }
    }
    
    public void setValue(string seed)
    {
        RandomInitable r = new RandomInitable(seed.GetHashCode());
        float status = r.value;
        playerdata.setStat(allotment * status);
        updated.Invoke();
    }

    public void Update()
    {
        bar.status = Mathf.Lerp(bar.status, playerdata.stats.ScaleChain(allotment).magnitude,0.1f);
    }
}
