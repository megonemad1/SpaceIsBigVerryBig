using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour {
    [SerializeField]
    statusBar HealthBar;
	// Use this for initialization
	void Awake () {
        if (HealthBar == null)
            HealthBar = GetComponent<statusBar>();
	}
	float calculateNonLiniarHealth(float healthPercentage)
    {
        return healthPercentage * healthPercentage;
    }
	// Update is called once per frame
	public void UpdateHealth (HealthArgs health) {
        HealthBar.status = calculateNonLiniarHealth(health.handeler.health/health.handeler.maxHeath);
	}
}
