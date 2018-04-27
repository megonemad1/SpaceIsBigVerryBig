using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemonet : MonoBehaviour
{
    private Rigidbody2D rbody = null;
    // Use this for initialization
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (rbody != null)
        {
            if (Input.GetKey(KeyCode.A))
                rbody.AddTorque(0.1f, ForceMode2D.Impulse);

            if (Input.GetKey(KeyCode.W))
                rbody.AddForce(this.transform.up,ForceMode2D.Impulse);

            if (Input.GetKey(KeyCode.S))
            {
                rbody.velocity = Vector2.Lerp(rbody.velocity, Vector2.zero, 0.05f);
                rbody.angularVelocity = Mathf.Lerp(rbody.angularVelocity, 0f, 0.05f);
            }
            if (Input.GetKey(KeyCode.D))
                rbody.AddTorque(-0.1f, ForceMode2D.Impulse);

        }
       
    }
    // Update is called once per frame
    void Update()
    {

    }
}
