using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class setSeedText : MonoBehaviour {
    [SerializeField]
    UnityEvent awake;
    [SerializeField]
    InputField t;
	// Use this for initialization
	void Awake () {
        if (t == null)
            t = GetComponent<InputField>();
        awake.Invoke();
    }
    public void setFromDeck(PlayerScriptableObject playerdata)
    {
        t.text = playerdata.deck_seed;
    }
    public void setFromCabin(PlayerScriptableObject playerdata)
    {
        t.text = playerdata.cabin_seed;
    }
    public void setFromEngine(PlayerScriptableObject playerdata)
    {
        t.text = playerdata.engine_seed;
    }
}
