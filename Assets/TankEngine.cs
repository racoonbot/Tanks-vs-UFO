using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEngine : MonoBehaviour
{
    private Rigidbody tankRb;
    public AudioSource audioSource;

    private void Start()
    {
        tankRb = FindObjectOfType<Tank>().GetComponent<Rigidbody>();
        if (tankRb == null)
        {
            Debug.LogError($"] No Tank found for {gameObject.name}");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError($"AudioSource not assigned in {gameObject.name}");
        }
        
        audioSource.loop = true; 
    }

    private void Update()
    {
        MovingSound(); 
    }

    public void MovingSound()
    {
        if (tankRb != null && tankRb.velocity.magnitude > 0.1f) 
        {
            if (!audioSource.isPlaying) 
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
