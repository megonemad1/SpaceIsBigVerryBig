using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShipSpawner : MonoBehaviour
{
    [SerializeField]
    ScriptableShipType[] ships;

    [SerializeField]
    int DecisionsPerShip;
    [SerializeField]
    float SpawnTickIntervel = 1;
    [SerializeField]
    ObserverInt seed;
    RandomInitable r;
    [HideInInspector]
    public bool killSpawnLoop = false;
    [SerializeField]
    float left, right;
    [SerializeField]
    CoroutineLoop _SpawnTick;



    private void OnValidate()
    {
        seed.OnValidate();
        Debug.DrawLine(transform.position + Vector3.left * left, transform.position + Vector3.right * right);
    }
    private void Awake()
    {
        r = new RandomInitable(seed.Value);
        seed.OnChanged += (_new, _old) => r.InitState(_new);
        _SpawnTick.onTick += SpawnTick;
        _SpawnTick.StartLoop();
    }

#if UNITY_EDITOR
    private void Update()
    {
        Debug.DrawLine(transform.position + Vector3.left * left, transform.position + Vector3.right * right);
    }
#endif
    void SpawnTick()
    {
        var seed = r.valueInt;
        Debug.Log("spawning ship id: " + seed);
        RandomInitable ship_R = new RandomInitable(seed);
        ScriptableShipType shipToSpawn = ship_R.Pick(ships);
        if (shipToSpawn.spawnChance.getCr() >= r.value)
        {
            Vector3 newPos = Vector3.right * ship_R.Range(-left, right) + this.transform.position;
            shipToSpawn.Spawn(ship_R, DecisionsPerShip, newPos, this);
        }
    }
}

