using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class filterReduceIslands : PixelFilter
{
    [SerializeField]
    float weight;
    [SerializeField]
    Color defaultColor = new Color(0, 0, 0, 0);
    public override FilterArgs applyFilter(FilterArgs a)
    {
        a.color = 9f / (a.nabours.Count + 1) * weight > a.r.value ? a.color : defaultColor;
        return a;
    }
}
