using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour

{
    public Camera cam;

    void OnDestroy()
    {
        if (cam != null)
        {
            cam.transform.parent = null; 
        }
    }
}