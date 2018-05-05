using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemonet : MonoBehaviour
{
    private Rigidbody2D rbody = null;
    private string tmp = "";
    // Use this for initialization
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (rbody != null)
        {
            var spd = rbody.velocity;

            if (Input.GetKey(KeyCode.A))
            {
                if(tmp == "D")
                {
                    rbody.velocity = Vector2.Lerp(rbody.velocity, Vector2.zero, 0.5f);
                }
                rbody.AddForce(-this.transform.right,ForceMode2D.Impulse);
                tmp = "A";
            }
            if (Input.GetKey(KeyCode.W))
            {
                if(tmp == "S")
                {
                    rbody.velocity = Vector2.Lerp(rbody.velocity, Vector2.zero, 0.5f);
                }
                rbody.AddForce(this.transform.up,ForceMode2D.Impulse);
                tmp = "W";
            }
            if (Input.GetKey(KeyCode.S))
            {
                if(tmp == "W")
                {
                    rbody.velocity = Vector2.Lerp(rbody.velocity, Vector2.zero, 0.5f);
                }
                rbody.AddForce(-this.transform.up,ForceMode2D.Impulse);
                tmp = "S";
            }
            if (Input.GetKey(KeyCode.D))
            {
                if(tmp == "A")
                {
                    rbody.velocity = Vector2.Lerp(rbody.velocity, Vector2.zero, 0.5f);
                }
                rbody.AddForce(this.transform.right,ForceMode2D.Impulse);
                tmp = "D";
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
