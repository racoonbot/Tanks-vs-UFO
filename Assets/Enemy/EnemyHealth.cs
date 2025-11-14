using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int Health;
    public Action OnDeathEnemy;
    private RandomSpawner spawner;

    private void Awake()
    {
        spawner = FindObjectOfType<RandomSpawner>();
        if (spawner == null) Debug.LogError("No RandomSpawner found");
    }

    private void OnEnable()
    {
        OnDeathEnemy += DestroyEnemy;
      
    }

    private void OnDisable()
    {
        OnDeathEnemy -= DestroyEnemy;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullets>())
        {
            TakeDamage();
        }
    }

    private void Update()
    {
        if (Health <= 0)
        {
            OnDeathEnemy?.Invoke();
        }
    }

    public void TakeDamage()
    {
        Health--;
    }

    public void DestroyEnemy()
    {
        spawner.Enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}