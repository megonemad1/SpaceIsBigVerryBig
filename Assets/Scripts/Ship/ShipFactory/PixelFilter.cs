using System;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

[CreateAssetMenu]
public class PixelFilter : ScriptableObject
{
    [Range(0,1), SerializeField]
    private float _hueVarence = 0.5f;
    [Range(0, 1),SerializeField]
    private float _saturationVarence = 0.5f;
    [Range(0, 1), SerializeField]
    private float _valueVarence = 0.5f;

    public virtual Color apply(Color c,RandomInitable r, List<ColorRulePair> nabours)
    {

        Vector4 hsv = c.ToVector();
        hsv.x += (r.value - 0.5f) * _hueVarence;
        hsv.y += (r.value - 0.5f) * _saturationVarence;
        hsv.z += (r.value - 0.5f) * _valueVarence;
        return hsv.ToColorHSV();
    }
}