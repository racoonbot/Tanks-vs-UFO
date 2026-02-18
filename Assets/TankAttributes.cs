using UnityEngine;
using System;
using YG;

public enum StatType
{
    Health,
    Speed,
    Rotation
}

public class TankAttributes : MonoBehaviour
{
    public float speedLimit;
    public float healthLimit;
    public float turretRotationSpeedLimit;


    public Action<StatType> OnMaximumLevelReached;


    public float maxSpeed;
    public float maxHealth;
    private string flagMaxHealth = YG2.GetFlag("PlayerMaxHealth"); /// <summary>
                                                                   /// Flag
                                                                   /// </summary>
    public float turretRotationSpeed;
    public int damage;

    private void Start()
    {
        ApplyFlagsSettings();
        LoadStats();
    }

    public void ApplyUpgrade(UpgradeType type, float amount)
    {
        switch (type)
        {
            case UpgradeType.MaxHealth:
                if (maxHealth < healthLimit)
                {
                    maxHealth += amount;
                    PlayerPrefs.SetFloat("Value_MaxHealth", maxHealth);
                }
                else
                {
                    OnMaximumLevelReached?.Invoke(StatType.Health);
                }


                break;

            case UpgradeType.TowerRotation:
                if (turretRotationSpeed < turretRotationSpeedLimit)
                {
                    turretRotationSpeed += amount;
                    PlayerPrefs.SetFloat("Value_TowerRotation", turretRotationSpeed);
                    
                }
                else
                {
                    OnMaximumLevelReached?.Invoke(StatType.Rotation);
                }

                break;

            case UpgradeType.MaxSpeed:
                if (maxSpeed < speedLimit)
                {
                    maxSpeed += amount;
                    PlayerPrefs.SetFloat("Value_MaxSpeed", maxSpeed);
                   
                }
                else
                {
                    OnMaximumLevelReached?.Invoke(StatType.Speed);
                }

                break;
        }

        PlayerPrefs.Save();
    }

    private void LoadStats()
    {
        if (PlayerPrefs.HasKey("Value_MaxHealth"))
        {
            float savedHealth = PlayerPrefs.GetFloat("Value_MaxHealth");
           
            maxHealth = savedHealth;
        }
        else
        {
            return;
        }

        if (PlayerPrefs.HasKey("Value_MaxSpeed"))
        {
            maxSpeed = PlayerPrefs.GetFloat("Value_MaxSpeed");
        }

        if (PlayerPrefs.HasKey("Value_TowerRotation"))
        {
            turretRotationSpeed = PlayerPrefs.GetFloat("Value_TowerRotation");
        }
    }
    private void ApplyFlagsSettings()
    {
        string valueStr = YG2.GetFlag("PlayerMaxHealth");
    
        if (!string.IsNullOrEmpty(valueStr))
        {
            if (int.TryParse(valueStr, out int result)) 
            {
                maxHealth = result;
            }
        }
        
        
    }
}