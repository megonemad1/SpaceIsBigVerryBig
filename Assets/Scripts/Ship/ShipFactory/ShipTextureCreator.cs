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


    public void setSeed(int seed)
    {
        this.seed.Value = seed;
    }
    public void setSeed(string seed)
    {
        setSeed(seed.GetHashCode());
    }
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
        Debug.Log("makeing a sprite",this.gameObject);
        var rect = new Rect(Vector2.zero, Vector2.one);
        var pivot = Vector2.one / 2;

        if (spriteGen != null)
            StopCoroutine(spriteGen);
        spriteGen = _template.CreateShip(R.valueInt, (texture) =>
        {
            texture.filterMode = FilterMode.Point;
            rect.width = texture.width;
            rect.height = texture.height;
            Sprite s = Sprite.Create(texture, rect, pivot);
            this.GetComponent<SpriteRenderer>().sprite = s;
        });

        StartCoroutine(spriteGen);

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
