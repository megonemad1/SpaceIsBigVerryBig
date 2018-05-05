using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovemonetHarness : MonoBehaviour
{
    private Rigidbody2D rbody = null;
    [SerializeField]
    float Force = 1;
    private Vector2 LastDirectionMoved;
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
            //if the direction has changed and it hasnt changed from zero
            if (Mathf.Sign(LastDirectionMoved.x) != Mathf.Sign(direction.x) && LastDirectionMoved.x != 0)
            {
                rbody.velocity = Vector2.zero;
            }
            rbody.AddForce(direction * Force * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        else
            Debug.LogError("Movement Harness Missing Rigidbody2D", this.gameObject);

    }
}
