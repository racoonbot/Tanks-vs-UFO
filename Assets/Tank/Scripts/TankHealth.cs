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
    public float damageCooldown = 0.5f;


    [Header("Бонус за отсутствие урона")] public float timeForBonus = 5f;
    public bool isBonusActive = false;
    // ------------------------

    public Action OnDeathPlayer;

    private void Start()
    {
        attributes = FindObjectOfType<TankAttributes>();
        if (attributes != null)
            health = attributes.maxHealth;

        lastDamageTime = Time.time;
    }

    private void Update()
    {
        CheckNoDamageBonus();
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
        if (Time.time > lastDamageTime + damageCooldown)
        {
            audioSource.Play();
            health--;
            lastDamageTime = Time.time;
            isBonusActive = false;

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void CheckNoDamageBonus()
    {
        if (Time.time - lastDamageTime > timeForBonus)
        {
            if (!isBonusActive)
            {
                isBonusActive = true;
                Debug.Log("БОНУС АКТИВИРОВАН! (Скорость стрельбы увеличена)");
            }
        }
        else
        {
            if (isBonusActive)
            {
                isBonusActive = false;
              
            }
        }
    }
    // -------------------

    public void Heal(int healAmount)
    {
        health += healAmount;
    }

    public void Die()
    {
        OnDeathPlayer?.Invoke();
        if (MusicPlayer.instance != null) MusicPlayer.instance.StopAllMusic();

        if (player != null) Destroy(player.gameObject);
        else Destroy(gameObject);
    }
}