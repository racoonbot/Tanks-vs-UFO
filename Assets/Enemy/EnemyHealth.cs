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
    [SerializeField]
    
    private int damage;
    private TankAttributes attributes;
    
    

    private void Awake()
    {
        spawner = FindObjectOfType<RandomSpawner>();
        if (spawner == null) Debug.LogError("No RandomSpawner found");
        attributes = FindObjectOfType<TankAttributes>();
        damage =  attributes.damage;
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
            TakeDamage(damage);
        }
    }

    private void Update()
    {
        if (Health <= 0)
        {
            OnDeathEnemy?.Invoke();
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;;
    }

    public void DestroyEnemy()
    {
        spawner.Enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}