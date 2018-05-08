using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackDecision : ScriptableDecisionOption
{

    internal override void go(ShipAi enermyAI, ShipSpawner cl)
    {
        AttackHandeler ah = enermyAI.GetComponent<AttackHandeler>();
        ah.Fire();
    }
}
