using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stayOnScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public float LearpSpeed = 0.2f;
    public float LowerBound = 0.1f;
    public float UpperBound = 0.9f;
    public Camera cam;
    // Update is called once per frame
    void Update () {
        if (cam != null)
        {
            Vector3 pos = cam.WorldToViewportPoint(transform.position);
            if (pos.x > UpperBound)
            {
                pos.x = Mathf.Lerp(pos.x, UpperBound, LearpSpeed);
            }
            else if (pos.x < LowerBound)
            {

                pos.x = Mathf.Lerp(pos.x, LowerBound, LearpSpeed);
            }
            if (pos.y > UpperBound)
            {
                pos.y = Mathf.Lerp(pos.y, UpperBound, LearpSpeed);
            }
            else if (pos.y < LowerBound)
            {

                pos.y = Mathf.Lerp(pos.y, LowerBound, LearpSpeed);
            }
            pos.x = Mathf.Clamp01(pos.x);
            pos.y = Mathf.Clamp01(pos.y);
            transform.position = cam.ViewportToWorldPoint(pos);
        }
    }
}
