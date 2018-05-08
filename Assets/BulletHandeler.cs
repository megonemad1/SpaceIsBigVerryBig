using System;
using System.Linq;
using UnityEngine;

public class BulletHandeler:MonoBehaviour
{
    [SerializeField]
    public GameObject sender;
    [SerializeField]
    public ScriptableDamageType damageType;

    internal void hit(Collider2D collision)
    {
        if (collision.gameObject != sender)
        {
            var lisoners = collision.GetComponents<HitLisoner>();
            Debug.Log(lisoners.Length);
            foreach (var l in lisoners)
                StartCoroutine(l.hit(collision, this));
            Destroy(gameObject);
        }
    }
}