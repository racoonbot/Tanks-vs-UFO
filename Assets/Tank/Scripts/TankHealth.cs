using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public GameObject player;
    public float health;
    private TankAttributes attributes;
    public AudioSource audioSource;
    
    private float lastDamageTime;
    public float damageCooldown = 0.4f;
    
    
    
    public Action OnDeathPlayer;

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
        if (Time.time > lastDamageTime + damageCooldown) {
            audioSource.Play();
            health --;
            lastDamageTime = Time.time;
        }
    }

    public void Heal(int healAmount)
    {
        health +=  healAmount;
    }

    public void Die()
    {
        OnDeathPlayer?.Invoke();
        MusicPlayer.instance.StopAllMusic();
        Destroy(player.gameObject);
    }
}