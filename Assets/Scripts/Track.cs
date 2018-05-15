using UnityEngine;
[System.Serializable]
public class Track
{
    [SerializeField]
    public string _name;
    [SerializeField]
    public AudioClip audio;
    [SerializeField]
    public bool unlocked;
    public string name
    {
        get
        {
            if(audio != null && audio.name != null)
            {
                _name = audio.name;
            }
            return _name;
        }
    }

}