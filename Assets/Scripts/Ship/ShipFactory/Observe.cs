using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Observe<T>
{
    [SerializeField]
    private T value;
    [SerializeField]
    private bool runEvents;
    [SerializeField]
    UnityEvent _onChanged;
    private T old_value;
    public Action<T, T> OnChanged = (newValue, oldValue) => { };
    
    public Observe()
    {
        OnChanged += (newValue, oldValue) => { _onChanged.Invoke(); };
        
    }

    protected virtual bool IsEquel(T a,T b) { return a.Equals(b); }

    public T Value
    {
        get { return value; }
        set
        {
            if (!IsEquel(this.value,old_value))
            {
                old_value = CloneValue(this.value); 
                this.value = value;
                if (runEvents)
                    OnChanged.Invoke(value, old_value);
            }
        }
    }

    protected virtual T CloneValue(T new_value)
    {
        return new_value;
    }

    public void OnValidate()
    {
        Value = value;
    }


}