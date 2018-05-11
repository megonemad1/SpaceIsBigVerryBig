using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PlayerScriptableObject : ScriptableObject {


    public int score;
    public string deck_seed;
    public string engine_seed;
    public string cabin_seed;
    [SerializeField]
    public SealFinalScore Seal;

    public void AddScore(int score)
    {
        this.score += score;
    }
    public void setDeckSeed(string seed)
    {
        deck_seed = seed;
    }
    public void setEngineSeed(string seed)
    {
        engine_seed = seed;
    }
    public void setCabbinSeed(string seed)
    {
       cabin_seed = seed;
    }
    public void ClearScore()
    {
        score = 0;
    }
    public void sealPlayerScore()
    {
        Seal.Invoke(this);
    }
}
