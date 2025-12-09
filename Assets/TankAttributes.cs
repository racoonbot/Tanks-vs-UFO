using UnityEngine;

public class TankAttributes : MonoBehaviour
{
    public float maxSpeed;
    public float maxHealth; // Лучше использовать float, раз уж начали, или приводить типы
    public float turretRotationSpeed; // Добавил, раз мы его улучшаем

    private void Start()
    {
        LoadStats();
    }

    public void ApplyUpgrade(UpgradeType type, float amount)
    {
        switch (type)
        {
            case UpgradeType.MaxHealth:
                maxHealth += amount;
                PlayerPrefs.SetFloat("Value_MaxHealth", maxHealth); 
                break;

            case UpgradeType.TowerRotation:
                turretRotationSpeed += amount;
                PlayerPrefs.SetFloat("Value_TowerRotation", turretRotationSpeed);
                break;

            case UpgradeType.MaxSpeed:
                maxSpeed += amount;
                PlayerPrefs.SetFloat("Value_MaxSpeed", maxSpeed);
                break;
        }
        
        PlayerPrefs.Save();
    }

    private void LoadStats()
    {
        maxHealth = PlayerPrefs.GetFloat("Value_MaxHealth", maxHealth);
        maxSpeed = PlayerPrefs.GetFloat("Value_MaxSpeed", maxSpeed);
        turretRotationSpeed = PlayerPrefs.GetFloat("Value_TowerRotation", turretRotationSpeed);
    }
}