using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthHandeler : HitLisoner {

    [SerializeField]
    public float health = 100;
    [SerializeField]
    public float maxHeath = 100;
    [SerializeField]
    UnityHealthEvent onDamageTaken;
    [SerializeField]
    UnityHealthEvent onDeath;

    public void DealDamage(ScriptableDamageType damage)
    {
        health -= damage.Magnitude;
        onDamageTaken.Invoke(new HealthArgs() { damage = damage, handeler = this });
        if (health <= 0)
        {
            health = 0;
            onDeath.Invoke(new HealthArgs() { damage = damage, handeler = this });
        }
    }

    public override IEnumerator hit(Collider2D collision, BulletHandeler bullet)
    {
        DealDamage(bullet.damageType);
        yield break;
    }
}
