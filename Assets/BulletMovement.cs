using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    // Use this for initialization
    void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var dist = speed * Time.deltaTime;
        var direction = transform.up;
        var hits = Physics2D.Raycast(transform.position, direction, dist);
        Debug.DrawRay(transform.position, direction * dist, Color.red);
        if (hits)
        {
            OnTriggerEnter2D(hits.collider);
        }
        transform.position += dist*direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponents<HitLisoner>().ToList().ForEach(l => StartCoroutine(l.hit(collision)));
        Destroy(gameObject);
    }
}
