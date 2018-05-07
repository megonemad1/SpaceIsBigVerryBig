using UnityEngine;

[CreateAssetMenu]
public class DificultyEnumm : ScriptableObject
{
    [SerializeField]
    DificultyManager manager;
    private void OnEnable()
    {
        manager.AddItem(this);
    }
    private void OnDisable()
    {
        manager.RemoveItem(this);
    }
    private void OnValidate()
    {
        manager.AddItem(this);
    } 
    [SerializeField, Range(0,1)]
    float cr;
    public float getCr()
    {
        return cr*manager.getCurrentDificultyModifyer;
    }
}