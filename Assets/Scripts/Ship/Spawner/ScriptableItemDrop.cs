using System;
using UnityEngine;

[CreateAssetMenu]
internal class ScriptableItemDrop:ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    public DificultyEnumm chalange;
    

    public void  spawn(Vector3 position, Transform t)
    {
        Instantiate(prefab, t).transform.position = position;

    }
}