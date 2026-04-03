using UnityEngine;
using System;
using YG; // Если используешь свою заглушку, убедись что она в проекте

public enum StatType
{
    Health,
    Speed,
    Rotation
}

public class TankAttributes : MonoBehaviour
{
    [Header("Лимиты (Максимально возможные)")]
    public float speedLimit = 20f;
    public float healthLimit = 100f;
    public float turretRotationSpeedLimit = 150f;

    [Header("Текущие значения (Начальные)")]
    public float maxSpeed = 5f;
    public float maxHealth = 50f;
    public float turretRotationSpeed = 40f;
    public int damage = 10;

    public Action<StatType> OnMaximumLevelReached;

    private void Start()
    {
        // Порядок важен: сначала грузим то, что сохранили, 
        // потом проверяем флаги (если они есть)
        LoadStats();
       // ApplyFlagsSettings();
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
                else OnMaximumLevelReached?.Invoke(StatType.Health);
                break;

            case UpgradeType.TowerRotation:
                if (turretRotationSpeed < turretRotationSpeedLimit)
                {
                    turretRotationSpeed += amount;
                    PlayerPrefs.SetFloat("Value_TowerRotation", turretRotationSpeed);
                }
                else OnMaximumLevelReached?.Invoke(StatType.Rotation);
                break;

            case UpgradeType.MaxSpeed:
                if (maxSpeed < speedLimit)
                {
                    maxSpeed += amount;
                    PlayerPrefs.SetFloat("Value_MaxSpeed", maxSpeed);
                }
                else OnMaximumLevelReached?.Invoke(StatType.Speed);
                break;
        }
        PlayerPrefs.Save();
    }

    private void LoadStats()
    {
        // Используем GetFloat с параметром по умолчанию. 
        // Если ключа нет в PlayerPrefs, он возьмет текущее значение из Инспектора.
        
        maxHealth = PlayerPrefs.GetFloat("Value_MaxHealth", maxHealth);
        maxSpeed = PlayerPrefs.GetFloat("Value_MaxSpeed", maxSpeed);
        turretRotationSpeed = PlayerPrefs.GetFloat("Value_TowerRotation", turretRotationSpeed);
        
        Debug.Log($"[Stats] Загружено: Скорость поворота = {turretRotationSpeed}");
    }

    /*private void ApplyFlagsSettings()
    {
        // Проверяем флаги только если YG2 инициализирован (в твоем случае - через заглушку)
        try 
        {
            string valueStr = YG2.GetFlag("PlayerMaxHealth");
            if (!string.IsNullOrEmpty(valueStr) && int.TryParse(valueStr, out int result)) 
            {
                maxHealth = result;
            }
        }
        catch { /* Игнорируем ошибки если YG2 недоступен #1# }
    }*/
}