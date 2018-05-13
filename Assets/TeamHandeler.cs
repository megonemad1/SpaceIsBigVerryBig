using UnityEngine;

internal class TeamHandeler:MonoBehaviour
{
    [SerializeField]
    public int team;
    public override bool Equals(object other)
    {
        TeamHandeler t = other as TeamHandeler;
        if (t)
        {
           return team.Equals(t.team);
        }
        return false;
    }
}