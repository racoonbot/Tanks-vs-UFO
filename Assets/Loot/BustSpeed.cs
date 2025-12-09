using System.Collections;
using UnityEngine;


public class BustSpeed : Loot
{
    public float speedMultiplayer;

    public override IEnumerator StartBust(Tank tank)
    {
        TankAttributes attributes = FindObjectOfType<TankAttributes>();

        if (attributes != null)
        {
            attributes.maxSpeed *= speedMultiplayer;
            yield return new WaitForSeconds(boostDuration);
            attributes.maxSpeed /= speedMultiplayer;
        }

        isBoosted = false;
    }
}