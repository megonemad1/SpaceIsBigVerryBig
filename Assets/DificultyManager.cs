using System;
using UnityEngine;

internal class DificultyManager : RuntimeSet<DificultyEnumm>
{
    [SerializeField, Range(0,2)]
    public float getCurrentDificultyModifyer;
}