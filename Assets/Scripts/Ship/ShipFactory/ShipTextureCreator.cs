using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTextureCreator : MonoBehaviour, ISeedSetter
{
    [SerializeField]
    ObserverInt seed;
    [SerializeField]
    Template _template;
    RandomInitable R;
    // Use this for initialization
   


    void Awake()
    {
        R = new RandomInitable(seed.Value);
        seed.OnChanged += (a, b) =>
        {
            R.InitState(a);
            setSprite(R, _template);
        };
        setSprite(R, _template);
    }

    private void setSprite(RandomInitable R, Template _template)
    {
        Texture2D t = CreateShip(R, _template);
        t.filterMode = FilterMode.Point;
        Sprite s = Sprite.Create(
              t,
              new Rect(new Vector2(0, 0),
              new Vector2(t.width, t.height)),
              new Vector2(0.5f, 0.5f)
              );
        this.GetComponent<SpriteRenderer>().sprite = s;
    }


    Texture2D CreateShip(int seed, Template _template)
    {
        return CreateShip(new RandomInitable(seed), _template);
    }
    Texture2D CreateShip(RandomInitable R, Template _template)
    {
        int width = _template.isMirroredY ? _template.Layout.width * 2 : _template.Layout.width;
        int height = _template.isMirroredX ? _template.Layout.height * 2 : _template.Layout.height;
        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
        var texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        for (int x = 0; x < _template.Layout.width; x++)
            for (int y = 0; y < _template.Layout.height; y++)
            {
                Color c = _template.getPixelAt(x, y, R);
                if (_template.isMirroredY)
                    texture.SetPixel(texture.width - x - 1, y, c);
                if (_template.isMirroredX)
                    texture.SetPixel(x, texture.height - y - 1, c);
                if (_template.isMirroredX && _template.isMirroredY)
                    texture.SetPixel(texture.width - x - 1, texture.height - y - 1, c);
                texture.SetPixel(x, y, c);
            }

        // Apply all SetPixel calls
        texture.Apply();


        // connect texture to material of GameObject this script is attached to
        //renderer.material.mainTexture = texture;
        return texture;
    }

    // Update is called once per frame
    private void OnValidate()
    {
        seed.OnValidate();
    }

    public ObserverInt getSeed()
    {
        return seed;
    }
}
