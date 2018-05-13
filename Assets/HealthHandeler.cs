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
    [SerializeField]
    PlayerScriptableObject playerdata;

    private void Awake()
    {
        if (playerdata)
            maxHeath *= playerdata.HealthModifyer*2 + 0.5f;
        health = maxHeath;
    }

    public override IEnumerator hit(Collider2D collision, BulletHandeler bullet)
    {
        DealDamage(bullet.damageType, bullet.sender);
        yield break;
    }

    public void DealDamage(ScriptableDamageType damageType, GameObject sender)
    {
        PlayerControler player = sender.GetComponent<PlayerControler>();
        if (player)
            health -= (player.playerdata.AtackModifyer*2 + 0.5f) * damageType.Magnitude;
        else
            health -= damageType.Magnitude;


        onDamageTaken.Invoke(new HealthArgs() { damage = damageType, handeler = this, cause = sender });
        if (health <= 0)
        {
            health = 0;
            onDeath.Invoke(new HealthArgs() { damage = damageType, handeler = this, cause = sender });
        }
    }
}
