using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseMenuUI.active = true;
                UnlockCursor();
            }
            else
            {
                Time.timeScale = 1;
                pauseMenuUI.active = false;
                LockCursor();
            }
        }
    }
    private void UnlockCursor() // Разблокируем мыша
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;                  
    }
    private void LockCursor() // Блокируем мыша
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;                  
    }
   
}
