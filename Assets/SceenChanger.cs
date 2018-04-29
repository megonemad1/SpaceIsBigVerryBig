using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceenChanger : ScriptableObject
{
    public void ChangeScene(Object Scene, bool additive)
    {
        SceneManager.LoadScene(Scene.name, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
    }
    public void ChangeScene(Object Scene)
    {
        ChangeScene(Scene, false);
    }
}
