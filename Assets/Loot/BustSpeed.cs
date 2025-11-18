using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustSpeed : Loot
{
    public float speedMultiplayer;

    public override IEnumerator StartBust(Tank tank)
    {
        tank.force *= speedMultiplayer;
        yield return new WaitForSeconds(boostDuration);
        tank.force /= speedMultiplayer;
        isBoosted = false;
    }
}