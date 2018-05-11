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
    private IEnumerator spriteGen;
    // Use this for initialization

    static Dictionary<Template, Dictionary<int, Sprite>> cache = new Dictionary<Template, Dictionary<int, Sprite>>();

    public void setSeed(int seed)
    {
        this.seed.Value = seed;
    }
    public void setSeed(string seed)
    {
        Debug.Log("setting seed");
        int a = seed.GetHashCode();
        this.seed.Value = a;
        if (R!=null)
            R.InitState(a);
        else
            R = new RandomInitable(a);
        setSprite(R, _template);
    }
    private void OnEnable()
    {
        R = new RandomInitable(seed.Value);
        seed.OnChanged += (a, b) =>
        {
            Debug.Log("running");

            R.InitState(a);
            setSprite(R, _template);
        };
        setSprite(R, _template);

    }

    private void setSprite(RandomInitable R, Template _template)
    {
        Debug.Log("makeing a sprite", this.gameObject);
        var rect = new Rect(Vector2.zero, Vector2.one);
        var pivot = Vector2.one / 2;

        if (spriteGen != null)
            StopCoroutine(spriteGen);
        int seed = R.valueInt;
        if (!cache.ContainsKey(_template))
            cache.Add(_template, new Dictionary<int, Sprite>());
        if (cache[_template].ContainsKey(seed))
            this.GetComponent<SpriteRenderer>().sprite = cache[_template][seed];
        else
        {
            spriteGen = _template.CreateShip(seed, (texture) =>
            {
                texture.filterMode = FilterMode.Point;
                rect.width = texture.width;
                rect.height = texture.height;
                Sprite s = Sprite.Create(texture, rect, pivot);
                this.GetComponent<SpriteRenderer>().sprite = s;
                cache[_template].Add(seed, s);
            });

            StartCoroutine(spriteGen);
        }

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
