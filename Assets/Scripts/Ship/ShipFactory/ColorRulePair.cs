using UnityEngine;

[System.Serializable]
public class ColorRulePair
{
    [SerializeField]
    public Color ColorKey;
    [SerializeField]
    public ScriptableRule rule;
    [SerializeField]
    public bool enabled=true;
}