using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu]
public class Template : ScriptableObject
{
    [SerializeField]
    public Texture2D Layout;
    [SerializeField]
    public List<ColorRulePair> Rules;
    [SerializeField]
    public bool isMirroredY;
    [SerializeField]
    public bool isMirroredX;
    [SerializeField]
    public PixelFilter[] filters;

    private void OnEnable()
    {
        Init();
    }
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        Dictionary<Color, List<ColorRulePair>> lookup = new Dictionary<Color, List<ColorRulePair>>(); ;
        if (Rules != null)
            Rules.ForEach(r =>
            {
                if (!lookup.ContainsKey(r.ColorKey))
                    lookup[r.ColorKey] = new List<ColorRulePair>();
                lookup[r.ColorKey].Add(r);
            });
        HashSet<Color> colorSet = new HashSet<Color>(Layout.GetPixels());
        Rules = new List<ColorRulePair>();
        foreach (var c in colorSet)
            if (lookup.ContainsKey(c))
                Rules.AddRange(lookup[c]);
            else
                Rules.Add(new ColorRulePair() { ColorKey = c, rule = null });
    }
    private void OnValidate()
    {
        if (!Rules.All(r => new HashSet<Color>(Layout.GetPixels()).Any(c => r.ColorKey == c)))
        {
            Init();
        }
    }
    public Color getPixelAt(int x, int y, RandomInitable R)
    {
        return getPixelAt(x, y, R, true);
    }
    public Color getPixelAt(int x, int y, RandomInitable R, bool useFilters)
    {
        return getPixelAt(new Vector2Int(x, y), R, useFilters);
    }
    public Color getPixelAt(Vector2Int pos, RandomInitable R, bool useFilters)
    {
        var nabours = new List<ColorRulePair>();
        ColorRulePair center = null;
        Vector2Int window_size = new Vector2Int(3, 3);
        Vector2Int window_pos = new Vector2Int(-1, -1);
        // the image bounds
        RectInt imageRect = new RectInt(0, 0, Layout.width, Layout.height);
        Dictionary<Vector2Int, ColorRulePair> colorRuleCache = new Dictionary<Vector2Int, ColorRulePair>();
        for (int dx = window_pos.x; dx < window_size.x; dx++)
            for (int dy = window_pos.y; dy < window_size.x; dy++)
            {
                Vector2Int p = new Vector2Int(dx, dy) + pos;
                //Debug.Log(p);
                // ensure p is on the image
                if (imageRect.Contains(p))
                    if (dx == 0 && dy == 0)
                    {

                        center = colorRuleCache.ContainsKey(p) ? colorRuleCache[p] : GetReleventRule(R, Layout.GetPixel(p.x, p.y));
                    }
                    else
                        nabours.Add(colorRuleCache.ContainsKey(p) ? colorRuleCache[p] : GetReleventRule(R, Layout.GetPixel(p.x, p.y)));
            }

        Color initialColor;
        if (center.rule != null)
            initialColor = center.rule.CalculatePixel(R, nabours);
        else
            initialColor = new Color(0, 0, 0, 0);

        if (useFilters)
            for (int i = 0; i < filters.Length; i++)
                if (filters[i] != null)
                    initialColor = filters[i].apply(initialColor, R, nabours);
        return initialColor;
    }



    private ColorRulePair GetReleventRule(RandomInitable R, Color key)
    {
        return R.Pick(Rules.Where(cr => cr.ColorKey.Equals(key)).ToArray());
    }
}