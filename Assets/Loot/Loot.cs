using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Loot : MonoBehaviour
{
    public float boostDuration;
    public bool isBoosted = false;

    protected virtual void ActivateBust()
    {
        Debug.Log("Activate Bust");
        isBoosted = true;
        StartCoroutine(StartBust());
    }

    public IEnumerator StartBust()
    {
        Debug.Log("IEnumerator");
        isBoosted = false;
        yield return new WaitForSeconds(boostDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.GetComponent<TankHealth>())
        {
            ActivateBust();
            Destroy(gameObject);
        }
    }
}