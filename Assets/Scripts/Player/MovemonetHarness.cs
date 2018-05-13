using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class MovemonetHarness : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rbody = null;
    [SerializeField]
    float Force = 1;
    [SerializeField]
    float dragAmount = 1;
    [SerializeField]
    Vector2 topSpeed = new Vector2(10, 10);
    [SerializeField]
    PlayerScriptableObject playerdata;
    //onlu global for memory management;
    private Vector2 newVelocity;

    public Vector2 LastDirectionMoved { get; private set; }

    // Use this for initialization
    void Awake()
    {
        if (!rbody)
            rbody = this.GetComponent<Rigidbody2D>();
        if (playerdata)
        {
            Force *= 0.5f + playerdata.SpeedModifyer*2;
            topSpeed.x *= 0.5f + playerdata.SpeedModifyer; 
            topSpeed.y *= 0.5f + playerdata.SpeedModifyer; 
        }
    }

    public void Move(Vector2 direction)
    {
        direction = direction.normalized;
        if (rbody != null)
        {
            newVelocity = rbody.velocity;
            //if the direction has changed

            //if the direction has changed and it hasnt changed from zero
            if ((newVelocity.x > 0 && direction.x < 0) || (newVelocity.x < 0 && direction.x > 0) || direction.x == 0)
                newVelocity.x -= newVelocity.x * dragAmount * Time.fixedDeltaTime;

            //if the direction has changed and it hasnt changed from zero
            if ((newVelocity.y > 0 && direction.y < 0) || (newVelocity.y < 0 && direction.y > 0) || direction.y == 0)
                newVelocity.y -= newVelocity.y * dragAmount * Time.fixedDeltaTime;

            newVelocity.x = Mathf.Min(Mathf.Abs(newVelocity.x), topSpeed.x) * Mathf.Sign(newVelocity.x);
            newVelocity.y = Mathf.Min(Mathf.Abs(newVelocity.y), topSpeed.y) * Mathf.Sign(newVelocity.y);
            rbody.velocity = newVelocity;
            rbody.AddForce(direction * Force * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        else
            Debug.LogError("Movement Harness Missing Rigidbody2D", this.gameObject);
        LastDirectionMoved = direction;
    }
}
