using System;
using UnityEngine;

public class FreezeBullet : MonoBehaviour
{
    public static Action OnGlobalFreeze;

    private Rigidbody rb;
    private Vector3 savedVelocity;
    private bool isFrozen = false;
    private float timer;
    private float freezeDuration = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    void OnEnable()
    {
        OnGlobalFreeze += StartFreeze;
    }

    private void OnDisable()
    {
        OnGlobalFreeze -= StartFreeze;
    }

   

    void Update()
    {
        if (isFrozen)
        {
            TimerLogic();
        }
    }

  

    private void StartFreeze()
    {
        if (isFrozen) return; 

        isFrozen = true;
        timer = freezeDuration; 
        savedVelocity = rb.velocity;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true; 
    }

    private void UnFreezeBullet()
    {
        isFrozen = false;
        rb.isKinematic = false;
        rb.velocity = savedVelocity;
    }

    private void TimerLogic()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            UnFreezeBullet();
        }
    }
    
}