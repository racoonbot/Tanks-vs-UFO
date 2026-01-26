using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ShowControl : MonoBehaviour
{
    public GameObject controlHelpUI;
    
    IEnumerator Start()
    {
        controlHelpUI.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        ShowHelp();
    }

    public void ShowHelp()
    {
        Time.timeScale = 0;
        controlHelpUI.SetActive(true);
         UnlockCursor();
    }

    public void HideHelp()
    {
        if (YG2.envir.deviceType == "desktop")
        {
            LockCursor();
        }
        Time.timeScale = 1;
        controlHelpUI.SetActive(false);
        
    }

    private void UnlockCursor() // Разблокируем мышь
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockCursor() // Блокируем мышь
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;                  
    }
}