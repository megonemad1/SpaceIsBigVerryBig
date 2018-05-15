using UnityEngine;

internal class DropHandeler : MonoBehaviour
{
    [SerializeField]
    public ScriptableItemDrop _drop;
    static Transform t;
    private void Awake()
    {
        if (t == null)
            t = new GameObject("dropFolder").transform;
    }
    public void drop()
    {
        if (_drop != null)
            _drop.spawn(this.transform.position,t);
    }
}