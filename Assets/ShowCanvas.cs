using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvas : MonoBehaviour
{
    public GameObject canvas;

    public void ActivateCanvas()
    {
        LevelManager.UnlockCursor(this);
        if (canvas != null)

            canvas.SetActive(true);
    }

    public void DeactivateCanvas()
    {
        LevelManager.LockCursor(this);
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }
}