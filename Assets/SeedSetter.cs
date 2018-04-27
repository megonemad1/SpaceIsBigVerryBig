using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SeedSetter : MonoBehaviour
{

    [SerializeField]
    List<ObserverInt> seeds;
    // Use this for initialization
    void Awake()
    {
        init();
    }
    private void OnValidate()
    {
        init();
    }
    private void init()
    {
        seeds = new List<ObserverInt>(transform.GetComponentsInChildren<ShipTextureCreator>().Select(stc=>stc.getSeed()));
    }
    // Update is called once per frame
    void Update()
    {

    }
}
