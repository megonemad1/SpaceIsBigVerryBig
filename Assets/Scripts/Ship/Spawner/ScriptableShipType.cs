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
    [SerializeField]
    public int ScoreValue;
    [SerializeField]
    int team;
    [SerializeField]
    ScriptableItemDrop[] items;

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
        var teemh = newEnermy.GetComponent<TeamHandeler>();
        teemh.team = this.team;
        var parts = repr.GetComponentsInChildren<ShipTextureCreator>();
        foreach (var a in parts)
        {

            var repr_seed = ai.R.valueInt;
            a.setSeed(repr_seed);
        }
        ai.controler = Controler;
        ai.ScoreValue = ScoreValue;
        var destroyer = newEnermy.GetComponent<DestroyOnAnimationExit>();
        destroyer.animationIndex = ai.R.Pick(explosions);
        var dropHandeler = newEnermy.GetComponent<DropHandeler>();
        var item = ai.R.Pick(items);
        if (item.chalange.getCr() >= ai.R.value)
        {
            var a = ai.R.Pick(items);
            dropHandeler._drop = a;
        }
        return newEnermy;
    }
}
