using System;
using UnityEngine;

internal class PauseHandeler:MonoBehaviour
{
    public bool isPaused { get; private set; }
    [SerializeField]
    GameObject pauseMenue;
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenue.SetActive(isPaused);
    }
}