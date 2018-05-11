using UnityEngine;

[UnityEngine.CreateAssetMenu]
public class ScriptableDamageType : UnityEngine.ScriptableObject
{
    public float Magnitude
    {
        get
        {
            if (dificultyDirection != 0 && DificultyManager.Instance.currentDificultyModifyer !=0)
                if (dificultyDirection > 0)
                    return DificultyManager.Instance.currentDificultyModifyer * magnitude;
                else
                    return (1/DificultyManager.Instance.currentDificultyModifyer) * magnitude;
            else
                return magnitude;
        }
    }
    [UnityEngine.SerializeField]
    float magnitude;
    [UnityEngine.SerializeField, Range(-1, 1)]
    int dificultyDirection;
}