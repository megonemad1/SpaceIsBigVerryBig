using System;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

[CreateAssetMenu]
public class PixelFilter : ScriptableObject
{

    public virtual FilterArgs applyFilter(FilterArgs a)
    {
        return a;
    }
}