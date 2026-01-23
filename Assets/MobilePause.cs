using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    
    
    public void Resume() 
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
    }

    public void Pause() 
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }
}
