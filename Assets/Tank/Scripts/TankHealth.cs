using System;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public GameObject player;
    public float health;
    private TankAttributes attributes;
    public AudioSource audioSource;

    private float lastDamageTime;
    public float damageCooldown = 0.5f;

    [Header("Накопительный бонус")] 
    public float timeForBonus = 3; 
    public int bonusLevel = 0;
    public int maxBonusLevel = 5; 
    
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
        CalculateBonusLevel();
    }

    private void CalculateBonusLevel()
    {
        float timeWithoutDamage = Time.time - lastDamageTime;
        if (timeWithoutDamage < timeForBonus)
        {
            bonusLevel = 0;
        }
        else
        {
            bonusLevel = (int)(timeWithoutDamage / timeForBonus);
            if (bonusLevel > maxBonusLevel) 
                bonusLevel = maxBonusLevel;
        }
    }

    public void TakeDamage()
    {
        if (Time.time > lastDamageTime + damageCooldown)
        {
            audioSource.Play();
            health--;
            lastDamageTime = Time.time; 
            
            if (health <= 0)
            {
                Die();
            }
        }
    }
   
    public float GetBonusProgress() // ДЛЯ UI полоски бонуса
    {
        float timeToMaxLevel = timeForBonus * maxBonusLevel;
        float timeSinceDamage = Time.time - lastDamageTime;
        float totalProgress = timeSinceDamage / timeToMaxLevel;
        return Mathf.Clamp01(totalProgress);
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullets>()) TakeDamage();
    }
    
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