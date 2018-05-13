using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackHandeler : MonoBehaviour
{
    [SerializeField]
    public GameObject spawnPoint;

    [SerializeField]
    UnityEvent onFire;
    [SerializeField]
    UnityEvent onCharging;
    [SerializeField]
    UnityEvent onCharged;
    [SerializeField]
    UnityEvent onDischarged;



    [SerializeField]
    ScriptableAmmoType currentAmmo;
    [SerializeField]
    bool _charged;
    public bool charged { get { return _charged; } }
    [SerializeField]
    float chargeTime;
    [SerializeField]
    SpriteRenderer chargeSprite;
    private IEnumerator charger;
    public void ChargeUp()
    {
        charger = _ChargeUp();
        StartCoroutine(charger);
    }

    private IEnumerator _ChargeUp()
    {
        onCharging.Invoke();
        chargeSprite.gameObject.SetActive(true);
        yield return new WaitForSeconds(chargeTime);
        onCharged.Invoke();
        _charged = true;
        chargeSprite.color = Color.magenta;
    }


    public void Fire()
    {
        Fire(this.currentAmmo);
    }
    public void Fire(ScriptableAmmoType ammo)
    {
        if (spawnPoint && gameObject && charged)
        {
            ammo.Spawn(gameObject, spawnPoint.transform.position);
            onFire.Invoke();
        }
        Discharge();
    }

    internal void Discharge()
    {
        onDischarged.Invoke();
        _charged = false;
        StopCoroutine(charger);
        chargeSprite.gameObject.SetActive(false);
        chargeSprite.color = Color.white;
    }
}
