using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu]
public class TrackList : ScriptableObject
{
    [SerializeField]
    List<Track> all = new List<Track>();
    RandomInitable r;
    [SerializeField]
    int seed;
    public System.Action<Track> onTrackSet = (testc) => { };
    private void Awake()
    {
        if (r == null)
            r = new RandomInitable(seed);
        setTrack(0);
    }
    public List<Track> unlocked
    {
        get
        {
            List<Track> u = new List<Track>();
            for (int i = 0; i < all.Count; i++)
                if (all[i].unlocked)
                    u.Add(all[i]);
            return u;
        }
    }

    public int TotalTrackCount { get { return all.Count; } }
    public int UnlockedTrackCount { get { return unlocked.Count; } }

    public int getRandomId()
    {

        if (r == null)
            r = new RandomInitable(seed);
        return r.Range(0, all.Count);
    }

    public bool exsists(int id)
    {
        return !(id >= all.Count || id < 0);
    }

    public bool isUnlocked(int id)
    {
        if (exsists(id))
            return all[id].unlocked;
        return false;
    }

    public bool unlock(int id)
    {
        if (!exsists(id))
            return false;
        if (isUnlocked(id))
            return false;
        all[id].unlocked = true;
        index = unlocked.IndexOf(all[id]);
        onTrackSet.Invoke(GetCurrentTrack());
        return true;
    }

    private int index;

    public Track nextTrack() { return setTrack(index + 1); }
    public Track previousTrack() { return setTrack(index - 1); }

    public Track GetCurrentTrack()
    {
        var _unlocked = unlocked;
        if (index < _unlocked.Count && index >= 0)
            return unlocked[index];
        return null;
    }
    public Track setTrack(int i)
    {
        var _unlocked = unlocked;

        if (_unlocked.Count() > 0)
        {
            index = i;
            while (index < 0)
                index += _unlocked.Count;
            index %= _unlocked.Count;

            onTrackSet.Invoke(GetCurrentTrack());
            return GetCurrentTrack();
        }
        Debug.LogError("no tracks unlocked");
        return null;
    }


}