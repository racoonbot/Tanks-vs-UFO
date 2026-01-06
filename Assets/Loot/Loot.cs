using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Loot : MonoBehaviour
{
    public float boostDuration;
    public bool isBoosted = false;
    private Sounds sounds;


    private void Start()
    {
        sounds = FindObjectOfType<Sounds>();
        if(sounds ==  null) Debug.LogError("No Sounds in loot Found");
    }

    protected virtual void ActivateBust()
    {
        isBoosted = true;
        Tank tank = FindObjectOfType<Tank>();
        if (tank != null)
            tank.StartCoroutine(StartBust(tank));
    }

    public virtual IEnumerator StartBust(Tank tank)
    {
        yield return new WaitForSeconds(boostDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
       
     
        if (other.GetComponent<TankHealth>())
        {
            sounds.source[0].Play();
            ActivateBust();
            gameObject.SetActive(false);
            Destroy(gameObject, 10f);
        }
    }
}