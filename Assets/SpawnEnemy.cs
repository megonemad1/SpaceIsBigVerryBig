using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject EnermyActorPrefab;

    [SerializeField]
    GameObject EnermySpritePrefab;

    [SerializeField]
    ScriptableDecisionOption[] options;
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

    IEnumerator spawnTick;

    private void OnValidate()
    {
        seed.OnValidate();
        Debug.DrawLine(transform.position + Vector3.left * left, transform.position + Vector3.right * right);
    }
    private void Awake()
    {
        spawnTick = SpawnTick();
        r = new RandomInitable();
        seed.OnChanged += (_new, _old) => r.InitState(_new);
    }
    private void OnEnable()
    {
        StartCoroutine(spawnTick);
    }
    private void OnDisable()
    {
        StopCoroutine(spawnTick);
    }
#if UNITY_EDITOR
    private void Update()
    {
        Debug.DrawLine(transform.position + Vector3.left * left, transform.position + Vector3.right * right);
    }
#endif
    private IEnumerator SpawnTick()
    {
        int shipSeed = 0;
        List<ScriptableDecisionOption> decisions = new List<ScriptableDecisionOption>();
        while (!killSpawnLoop)
        {
            shipSeed = r.valueInt;
            if (true)
            {
                GameObject newEnermy = Instantiate(EnermyActorPrefab, transform);
                var tmp_R = new RandomInitable(shipSeed);
                newEnermy.transform.position += Vector3.right * tmp_R.Range(-left, right);
                var ai = newEnermy.GetComponent<EnermyAI>();
                ai.R = tmp_R;
                decisions.Clear();
                for (int i = 0; i < DecisionsPerShip; i++)
                    decisions.Add(r.Pick(options));
                ai.DecisionOptions = decisions;
                Instantiate(EnermySpritePrefab, newEnermy.transform).GetComponentsInChildren<ShipTextureCreator>().ToList().ForEach(a => a.setSeed(ai.R.valueInt));
                newEnermy.transform.Rotate(0, 0, 180);
                newEnermy.GetComponent<EnermyAI>().controler = this;
            }
            yield return new WaitForSeconds(SpawnTickIntervel);
        }
    }
}
