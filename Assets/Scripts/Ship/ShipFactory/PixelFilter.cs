using System;
using System.Collections.Generic;
using UnityEngine;

public class PixelFilter : ScriptableObject
{
    public virtual Color apply(Color c,RandomInitable r, List<ColorRulePair> nabours)
    {
        return c;
    }
}