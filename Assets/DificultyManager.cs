using System;
using UnityEngine;

internal class DificultyManager:ScriptableObject
{
    public static DificultyManager Instance;

    private void OnEnable()
    {
        if (Instance == null)
            Instance = this;
        if(Instance != this)
            Destroy(this);
    }
    [SerializeField, Range(0,2)]
    public float getCurrentDificultyModifyer;
}