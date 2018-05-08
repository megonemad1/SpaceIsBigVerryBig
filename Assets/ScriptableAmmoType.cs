using System;
using UnityEngine;
[CreateAssetMenu]
public class ScriptableAmmoType : ScriptableObject
{

    static GameObject BulletFolder;
    [SerializeField]
    GameObject ammoPrefab;
    internal void Spawn(GameObject gameObject, Vector3 SpawnLocation)
    {
        if (BulletFolder == null)
        {
            BulletFolder = new GameObject("Bullet Folder");
            BulletFolder.transform.position = Vector3.zero; //just in case
        }
        GameObject bullet = Instantiate(ammoPrefab, BulletFolder.transform);
        bullet.transform.rotation = gameObject.transform.rotation;
        bullet.transform.position = SpawnLocation;
        BulletHandeler bh = bullet.GetComponent<BulletHandeler>();
        if (bh != null)
            bh.sender = gameObject;

    }
}