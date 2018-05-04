using System.Collections.Generic;
using UnityEngine;

public class FilterArgs
{
    public FilterArgs()
    {
    }

    public Color color { get; set; }
    public ColorRulePair rule { get; set; }
    public List<Color> nabours { get; set; }
    public List<ColorRulePair> naboursRules { get; set; }
    public RandomInitable r { get; set; }
}