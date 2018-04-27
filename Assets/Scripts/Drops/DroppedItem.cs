using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{

    public GameObject dropped_item;
    Collider2D pickup_area;
    SpriteRenderer sprite_renderer;

    // Use this for initialization
    void Start()
    {
        pickup_area = this.GetComponent<Collider2D>();
        sprite_renderer = this.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy(other.gameObject);
        Debug.Log(other.name);
        ICollector c = other.gameObject.GetComponent<ICollector>();
        if (c != null)
        {
            c.PickUp(dropped_item);
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("something has hit me");
    }

    // Update is called once per frame
    void Update()
    {
        if (dropped_item != null)
        {
            IDrop d = dropped_item.GetComponent<IDrop>();
            sprite_renderer.sprite = d.GetIcon();
            
        }
      
    }
}
