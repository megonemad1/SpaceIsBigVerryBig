using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackDecision : ScriptableDecisionOption
{
    [SerializeField]
    int FixedUpdateFramesChargedFor = 200;
    internal override void go(ShipAi enermyAI, ShipSpawner cl)
    {
        if (cl)
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
                enermyAI.StartCoroutine(chargeAndFire(enermyAI, cl));
            }
        }
    }

    IEnumerator chargeAndFire(ShipAi enermyAI, ShipSpawner cl)
    {
        AttackHandeler ah = enermyAI.GetComponent<AttackHandeler>();
        ah.ChargeUp();
        for (int i = 0; i < FixedUpdateFramesChargedFor; i++)
        {
            yield return new WaitForFixedUpdate();
            if (enermyAI == null || ah ==null || ah.spawnPoint == null)
                yield break;
            var hit = Physics2D.Raycast(ah.spawnPoint.transform.position, enermyAI.transform.up,10);
            Debug.DrawRay(ah.spawnPoint.transform.position, enermyAI.transform.up * 10);
            if (hit)
            {
                Debug.Log("hit", hit.transform);
                Debug.Log("by", enermyAI.transform);
                var other_team = hit.transform.GetComponent<TeamHandeler>();
                var my_team = enermyAI.GetComponent<TeamHandeler>();
                if (!my_team.Equals(other_team))
                {
                    if (ah.charged)
                    {
                        ah.Fire();
                    }
                }
            }
        }
        ah.Discharge();
    }
}
