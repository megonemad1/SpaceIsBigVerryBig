using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ColorProbablityPair
{
    [SerializeField]
    bool isRefrence = false;
    [SerializeField]
    Color color;
    [SerializeField]
    ScriptableRule rule;
    [SerializeField]
    internal float probability;

    internal Color GetColor(RandomInitable R, List<ColorRulePair> nabours)
    {
        return isRefrence ? rule.CalculatePixel(R, nabours) : color;
    }

}