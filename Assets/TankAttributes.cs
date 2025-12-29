using UnityEngine;
using System;

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


    public Action<StatType> OnMaximumLevelReached; //Доделать


    public float maxSpeed;
    public float maxHealth;
    public float turretRotationSpeed;
    public int damage;

    private void Start()
    {
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
                    Debug.Log($"💾 СОХРАНЯЮ Здоровье: {maxHealth}");
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
                    Debug.Log($"💾 СОХРАНЯЮ Поворот: {turretRotationSpeed}");
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
                    Debug.Log($"💾 СОХРАНЯЮ Скорость: {maxSpeed}");
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
            Debug.Log($"📂 НАШЕЛ сохранение Здоровья: {savedHealth}. Было: {maxHealth}");
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
}