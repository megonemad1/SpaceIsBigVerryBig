using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceenChanger : ScriptableObject
{
    static Dictionary<SceenWrapper, string> cache = new Dictionary<SceenWrapper, string>();
    public void ChangeScene(SceenWrapper Scene, bool additive)
    {
        if (cache.ContainsKey(Scene))
            SceneManager.LoadScene(cache[Scene], additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        else
        {
            cache.Add(Scene, Scene.sceneName);
            SceneManager.LoadScene(Scene.sceneName, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        }
    }
    public void ChangeScene(SceenWrapper Scene)
    {
        ChangeScene(Scene, false);
    }
}
