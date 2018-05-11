using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackDecision : ScriptableDecisionOption
{
    internal override void go(ShipAi enermyAI, ShipSpawner cl)
    {
        Camera cam = cl.cam;
        var bottomLeft = cam.ScreenToWorldPoint(Vector3.zero);
        var topRight = cam.ScreenToWorldPoint(new Vector3(
            cam.pixelWidth, cam.pixelHeight));

        var cameraRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y);
        if (cameraRect.Contains(enermyAI.transform.position))
        {
            AttackHandeler ah = enermyAI.GetComponent<AttackHandeler>();
            ah.Fire();
        }
    }
}
