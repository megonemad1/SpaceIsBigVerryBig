using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipCollision : MonoBehaviour
{
    [SerializeField]
    ScriptableDamageType damageType;
    [SerializeField]
    UnityEvent Colliding;

    void OnCollisionEnter2D(Collision2D collision)
    {

        var other = collision.gameObject.GetComponent<HealthHandeler>();

        if (other != null)
            other.DealDamage(damageType, this.gameObject);
        Colliding.Invoke();

    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        var other = collision.gameObject.GetComponent<HealthHandeler>();

        if (other != null)
            other.DealDamage(damageType, this.gameObject);
        Colliding.Invoke();

    }

}
