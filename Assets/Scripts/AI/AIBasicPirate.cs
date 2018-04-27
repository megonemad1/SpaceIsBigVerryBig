using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasicPirate : MonoBehaviour, IArtInt
{
    enum State
    {
        idle,
        moving,
        targeting,
        shooting,
        raming,
        reloading,
        dodging
    }
    State state;
    public GameObject player;
    public float agro = 10;
    Rigidbody2D rigidbody2d;
    IWeppon weppon;
    public float turningChance = 0.25f;
    public float turningDirectionLeftChance = 0.5f;
    public float turningTorque = 1;
    public float idleChance = 0.3f;
    public float forwardForce = 10;
    public float dodgeChance = 0.1f;
    public string Currentstate="na";
    // Use this for initialization
    void Start()
    {
        state = State.idle;
        rigidbody2d = this.GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {
        Currentstate = getState();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Step();
    }

    public string getState()
    {
        return Enum.GetName(typeof(State),state);
    }

    public string[] getStates()
    {
       return Enum.GetNames(typeof(State));
    }

    void WepponHit(GameObject g)
    {
        if (g != player)
        {
            state = State.targeting;
        }
    }

    public void Step()
    {
        ICollector c = GetComponent<ICollector>();
        if (c != null)
        {
            this.weppon = c.getCurrentWeppon();
        }
        switch (state)
        {
            case State.idle:
                if (Vector3.SqrMagnitude(this.transform.position- player.transform.position) < agro)
                {
                    if (UnityEngine.Random.value < dodgeChance)
                        state = State.targeting;
                    else
                        state = State.dodging;
                }
                else
                {
                    state = State.moving;
                }
                break;
            case State.moving:
                if (UnityEngine.Random.value < turningChance)
                    if (UnityEngine.Random.value < turningDirectionLeftChance)
                    {
                        rigidbody2d.AddTorque(-turningTorque);
                    }
                    else
                    {

                        rigidbody2d.AddTorque(turningTorque);
                    }
                else
                {
                    if (UnityEngine.Random.value < idleChance)
                    {
                        this.state = State.idle;
                    }
                    else
                        rigidbody2d.AddForce(transform.up* forwardForce * Time.deltaTime);
                }


                break;
            case State.raming:
                if (UnityEngine.Random.value < idleChance)
                    rigidbody2d.AddForce(transform.up * forwardForce * 2);
                else
                    state = State.idle;
                break;
            case State.reloading:
                state = State.idle;
                break;
            case State.shooting:
                if (weppon != null && weppon.GetAmmo() > 0 && UnityEngine.Random.value < idleChance)
                    weppon.StartFire(WepponHit);
                else
                    state = State.idle;
                break;
            case State.targeting:
                if (Vector3.SqrMagnitude(this.transform.position - player.transform.position) >= agro)
                {
                    state = State.idle;
                }
                else
                {

                    this.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, agro);
                    this.transform.gameObject.layer = LayerMask.NameToLayer("Default");
                    Debug.Log("ray");
                    if (hit && hit.transform.gameObject.GetHashCode() == player.gameObject.GetHashCode())
                    {
                        Debug.Log("hit");
                        if (weppon != null && weppon.GetAmmo() > 0)
                        {
                            state = State.shooting;
                            Debug.Log("Fire!");
                        }
                        else
                        {
                            state = State.raming;
                            Debug.Log("Ram!");
                        }
                    }
                    else
                    {
                       // Vector3 cross = Vector3.Cross(transform.rotation * Vector3.forward, Quaternion.Euler(player.transform.position-transform.position) * Vector3.forward);
                        if (transform.InverseTransformPoint(player.transform.position - transform.position).x>0)
                        {

                            rigidbody2d.AddTorque(-turningTorque);
                        }
                        else
                        {

                                rigidbody2d.AddTorque(turningTorque);
                        }
                        

                    }
                }
                break;
            case State.dodging:
                if (turningDirectionLeftChance < UnityEngine.Random.value)
                    rigidbody2d.AddTorque(-turningTorque*10);
                else
                    rigidbody2d.AddTorque(turningTorque*10);
                state = State.moving;
                break;
        }
    }
}
