using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableShipType : ScriptableObject
{
    [SerializeField]
    GameObject Actor;
    [SerializeField]
    GameObject Representaion;
    [SerializeField]
    ScriptableDecisionOption[] options;
    [SerializeField]
    public DificultyEnumm spawnChance;

    public GameObject Spawn(RandomInitable seed, int Decisions, Vector3 pos, ShipSpawner Controler)
    {
        // RandomInitable r = seed;
        GameObject newEnermy = Instantiate(Actor, Controler.transform);
        var tmp_R = seed;
        newEnermy.name = string.Format("{0} id = {1}", name, tmp_R.seed);
        newEnermy.transform.position = pos;
        var ai = newEnermy.GetComponent<ShipAi>();
        ai.R = tmp_R;
        for (int i = 0; i < Decisions; i++)
        {
            ai.addDecision(ai.R.Pick(options));
        }
        ai.decisionLoop.StartLoop();
        Instantiate(Representaion, newEnermy.transform).GetComponentsInChildren<ShipTextureCreator>().ToList().ForEach(a => a.setSeed(ai.R.valueInt));
        newEnermy.transform.Rotate(0, 0, 180);
        ai.controler = Controler;
        return newEnermy;
    }
}
