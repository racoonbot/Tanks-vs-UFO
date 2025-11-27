using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private int health;
    private TankAttributes attributes;

    private void Start()
    {
        attributes = FindObjectOfType<TankAttributes>();
        health = attributes.maxHealth;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullets>())
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {

        health--;
    }

    public void Heal(int healAmount)
    {
        health +=  healAmount;
    }

    public void Die()
    {
        Destroy(player.gameObject);
    }
}