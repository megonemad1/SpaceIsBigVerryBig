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
        while (!killSpawnLoop)
        {
            shipSeed = r.valueInt;
            GameObject newEnermy = Instantiate(EnermyActorPrefab, transform);
            Instantiate(EnermySpritePrefab, newEnermy.transform).GetComponentsInChildren<ShipTextureCreator>().ToList().ForEach(a=>a.setSeed(shipSeed++));
            newEnermy.transform.LookAt(newEnermy.transform.position + Vector3.down);
            newEnermy.GetComponent<EnermyAI>().controler = this;
            yield return new WaitForSeconds(SpawnTickIntervel);
        }
    }
}
