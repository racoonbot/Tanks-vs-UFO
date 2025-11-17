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
        Debug.Log("OnTriggerEnter");
        if (other.GetComponent<TankHealth>())
        {
            ActivateBust();
            gameObject.SetActive(false);
            Destroy(gameObject, 10f);
        }
    }
}