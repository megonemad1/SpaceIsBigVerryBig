using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScrollBarUpdateDificultyManager : MonoBehaviour {

    Slider sb;

    private void Awake()
    {
        sb = GetComponent<Slider>();
        sb.value = DificultyManager.Instance.currentDificultyModifyer;
    }
    

    public void run()
    {
        DificultyManager.Instance.setDificulty(sb.value);
    }
}
