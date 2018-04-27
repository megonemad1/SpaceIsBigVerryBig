
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomInitable
{
    public RandomInitable()
    {
        init(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
    }
    public RandomInitable(int seed)
    {
        init(seed);
    }

    private void init(int seed)
    {
        lock (_lock)
        {
            Random.InitState(seed);
            s = Random.state;
        }
        this._seed = seed;
    }

    static object _lock = new object();
    int _seed;
    Random.State s;
    private T Decorate<T>(System.Func<T> p)
    {
        lock (_lock)
        {
            Random.state = s;
            T t = p.Invoke();
            s = Random.state;
            return t;
        }
    }

    public T Pick<T>(ICollection<T> c)
    {
        if (c.Count == 0)
            return default(T);
        var e = c.GetEnumerator();
        int x = Range(0,c.Count); // random range int is not max inclusive
        e.MoveNext();
        for (int i = 0; i < x; i++)
            e.MoveNext();
        return e.Current;
    }
    public IOrderedEnumerable<T> Chose<T>(ICollection<T> c, int num)
    {

        int count = c.Count;

        if (count < num)
        {
            int fullSelections = num / count;
            int remainderSelections = num % count;
            List<T> rval = new List<T>();
            for (int i = 0; i < fullSelections; i++)
            {
                rval.AddRange(Shuffle(c));
            }
            rval.AddRange(Chose(c, remainderSelections));
            return Shuffle(rval);
        }
        else
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < count; i++)
            {
                indexes.Add(i);
            }
            for (int i = 0; i < count - num; i++)
            {
                indexes.RemoveAt(Range(0, indexes.Count));
            }
            List<T> rval = new List<T>();
            int index = 0;
            foreach (var item in c)
            {
                if (indexes.Contains(index))
                {
                    rval.Add(item);
                }
                index++;
            }
            return Shuffle(rval);
        }
    }

    public IOrderedEnumerable<T> Shuffle<T>(IEnumerable<T> list)
    {
        return new List<T>(list).OrderBy((k) => { return valueInt; });
    }
    //
    // Summary:
    //     ///
    //     Returns a random point inside a circle with radius 1 (Read Only).
    //     ///
    public Vector2 insideUnitCircle { get { return Decorate(() => Random.insideUnitCircle); } }



    //
    // Summary:
    //     ///
    //     Returns a random point inside a sphere with radius 1 (Read Only).
    //     ///
    public Vector3 insideUnitSphere { get { return Decorate(() => Random.insideUnitSphere); } }
    //
    // Summary:
    //     ///
    //     Returns a random point on the surface of a sphere with radius 1 (Read Only).
    //     ///
    public Vector3 onUnitSphere { get { return Decorate(() => Random.onUnitSphere); } }
    //
    // Summary:
    //     ///
    //     Returns a random rotation (Read Only).
    //     ///
    public Quaternion rotation { get { return Decorate(() => Random.rotation); } }
    //
    // Summary:
    //     ///
    //     Returns a random rotation with uniform distribution (Read Only).
    //     ///
    public Quaternion rotationUniform { get { return Decorate(() => Random.rotationUniform); } }
    [System.Obsolete("Deprecated. Use InitState() function or Random.state property instead.")]
    public int seed { get { return _seed; } set { init(value); } }

    //
    // Summary:
    //     ///
    //     Returns a random number between 0.0 [inclusive] and 1.0 [inclusive] (Read Only).
    //     ///
    public float value { get { return Decorate(() => Random.value); } }
    //
    // Summary:
    //     ///
    //     Returns a random number between int.MinValue [inclusive] and int.MaxValue [exclusive] (Read Only).
    //     ///
    public int valueInt { get { return Range(int.MinValue, int.MaxValue); } }

    //
    // Summary:
    //     ///
    //     Generates a random color from HSV and alpha ranges.
    //     ///
    //
    // Parameters:
    //   hueMin:
    //     Minimum hue [0..1].
    //
    //   hueMax:
    //     Maximum hue [0..1].
    //
    //   saturationMin:
    //     Minimum saturation [0..1].
    //
    //   saturationMax:
    //     Maximum saturation[0..1].
    //
    //   valueMin:
    //     Minimum value [0..1].
    //
    //   valueMax:
    //     Maximum value [0..1].
    //
    //   alphaMin:
    //     Minimum alpha [0..1].
    //
    //   alphaMax:
    //     Maximum alpha [0..1].
    //
    // Returns:
    //     ///
    //     A random color with HSV and alpha values in the input ranges.
    //     ///
    public Color ColorHSV() { return Decorate(() => Random.ColorHSV()); }
    //
    // Summary:
    //     ///
    //     Generates a random color from HSV and alpha ranges.
    //     ///
    //
    // Parameters:
    //   hueMin:
    //     Minimum hue [0..1].
    //
    //   hueMax:
    //     Maximum hue [0..1].
    //
    //   saturationMin:
    //     Minimum saturation [0..1].
    //
    //   saturationMax:
    //     Maximum saturation[0..1].
    //
    //   valueMin:
    //     Minimum value [0..1].
    //
    //   valueMax:
    //     Maximum value [0..1].
    //
    //   alphaMin:
    //     Minimum alpha [0..1].
    //
    //   alphaMax:
    //     Maximum alpha [0..1].
    //
    // Returns:
    //     ///
    //     A random color with HSV and alpha values in the input ranges.
    //     ///
    public Color ColorHSV(float hueMin, float hueMax) { return Decorate(() => Random.ColorHSV(hueMin, hueMax)); }
    //
    // Summary:
    //     ///
    //     Generates a random color from HSV and alpha ranges.
    //     ///
    //
    // Parameters:
    //   hueMin:
    //     Minimum hue [0..1].
    //
    //   hueMax:
    //     Maximum hue [0..1].
    //
    //   saturationMin:
    //     Minimum saturation [0..1].
    //
    //   saturationMax:
    //     Maximum saturation[0..1].
    //
    //   valueMin:
    //     Minimum value [0..1].
    //
    //   valueMax:
    //     Maximum value [0..1].
    //
    //   alphaMin:
    //     Minimum alpha [0..1].
    //
    //   alphaMax:
    //     Maximum alpha [0..1].
    //
    // Returns:
    //     ///
    //     A random color with HSV and alpha values in the input ranges.
    //     ///
    public Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax) { return Decorate(() => Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax)); }
    //
    // Summary:
    //     ///
    //     Generates a random color from HSV and alpha ranges.
    //     ///
    //
    // Parameters:
    //   hueMin:
    //     Minimum hue [0..1].
    //
    //   hueMax:
    //     Maximum hue [0..1].
    //
    //   saturationMin:
    //     Minimum saturation [0..1].
    //
    //   saturationMax:
    //     Maximum saturation[0..1].
    //
    //   valueMin:
    //     Minimum value [0..1].
    //
    //   valueMax:
    //     Maximum value [0..1].
    //
    //   alphaMin:
    //     Minimum alpha [0..1].
    //
    //   alphaMax:
    //     Maximum alpha [0..1].
    //
    // Returns:
    //     ///
    //     A random color with HSV and alpha values in the input ranges.
    //     ///
    public Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax) { return Decorate(() => Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax)); }
    //
    // Summary:
    //     ///
    //     Generates a random color from HSV and alpha ranges.
    //     ///
    //
    // Parameters:
    //   hueMin:
    //     Minimum hue [0..1].
    //
    //   hueMax:
    //     Maximum hue [0..1].
    //
    //   saturationMin:
    //     Minimum saturation [0..1].
    //
    //   saturationMax:
    //     Maximum saturation[0..1].
    //
    //   valueMin:
    //     Minimum value [0..1].
    //
    //   valueMax:
    //     Maximum value [0..1].
    //
    //   alphaMin:
    //     Minimum alpha [0..1].
    //
    //   alphaMax:
    //     Maximum alpha [0..1].
    //
    // Returns:
    //     ///
    //     A random color with HSV and alpha values in the input ranges.
    //     ///
    public Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax) { return Decorate(() => Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin, alphaMax)); }
    //
    // Summary:
    //     ///
    //     Initializes the random number generator state with a seed.
    //     ///
    //
    // Parameters:
    //   seed:
    //     Seed used to initialize the random number generator.
    public void InitState(int seed) { init(seed); }
    [System.Obsolete("Use Random.Range instead")]
    public int RandomRange(int min, int max) { return Decorate(() => Random.RandomRange(min, max)); }
    [System.Obsolete("Use Random.Range instead")]
    public float RandomRange(float min, float max) { return Decorate(() => Random.RandomRange(min, max)); }
    //
    // Summary:
    //     ///
    //     Returns a random integer number between min [inclusive] and max [exclusive] (Read
    //     Only).
    //     ///
    //
    // Parameters:
    //   min:
    //
    //   max:
    public int Range(int min, int max) { return Decorate(() => Random.Range(min, max)); }
    //
    // Summary:
    //     ///
    //     Returns a random float number between and min [inclusive] and max [inclusive]
    //     (Read Only).
    //     ///
    //
    // Parameters:
    //   min:
    //
    //   max:
    public float Range(float min, float max) { return Decorate(() => Random.Range(min, max)); }

}
