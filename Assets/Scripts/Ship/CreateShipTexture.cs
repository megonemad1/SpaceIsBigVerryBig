using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateShipTexture : MonoBehaviour
{
    int oldseed = int.MaxValue;
    public int seed = 3;
    public int[,] mask = new int[,] {
            {0,0,0,0,0,0},
            {0,0,0,1,1,1},
            {0,0,0,1,1,2},
            {0,0,1,1,1,2},
            {0,0,1,1,2,3},
            {0,1,1,1,2,3},
            {0,0,1,1,1,4},
            {0,0,0,1,2,3},
            {0,1,1,1,2,3},
            {0,1,1,1,1,2},
            {0,0,0,0,0,0}
        };
    // Use this for initialization
    void Start()
    {
        seed = Random.Range(int.MinValue, int.MaxValue);
    }

    enum ship
    {
        empty = 0,
        body = 1,
        skeliton = 2,
        cockpit = 3,
        cockpitbody = 4
    }
    const float BODYCAHNCE = 0.5f;
    const float COCKPITCAHNCE = 0.5f;
    Texture2D CreateShip(int seed, int[,] mask)
    {
        Random.InitState(seed);
        Color ShipColor = Color.red;
        Color CockpitColor = Color.blue;
        float[] ShipColorHSV = new float[3];
        Color.RGBToHSV(ShipColor, out ShipColorHSV[0], out ShipColorHSV[1], out ShipColorHSV[2]);
        float[] CockpitColorHSV = new float[3];
        Color.RGBToHSV(CockpitColor, out CockpitColorHSV[0], out CockpitColorHSV[1], out CockpitColorHSV[2]);
        ShipColorHSV[0] = Random.value;
        CockpitColor[0] = Random.value;
        CockpitColor[1] += Random.value*0.5f - 0.25f;
        ShipColorHSV[1] += Random.value*0.5f - 0.25f;

        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
        var texture = new Texture2D(mask.GetLength(0), mask.GetLength(1) * 2, TextureFormat.ARGB32, false);
        for (int x = 0; x < mask.GetLength(0); x++)
            for (int y = 0; y < mask.GetLength(1); y++)
            {
                switch (mask[x, y])
                {
                    case 1:
                            if (Random.value > BODYCAHNCE)
                        {
                            setMirroredHsvWithNoise(mask, ShipColorHSV[0], ShipColorHSV[1] + Random.value * 0.25f - 0.125f, ShipColorHSV[2] + Random.value * 0.25f - 0.125f, texture, x,  y);
                        }
                        else
                            {
                                setMirroredPixel(mask, new Color(0,0,0,0), texture, x, y);
                            }
                        break; 
                    case 3:
                        setMirroredHsvWithNoise(mask, CockpitColorHSV[0], CockpitColorHSV[1] + Random.value * 0.25f - 0.125f, CockpitColorHSV[2] + Random.value * 0.25f - 0.125f, texture, x,  y);
                        break;
                    case 2:
                        setMirroredHsvWithNoise(mask, ShipColorHSV[0], ShipColorHSV[1] + Random.value * 0.25f - 0.125f, ShipColorHSV[2] + Random.value * 0.25f - 0.125f, texture, x,  y);
                        break;
                    case 0:
                            setMirroredPixel(mask, new Color(0,0,0,0), texture, x, y);
                        break;
                    case 4:
                            if (Random.value > COCKPITCAHNCE)
                        {
                            setMirroredHsvWithNoise(mask, CockpitColorHSV[0], CockpitColorHSV[1] + Random.value * 0.25f - 0.125f, CockpitColorHSV[2] + Random.value * 0.25f - 0.125f, texture, x, y);
                        }
                            else
                        {
                            setMirroredHsvWithNoise(mask, ShipColorHSV[0], ShipColorHSV[1] + Random.value * 0.25f - 0.125f, ShipColorHSV[2] + Random.value * 0.25f - 0.125f, texture, x,  y);
                        }
                        break;
                }
            }

        // Apply all SetPixel calls
        texture.Apply();
        

        // connect texture to material of GameObject this script is attached to
        //renderer.material.mainTexture = texture;
        return texture;
    }

    private static void setMirroredHsvWithNoise(int[,] mask, float h, float s, float v, Texture2D texture, int x, int y)
    {
        setMirroredPixel(mask, Color.HSVToRGB(h,s,v), texture, x, y);
    }

    private static void setMirroredPixel(int[,] mask, Color CockpitColor, Texture2D texture, int x, int y)
    {
        texture.SetPixel(x, y , CockpitColor);
        texture.SetPixel(x  , mask.GetLength(1) * 2 - y - 1, CockpitColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (seed != oldseed)
        {

            oldseed = seed;
            Texture2D t = CreateShip(seed, mask);
            t.filterMode = FilterMode.Point;
            Sprite s = Sprite.Create(
                  t,
                  new Rect(new Vector2(0, 0),
                  new Vector2(t.width, t.height)),
                  new Vector2(0.5f, 0.5f)
                  );
            this.GetComponent<SpriteRenderer>().sprite = s;

        }

    }
}
