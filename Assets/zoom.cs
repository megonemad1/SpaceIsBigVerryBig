using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour {
    [SerializeField]
    Camera c;
    [SerializeField]
    float finalSize;
    [SerializeField]
    float dt;
    private void Awake()
    {
        c = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update () {
        c.orthographicSize = Mathf.Lerp(c.orthographicSize, finalSize, dt);
	}
}
