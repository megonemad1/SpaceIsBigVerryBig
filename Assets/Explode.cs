using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {
    [SerializeField]
    Animation Explosion;
	// Use this for initialization
	void Awake () {
        Explosion.Play();
        Destroy(this.gameObject, Explosion.clip.length);
    }
    
}
