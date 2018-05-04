using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using System.Linq;

[CreateAssetMenu]
public class ColorValueAvrage : PixelFilter
{

    [Range(0, 1), SerializeField]
    private float _hueVarence = 0.5f;
    [Range(0, 1), SerializeField]
    private float _saturationVarence = 0.5f;
    [Range(0, 1), SerializeField]
    private float _valueVarence = 0.5f;

    public override FilterArgs applyFilter(FilterArgs a)
    {
        float center_H, center_S, center_V, center_A;
        float avr_H, avr_S, avr_V;
        Color.RGBToHSV(a.color, out center_H, out center_S, out center_V);
        center_A = a.color.a;
        avr_S = 1;
        avr_V = 1;
        a.nabours.ForEach((color) =>
        {

            float tmp_H, tmp_S, tmp_V;
            Color.RGBToHSV(color, out tmp_H, out tmp_S, out tmp_V);
            avr_S += tmp_S;
            avr_V += tmp_V;
        });
        avr_V += center_V;
        avr_S += center_S;
        float f = 1f / (a.nabours.Count + 1);
        avr_V *= f;
        avr_S *= f;
        Color x = Color.HSVToRGB(center_H, avr_S, avr_V);
        x.a = center_A;
        a.color = x;
        
        return a;
    }

}
