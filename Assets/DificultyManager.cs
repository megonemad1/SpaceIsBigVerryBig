using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
internal class DificultyManager : ScriptableObject
{
    public static DificultyManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        if (Instance != this)
            Destroy(this);
    }
    private void OnEnable()
    {
        if (Instance == null)
            Instance = this;
        if (Instance != this)
            Destroy(this);
    }
    [SerializeField, Range(0, 2)]
    public float currentDificultyModifyer;
    [SerializeField]
    int movingAvrCount = 3;
    [SerializeField]
    int CurrentProjectedScore
    {
        get
        {
            if (movingAvr.Count == 0)
                return 1;
            return movingAvr.Sum() / movingAvr.Count;
        }
    }
    [SerializeField]
    List<int> movingAvr = new List<int>();
    public void updateProjectedScore(PlayerScriptableObject playerData)
    {
        float projection = CurrentProjectedScore;
        float scoreDiffrence = playerData.score - projection;
        float diffrenceMean = scoreDiffrence / projection;
        movingAvr.Add(playerData.score);
        while (movingAvr.Count > movingAvrCount)
            movingAvr.RemoveAt(0);
        var percentChange = currentDificultyModifyer * (diffrenceMean + 1);
        // y = (2 / PI) * arctan(100 * x) + 1
        currentDificultyModifyer = Mathf.Lerp(currentDificultyModifyer, currentDificultyModifyer * percentChange, 1f / movingAvrCount);    
    }
    public void setDificulty(float dificulty)
    {
        currentDificultyModifyer = dificulty;
    }
}