using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setSeeds : MonoBehaviour
{
    [SerializeField]
    ShipTextureCreator cabin;
    [SerializeField]
    ShipTextureCreator deck;
    [SerializeField]
    ShipTextureCreator engine;
    [SerializeField]
    PlayerScriptableObject playerData;
    public void Setup()
    {
        cabin.setSeed(playerData.cabin_seed);
        deck.setSeed(playerData.deck_seed);
        engine.setSeed(playerData.engine_seed);
    }
}
