using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScorePickup : MonoBehaviour
{

    [SerializeField]
    CollisionEvent PickedUp;
    [SerializeField]
    CollisionEvent Destroyed;
    [SerializeField]
    int score;
    Collider2D c;
    SpriteRenderer r;
    private void Awake()
    {
        c = GetComponent<Collider2D>();
        r = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if its a bullet or not sentiant dont pick it up
        if (collision.GetComponent<BulletHandeler>() || !collision.GetComponent<AttackHandeler>())
            Destroyed.Invoke(collision.gameObject);
        else
            PickedUp.Invoke(collision.gameObject);
        var a = collision.GetComponent<PlayerControler>();
        if (a)
        {
            a.playerdata.AddScore(score);
        }
        c.enabled = false;
        r.enabled = false;
        Destroy(gameObject, 1);
    }

}
