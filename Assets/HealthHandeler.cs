using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthHandeler : HitLisoner
{

    [SerializeField]
    public float health = 100;
    [SerializeField]
    public float maxHeath = 100;
    [SerializeField]
    UnityHealthEvent onDamageTaken;
    [SerializeField]
    UnityHealthEvent onDeath;

    public override IEnumerator hit(Collider2D collision, BulletHandeler bullet)
    {
        DealDamage(bullet.damageType, bullet.sender);
        yield break;
    }

    public void DealDamage(ScriptableDamageType damageType, GameObject sender)
    {
        health -= damageType.Magnitude;
        onDamageTaken.Invoke(new HealthArgs() { damage = damageType, handeler = this,  cause= sender });
        if (health <= 0)
        {
            health = 0;
            onDeath.Invoke(new HealthArgs() { damage = damageType, handeler = this, cause= sender });
        }
    }
}
