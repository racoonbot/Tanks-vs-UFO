using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCounter : MonoBehaviour
{
    public float ShotInterval = 2f;
    public bool isShooted = false;

    void Start()
    {

    }

    void Update()
    {
        ShotInterval -= Time.deltaTime;
        if (ShotInterval <= 0)
        {
            isShooted = true;
            ShotInterval = 2f; 
        }
    }
}