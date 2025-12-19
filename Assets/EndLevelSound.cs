using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelSound : MonoBehaviour
{
    public TankHealth _tankHealth;
    public AudioSource source;
    private bool soundPlayed = false; 

    void Start()
    {
        _tankHealth = FindObjectOfType<TankHealth>().GetComponent<TankHealth>();
        
        if (_tankHealth == null)
        {
            Debug.LogError("_tankHealth not found.");
            return; // Выйти из метода, если _tankHealth не найден
        }

        _tankHealth.OnDeathPlayer += PlaySound; // Подписка на событие
    }

    public void PlaySound()
    {
        if (!soundPlayed) // Проверка, было ли уже воспроизведение
        {
            Debug.Log("Playing sound");
            if (source != null)
            {
                source.Play();
                soundPlayed = true; // Установка флага после воспроизведения
            }
            else
            {
                Debug.LogError("AudioSource not assigned in " + gameObject.name);
            }
        }
    }
}