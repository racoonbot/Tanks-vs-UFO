using System;
using UnityEngine;

public class BustBulletDestr : Loot

{
    private Sounds sounds;


    private void Start()
    {
        sounds = FindObjectOfType<Sounds>();
        if(sounds ==  null) Debug.LogError("No Sounds in loot Found");
    }
    
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.GetComponent<TankHealth>())
        {
            BulletDestroyer.OnGlobalDestruction?.Invoke();
            sounds.source[0].Play();
            Destroy(gameObject);
        }
    }
}