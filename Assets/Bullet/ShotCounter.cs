using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCounter : MonoBehaviour
{
    public float ShotInterval = 2f; 
    public bool isShooted = false;
    public float ShotMultiplier = 2f;
    private float startInterval; 
    public TankHealth tankHealth; 

    void Start()
    {
        startInterval = ShotInterval;
    }

    void Update()
    {
        ShotInterval -= Time.deltaTime;
        
        if (ShotInterval <= 0)
        {
            isShooted = true;
            ResetTimer(); 
        }
    }

    private void ResetTimer()
    {
        if (tankHealth != null && tankHealth.isBonusActive)
        {
            ShotInterval = startInterval / ShotMultiplier; 
            Debug.Log("Интервал стрельбы "+ShotInterval);
        }
        else
        {
            ShotInterval = startInterval;
        }
    }
}