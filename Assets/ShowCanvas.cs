using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvas : MonoBehaviour
{
    public GameObject canvas;

    public void ActivateCanvas()
    {
        if (canvas != null)

            canvas.SetActive(true);
    }

    public void DeactivateCanvas()
    {
        if (canvas != null)
        {
            Debug.Log("Deactivate canvas");
            canvas.SetActive(false);
        }
    }
}