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
    [SerializeField]
    List<int> explosions;

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
        var repr = Instantiate(Representaion, newEnermy.transform);
        repr.name = repr.name.Replace("(Clone)", "");
        var parts = repr.GetComponentsInChildren<ShipTextureCreator>();
        Debug.Log(parts.Length);
        foreach (var a in parts)
        {

            var repr_seed = ai.R.valueInt;
            a.setSeed(repr_seed);
            Debug.Log("seed: " + repr_seed, a);
        }
        ai.controler = Controler;
        var destroyer = newEnermy.GetComponent<DestroyOnAnimationExit>();
        destroyer.animationIndex = ai.R.Pick(explosions);
        return newEnermy;
    }
}
