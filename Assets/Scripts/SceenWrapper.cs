using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceenWrapper : ScriptableObject
{
    [SerializeField]
    Object scene;
    [SerializeField]
    public string sceneName;
    // Use this for initialization
    void Awake()
    {
        if (scene != null)
            sceneName = scene.name;
    }
    private void OnEnable()
    {
        if (scene != null)
            sceneName = scene.name;
    }
}
