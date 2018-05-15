using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMusicPickup : MonoBehaviour
{

    [SerializeField]
    CollisionEvent PickedUp;
    [SerializeField]
    CollisionEvent Destroyed;
    [SerializeField]
    TrackList tl;
    [SerializeField]
    public int trackid;
    Collider2D c;
    SpriteRenderer r;
    bool isdestroying = false;
    IEnumerator check;
    private void Awake()
    {
        trackid = tl.getRandomId();
        c = GetComponent<Collider2D>();
        r = GetComponent<SpriteRenderer>();
        check = CheckTrack();
        StartCoroutine(check);
        if (!tl.isUnlocked(trackid))
            r.enabled = true;
       
    }
    private IEnumerator CheckTrack()
    {
        while (!isdestroying)
        {
            if (tl.isUnlocked(trackid))
            {
                isdestroying = true;
                Destroy(this.gameObject);
            }
            yield return new WaitForSeconds(1f);
        }
        yield break;
    }
    private void OnDestroy()
    {
        isdestroying = true;
        StopCoroutine(check);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isdestroying)
        {
            // if its a bullet or not sentiant dont pick it up
            if (collision.GetComponent<PlayerControler>())
            {
                if (tl.unlock(trackid))
                    PickedUp.Invoke(collision.gameObject);
            }
            else
                Destroyed.Invoke(collision.gameObject);
            c.enabled = false;
            r.enabled = false;
            isdestroying = true;
            Destroy(gameObject, 1);
        }
    }
}
