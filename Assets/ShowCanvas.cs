using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvas : MonoBehaviour
{
    public GameObject canvas;

    private void Start()
    {
        // if (canvas != null)
        //     canvas.SetActive(false);
    }

    public void ActivateCanvas()
    {
        Debug.Log("ShowCanvas");
        if (canvas != null)
        
        canvas.SetActive(true);
    }
}