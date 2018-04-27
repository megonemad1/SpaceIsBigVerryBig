using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeppon : MonoBehaviour
{
     ICollector collector;

    // Use this for initialization
    void Start()
    {
        collector = GetComponent<ICollector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collector != null)
        {
            IWeppon w = collector.getCurrentWeppon();
            if (w != null)
                if (Input.GetAxisRaw("Jump") != 0)
                {
                        w.StartFire(HandleHit);
                    Debug.Log(w.GetAmmo());
                }
        }
    }

    void HandleHit(GameObject sender)
    {
        Debug.Log(sender.name);
    }
}
