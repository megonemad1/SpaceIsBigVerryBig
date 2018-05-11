[UnityEngine.CreateAssetMenu]
public class ScriptableDamageType:UnityEngine.ScriptableObject
{
    public float Magnitude { get { return DificultyManager.Instance.currentDificultyModifyer*magnitude; } }
    [UnityEngine.SerializeField]
    float magnitude;
}