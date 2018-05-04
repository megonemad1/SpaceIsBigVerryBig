using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Collections;

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
    private Vector2Int p = Vector2Int.zero;
    Dictionary<Vector2Int, ColorRulePair> colorRuleCache = new Dictionary<Vector2Int, ColorRulePair>();
    Vector2Int window_size = new Vector2Int(3, 3);
    Vector2Int window_pos = new Vector2Int(-1, -1);

    public IEnumerator CreateShip(int seed, Action<Texture2D> cb)
    {
        return CreateShip(new RandomInitable(seed), cb);
    }
    public IEnumerator CreateShip(RandomInitable R, Action<Texture2D> cb)
    {
        int width = isMirroredY ? Layout.width * 2 : Layout.width;
        int height = isMirroredX ? Layout.height * 2 : Layout.height;
        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
        colorRuleCache.Clear();
        var flattenedColorMap = new Color[(width + 1) * height];
        RectInt TextureWindow = new RectInt(0, 0, width, height);
        Vector2Int nabourpoint = new Vector2Int(0, 0);
        Vector2Int center = new Vector2Int();
        Texture2D doubleBuffer = new Texture2D(width, height, TextureFormat.ARGB32, false);
        FilterArgs filterArguments = new FilterArgs() { r = R, nabours = new List<Color>(), naboursRules = new List<ColorRulePair>() };

        RectInt imageRect = new RectInt(0, 0, Layout.width, Layout.height);
        for (int x = 0; x < Layout.width; x++)
        {
            for (int y = 0; y < Layout.height; y++)
            {
                Color c = getPixelAt(x, y, ref filterArguments, imageRect);
                if (isMirroredY)
                {
                    center.x = width - x - 1;
                    center.y = y;
                    flattenedColorMap[center.x + center.y * width] = c;
                    setBufferPoints(width, flattenedColorMap, ref TextureWindow, ref nabourpoint, ref center, doubleBuffer, ref filterArguments);
                }
                if (isMirroredX)
                {
                    center.x = x;
                    center.y = height - y - 1;
                    flattenedColorMap[center.x + center.y * width] = c;
                    setBufferPoints(width, flattenedColorMap, ref TextureWindow, ref nabourpoint, ref center, doubleBuffer, ref filterArguments);

                }
                if (isMirroredX && isMirroredY)
                {
                    center.x = width - x - 1;
                    center.y = height - y - 1;
                    flattenedColorMap[center.x + center.y * width] = c;
                    setBufferPoints(width, flattenedColorMap, ref TextureWindow, ref nabourpoint, ref center, doubleBuffer, ref filterArguments);

                }
                center.x = x;
                center.y = y;
                flattenedColorMap[center.x + center.y * width] = c;
                setBufferPoints(width, flattenedColorMap, ref TextureWindow, ref nabourpoint, ref center, doubleBuffer, ref filterArguments);

                // if the diagnal top right pixel is on the map
            }
            yield return null;
        }
        doubleBuffer.Apply();


        // connect texture to material of GameObject this script is attached to
        //renderer.material.mainTexture = texture;
        cb.Invoke(doubleBuffer);
        yield break;
    }


    private void setBufferPoints(int width, Color[] flattenedColorMap, ref RectInt TextureWindow, ref Vector2Int nabourpoint, ref Vector2Int center, Texture2D doubleBuffer, ref FilterArgs filterArguments)
    {
        if (TextureWindow.Contains(center))
        {
            filterArguments.color = flattenedColorMap[center.x + width * center.y];
            filterArguments.nabours.Clear();
            for (int dx = -1; dx < 2; dx++)
                for (int dy = -1; dy < 2; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;
                    nabourpoint.x = center.x + dx;
                    nabourpoint.y = center.y + dy;
                    if (TextureWindow.Contains(nabourpoint))
                        filterArguments.nabours.Add(flattenedColorMap[nabourpoint.x + width * nabourpoint.y]);

                }
            foreach (var pixelFilters in filters)
            {
                filterArguments = pixelFilters.applyFilter(filterArguments);
            }
            doubleBuffer.SetPixel(center.x, center.y, filterArguments.color);
        }
    }

    //    ////
    //    Texture2D doubleBuffer = new Texture2D(width, height, TextureFormat.ARGB32, false);
    //    for (int x = 0; x < width; x++)
    //        for (int y = 0; y < height; y++)
    //        {
    //            Color center = texture[x + width * y];
    //            List<Color> nabours = new List<Color>();
    //            Vector2Int point = new Vector2Int(0, 0);
    //            for (int dx = -1; dx < 2; dx++)
    //                for (int dy = -1; dy < 2; dy++)
    //                {
    //                    if (dx == 0 && dy == 0)
    //                        continue;
    //                    point.x = x + dx;
    //                    point.y = y + dy;

    //                    if (window.Contains(point))
    //                    {
    //                        nabours.Add(texture[point.x + width * point.y]);
    //                    }
    //                }
    //            FilterArgs filterArguments = new FilterArgs() { r = R, color = center, nabours = nabours };
    //            foreach (var pixelFilters in filters)
    //            {
    //                filterArguments = pixelFilters.applyFilter(filterArguments);
    //            }
    //            doubleBuffer.SetPixel(x, y, filterArguments.color);


    //        }
    //    // Apply all SetPixel calls
    //    doubleBuffer.Apply();


    //    // connect texture to material of GameObject this script is attached to
    //    //renderer.material.mainTexture = texture;
    //    return doubleBuffer;
    //}

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

    public Color getPixelAt(Vector2Int pos, ref FilterArgs filterArguments, RectInt imageRect)
    {
        return getPixelAt(pos.x, pos.y, ref filterArguments, imageRect);
    }
    public Color getPixelAt(int x, int y, ref FilterArgs filterArguments, RectInt imageRect)
    {
        filterArguments.naboursRules.Clear();
        // the image bounds
        for (int dx = window_pos.x; dx < window_size.x; dx++)
            for (int dy = window_pos.y; dy < window_size.x; dy++)
            {
                //Debug.Log(p);
                // ensure p is on the image
                p.x = dx + x;
                p.y = dy + y;
                if (imageRect.Contains(p))
                    if (dx == 0 && dy == 0)
                    {

                        filterArguments.rule = colorRuleCache.ContainsKey(p) ? colorRuleCache[p] : GetReleventRule(filterArguments.r, Layout.GetPixel(p.x, p.y));
                    }
                    else
                        filterArguments.naboursRules.Add(colorRuleCache.ContainsKey(p) ? colorRuleCache[p] : GetReleventRule(filterArguments.r, Layout.GetPixel(p.x, p.y)));
            }

        Color initialColor;
        if (filterArguments.rule.rule != null)
            initialColor = filterArguments.rule.rule.CalculatePixel(filterArguments.r, filterArguments.naboursRules);
        else
            initialColor = new Color(0, 0, 0, 0);
        return initialColor;
    }



    private ColorRulePair GetReleventRule(RandomInitable R, Color key)
    {
        return R.Pick(Rules.Where(cr => cr.ColorKey.Equals(key)).ToArray());
    }
}