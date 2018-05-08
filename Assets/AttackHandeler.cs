using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackHandeler : MonoBehaviour
{
    [SerializeField]
    GameObject spawnPoint;

    [SerializeField]
    UnityEvent onFire;



    [SerializeField]
    ScriptableAmmoType currentAmmo;

    public void Fire()
    {


        Fire(this.currentAmmo);

    }
    public void Fire(ScriptableAmmoType ammo)
    {
        ammo.Spawn(gameObject, spawnPoint.transform.position);
        onFire.Invoke();
    }
}
