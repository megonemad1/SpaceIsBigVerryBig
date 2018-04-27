using System.Collections.Generic;
using UnityEngine;
using System.Linq;

internal class ScriptableNabourRule : ScriptableRule
{
    float nabourWeight = 1;
    [SerializeField]
    List<ScriptableRule> validNabourTypes;
    [SerializeField]
    ScriptableRule NotNabour;
    public override Color CalculatePixel(RandomInitable R, List<ColorRulePair> nabours)
    {
        var nabourRules = nabours.Select(cr => cr.rule);
        if (!nabourRules.Contains(this))
            return NotNabour.CalculatePixel(R, nabours);
        var matchingNabours = nabourRules.Sum(r => r==this || validNabourTypes.Contains(r) ? 1 : 0);
        var max = _probablilitys.Sum(i => i.probability);
        var gen = R.value * max;
        foreach (var p in _probablilitys)
        {
            if (gen <= p.probability)
                return p.GetColor(R, nabours);
            else
                gen -= p.probability;
        }
        return _probablilitys.Last().GetColor(R, nabours);
    }
}