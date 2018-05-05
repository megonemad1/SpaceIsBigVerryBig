using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    MovemonetHarness mover;
    [SerializeField]
    bool useSelf = true;
    public Vector2 PlayerDirectionImput;
    private void Awake()
    {
        PlayerDirectionImput = new Vector2();
    }
    private void OnValidate()
    {
        if (useSelf)
        {
            mover = GetComponent<MovemonetHarness>();
        }

    }
    void FixedUpdate()
    {
        mover.Move(PlayerDirectionImput);
    }
    private void Update()
    {

        PlayerDirectionImput.x = Input.GetAxisRaw("Horizontal");

        PlayerDirectionImput.y = Input.GetAxisRaw("Vertical");
    }
}

