using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
    [SerializeField] private List<T> _set;
    public Action<RuntimeSet<T>, T> onAdded;
    public Action<RuntimeSet<T>, T> onRemove;

    public List<T> Set
    {
        get { return new List<T>(_set).Where(a => a != null).ToList(); }
    }

    private void OnValidate()
    {
        var tempHashSet = new HashSet<T>(_set);
//        tempHashSet.RemoveWhere(a => a == null);
        _set = tempHashSet.ToList();
        _set.Remove(default(T));
        PostValidate();
    }

    public virtual void PostValidate()
    {
    }

    private void Awake()
    {
        if (onAdded == null)
        {
            onAdded = delegate(RuntimeSet<T> set, T arg2) { };
        }

        if (onRemove == null)
        {
            onRemove = delegate(RuntimeSet<T> set, T arg2) { };
        }

        if (_set == null)
            _set = new List<T>();
        OnValidate();
        PostAwake();
    }

    public virtual void PostAwake()
    {
    }

    public bool AddItem(T item)
    {
        if (_set.Contains(item) || item == null)
            return false;
        _set.Add(item);
        if (onAdded != null)
        {
            onAdded.Invoke(this, item);
        }

        PostAdd(item);
        return true;
    }

    public virtual void PostAdd(T item)
    {
    }

    public bool RemoveItem(T item)
    {
        if (item == null)
        {
            return false;
        }

        bool toReturn = _set.Contains(item) && _set.Remove(item);
        if (onRemove != null)
        {
            onRemove.Invoke(this, item);
        }

        PostRemove(item);
        return toReturn;
    }

    public virtual void PostRemove(T item)
    {
    }
}

[CreateAssetMenu]
public class RuntimeSet : RuntimeSet<object>
{
}