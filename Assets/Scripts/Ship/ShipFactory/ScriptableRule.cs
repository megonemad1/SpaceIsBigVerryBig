using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu]
public class ScriptableRule : ScriptableObject
{
    [SerializeField]
    protected List<ColorProbablityPair> _probablilitys;
    public virtual Color CalculatePixel(RandomInitable R, List<ColorRulePair> nabours)
    {
        return R.WeightedPick(_probablilitys.Select(p => new KeyValuePair<float, ColorProbablityPair>(p.probability, p)).ToList()).GetColor(R, nabours);
    }

}