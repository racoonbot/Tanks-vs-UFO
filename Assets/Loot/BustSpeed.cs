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
            float originalSpeed = attributes.maxSpeed;
            float newSpeed = attributes.maxSpeed * speedMultiplayer;
            if (newSpeed > attributes.speedLimit)
            {
                attributes.maxSpeed = attributes.speedLimit;
            }
            else
            {
                attributes.maxSpeed = newSpeed;
            }

            yield return new WaitForSeconds(boostDuration);
            attributes.maxSpeed = originalSpeed;
        }

        isBoosted = false;
    }
}