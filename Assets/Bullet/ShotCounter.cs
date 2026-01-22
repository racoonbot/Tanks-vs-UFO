using UnityEngine;

public class ShotCounter : MonoBehaviour
{
    public float ShotInterval = 2f; 
    public bool isShooted = false;
    
    [Header("Настройки усиления")]
    public float ShotMultiplier = 2f;
    
    private float startInterval; 
    public TankHealth tankHealth; 

    void Start()
    {
        startInterval = ShotInterval;
        if (tankHealth == null) tankHealth = GetComponentInParent<TankHealth>();
    }

    void Update()
    {
        ShotInterval -= Time.deltaTime;
        
        if (ShotInterval <= 0)
        {
            isShooted = true;
            ResetTimer(); 
        }
    }

    private void ResetTimer()
    {
        if (tankHealth != null && tankHealth.bonusLevel > 0)
        {
            float currentMultiplier = Mathf.Pow(ShotMultiplier, tankHealth.bonusLevel);
            ShotInterval = startInterval / currentMultiplier;
            
            Debug.Log($"БОНУС УР.{tankHealth.bonusLevel}! Скорость x{currentMultiplier}. Интервал: {ShotInterval}");
        }
        else
        {
            ShotInterval = startInterval;
        }
    }
}