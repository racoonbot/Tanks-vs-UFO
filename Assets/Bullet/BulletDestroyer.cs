using System;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    public static Action OnGlobalDestruction;

    private void OnEnable()
    {
        OnGlobalDestruction += DestroySelf;
    }

    private void OnDisable()
    {
        OnGlobalDestruction -= DestroySelf;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BulletDestroyer bullet))
        {
            DestroySelf(); 
        }
    }
}