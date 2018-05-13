using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PlayerScriptableObject : ScriptableObject
{

    public GameObject player;
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
    public bool seenInto { get { return _seenInto; } set { _seenInto = value; } }
    public bool _seenInto;
    public float AtackModifyer { get { return stats.x; } set { stats.x = value; } }
    public float HealthModifyer { get { return stats.y; } set { stats.y = value; } }
    public float SpeedModifyer { get { return stats.z; } set { stats.z = value; } }
    public Vector3 stats = Vector3.zero;
    public void setStat(Vector3 allotment)
    {
        var al = allotment.normalized;
        var inverse = Vector3.one - al;
        stats.Scale(inverse);
        stats += allotment;
        stats.Normalize();
    }
}
