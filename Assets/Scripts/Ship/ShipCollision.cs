using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollision : MonoBehaviour, IDamageHandler
{
    public float CurrentHelth;
    public float MaxHelth = 10;

    public IDamageHandler DealDamage(GameObject sender, float damage)
    {
        CurrentHelth = Mathf.Clamp(CurrentHelth - damage, 0, MaxHelth);
        if (this.CurrentHelth > 0)
            return this;
        else
            return null;
    }

    // Use this for initialization
    void Start()
    {
        CurrentHelth = MaxHelth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        DealDamage(collision.gameObject, collision.relativeVelocity.magnitude);
        IDamageHandler d = collision.gameObject.GetComponent<IDamageHandler>();
        if (d != null)
        {
            d.DealDamage(this.gameObject, collision.relativeVelocity.magnitude);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.CurrentHelth <= 0)
            Destroy(this.gameObject);
    }
}
