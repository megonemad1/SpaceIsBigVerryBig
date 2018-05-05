using UnityEngine;

internal class MovementDecision : ScriptableDecisionOption
{

    [Range(-1, 1), SerializeField]
    float movement;
    internal override void go(EnermyAI enermyAI, SpawnEnemy cl)
    {
        enermyAI.xDirection = movement;
    }
}