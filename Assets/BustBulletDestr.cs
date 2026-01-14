using System;
using UnityEngine;

public class BustBulletDestr : Loot
{

    private Sounds audioController; 

    private void Start()
    {
        audioController = FindObjectOfType<Sounds>();
        if(audioController == null) Debug.LogError("No Sounds in loot Found");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TankHealth>())
        {
            BulletDestroyer.OnGlobalDestruction?.Invoke();
            if (audioController != null) 
            {
                audioController.source[0].Play();
            }
            Destroy(gameObject);
        }
    }
}