[UnityEngine.CreateAssetMenu]
public class ScriptableDamageType:UnityEngine.ScriptableObject
{
    public float Magnitude { get { return DificultyManager.Instance.getCurrentDificultyModifyer*magnitude; } }
    [UnityEngine.SerializeField]
    float magnitude;
}