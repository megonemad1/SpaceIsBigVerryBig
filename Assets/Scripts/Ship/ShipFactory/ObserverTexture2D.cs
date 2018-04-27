using UnityEngine;

public class ObserverTexture2D: Observe<Texture2D>
{
    protected override Texture2D CloneValue(Texture2D new_value)
    {
        Texture2D tmp = new Texture2D(new_value.width, new_value.height);
        Graphics.CopyTexture(new_value, tmp);
        return tmp;
    }
   
}