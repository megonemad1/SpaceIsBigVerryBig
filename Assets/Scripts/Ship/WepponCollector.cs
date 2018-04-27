using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WepponCollector : MonoBehaviour, ICollector {
    public GameObject currentWeppon = null;
    public IWeppon getCurrentWeppon()
    {
        if (currentWeppon != null)
            return currentWeppon.GetComponent<IWeppon>();
        else
            return null;
    }

    public void PickUp(GameObject pickup)
    {
        pickup.transform.parent = this.transform;
        pickup.transform.localRotation = Quaternion.identity;
        pickup.transform.localPosition = Vector3.forward;
        Destroy(currentWeppon);
        currentWeppon = pickup;
    }

    // Use this for initialization
    void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
