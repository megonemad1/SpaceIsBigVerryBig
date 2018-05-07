using UnityEngine;

[CreateAssetMenu]
internal class MovementDecision : ScriptableDecisionOption
{

    [Range(-1, 1), SerializeField]
    float movement;
    internal override void go(ShipAi enermyAI, ShipSpawner cl)
    {
        enermyAI.xDirection = movement;
    }
}