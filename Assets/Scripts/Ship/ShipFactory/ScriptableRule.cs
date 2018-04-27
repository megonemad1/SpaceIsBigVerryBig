using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu]
public class ScriptableRule:ScriptableObject
{
    [SerializeField]
    protected List<ColorProbablityPair> _probablilitys;
    public virtual Color CalculatePixel(RandomInitable R, List<ColorRulePair> nabours)
    {
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