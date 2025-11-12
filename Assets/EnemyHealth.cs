using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public int Health;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Bullets>())
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        Health--;
    }
}
