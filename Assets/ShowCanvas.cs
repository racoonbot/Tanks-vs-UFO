using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvas : MonoBehaviour
{
    public GameObject canvas;

    public void ActivateCanvas()
    {
        UnlockCursor(); 
        if (canvas != null)

            canvas.SetActive(true);
    }

    public void DeactivateCanvas()
    {
        LockCursor();
        if (canvas != null)
        {
            canvas.SetActive(false);
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