using UnityEngine;

[CreateAssetMenu]
public class DificultyEnumm : ScriptableObject
{
    
    [SerializeField, Range(0,1)]
    float cr;
    public float getCr()
    {
        return cr*DificultyManager.Instance.currentDificultyModifyer;
    }
}