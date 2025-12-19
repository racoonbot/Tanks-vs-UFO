using UnityEngine;

public class TankAttributes : MonoBehaviour
{
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
                maxHealth += amount;
                PlayerPrefs.SetFloat("Value_MaxHealth", maxHealth);
                Debug.Log($"💾 СОХРАНЯЮ Здоровье: {maxHealth}");
                break;

            case UpgradeType.TowerRotation:
                turretRotationSpeed += amount;
                PlayerPrefs.SetFloat("Value_TowerRotation", turretRotationSpeed);
                Debug.Log($"💾 СОХРАНЯЮ Поворот: {turretRotationSpeed}");
                break;

            case UpgradeType.MaxSpeed:
                maxSpeed += amount;
                PlayerPrefs.SetFloat("Value_MaxSpeed", maxSpeed);
                Debug.Log($"💾 СОХРАНЯЮ Скорость: {maxSpeed}");
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