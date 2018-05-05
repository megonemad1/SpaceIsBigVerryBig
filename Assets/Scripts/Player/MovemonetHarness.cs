using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class MovemonetHarness : MonoBehaviour
{
    private Rigidbody2D rbody = null;
    [SerializeField]
    float Force = 1;
    private Vector2 LastDirectionMoved;
    //onlu global for memory management;
    private Vector2 newVelocity;
    // Use this for initialization
    void Awake()
    {
        rbody = this.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        direction = direction.normalized;
        if (rbody != null)
        {
            newVelocity = rbody.velocity;
            //if the direction has changed and it hasnt changed from zero
            if (Mathf.Sign(LastDirectionMoved.x) != Mathf.Sign(direction.x) && LastDirectionMoved.x != 0)
            {
                newVelocity.x = 0;
            }
            //if the direction has changed and it hasnt changed from zero
            if (Mathf.Sign(LastDirectionMoved.y) != Mathf.Sign(direction.y) && LastDirectionMoved.y != 0)
            {
                newVelocity.y = 0;
            }
            rbody.velocity = newVelocity;
            rbody.AddForce(direction * Force * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        else
            Debug.LogError("Movement Harness Missing Rigidbody2D", this.gameObject);
        LastDirectionMoved = direction;
    }
    private void FixedUpdate()
    {
        Vector2 Drag = Vector2.one;
        if (LastDirectionMoved.x == 0)
            Drag.x *= 0.95f;
        if (LastDirectionMoved.y == 0)
            Drag.y *= 0.95f;
        
        rbody.velocity= rbody.velocity.ScaleChain(Drag);


    }
}
