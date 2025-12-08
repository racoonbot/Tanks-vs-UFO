using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TankAttributes : MonoBehaviour
{
    public float maxSpeed;
    public float maxHealth;
    public int damage;
    public float turretRotationSpeed;


    public void ApplyUpgrade(UpgradeType type, float amount)
    {
        switch (type)
        {
            case UpgradeType.MaxHealth:
                maxHealth += amount;
                break;
            case UpgradeType.TowerRotation:
                turretRotationSpeed += amount;
                break;
            case UpgradeType.MaxSpeed:
                maxSpeed += amount;
                break;
        }
    }
}