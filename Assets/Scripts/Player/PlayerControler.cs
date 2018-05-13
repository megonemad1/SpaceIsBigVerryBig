using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    MovemonetHarness mover;
    [SerializeField]
    AttackHandeler attacker;
    [SerializeField]
    PauseHandeler pauseHandeler;
    [SerializeField]
    bool useSelf = true;
    public Vector2 PlayerDirectionImput;
    [SerializeField]
    public PlayerScriptableObject playerdata;
    private void Awake()
    {
        PlayerDirectionImput = new Vector2();
        playerdata.player = this.gameObject;
    }
    private void OnValidate()
    {
        if (useSelf)
        {
            mover = GetComponent<MovemonetHarness>();
            attacker = GetComponent<AttackHandeler>(); 
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

        if (Input.GetButtonDown("Jump"))
        {
            attacker.ChargeUp();
        }
        if (Input.GetButtonUp("Jump"))
        {
            attacker.Fire();
        }
        if (Input.GetButtonDown("Cancel"))
        {
            pauseHandeler.TogglePause();
        }
    }
}

