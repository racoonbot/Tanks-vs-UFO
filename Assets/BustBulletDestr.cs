using System;
using UnityEngine;

public class BustBulletDestr : Loot
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.GetComponent<TankHealth>())
        {
            BulletDestroyer.OnGlobalDestruction?.Invoke();
            Destroy(gameObject);
        }
    }
}