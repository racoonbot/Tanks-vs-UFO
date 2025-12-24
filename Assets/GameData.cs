using UnityEngine;

public class GameData : MonoBehaviour
{
    public Wallet wallet;
    public LevelManager levelManager;
    public SpawnEnemy spawner;

    private const string KEY_MONEY = "Money";
    private const string KEY_LEVEL = "Level";
    private const string KEY_MAXENEMYS = "MaxCount";
    private const int DEFAULT_MONEY = 0;
    private const int DEFAULT_LEVEL = 1;
    private const int DEFAULT_MAXENEMYS = 1;

    private void Start()
    {
        PlayerPrefs.DeleteKey("Value_MaxHealth");
        PlayerPrefs.DeleteKey("Value_MaxSpeed");
        PlayerPrefs.DeleteKey("Value_TowerRotation");
    }

    public void SaveData()
    {
        if (wallet != null)
            PlayerPrefs.SetInt(KEY_MONEY, wallet.LevelMoney);
        if (levelManager != null)
            PlayerPrefs.SetInt(KEY_LEVEL, levelManager.level);
        if (spawner != null)
            PlayerPrefs.SetInt(KEY_MAXENEMYS, spawner.MaxCount);

        PlayerPrefs.Save();
        Debug.Log("Preffs Save)");
    }

    public void LoadData()
    {
        int money = PlayerPrefs.GetInt(KEY_MONEY, DEFAULT_MONEY);
        int lvl = PlayerPrefs.GetInt(KEY_LEVEL, DEFAULT_LEVEL);
        int maxCount = PlayerPrefs.GetInt(KEY_MAXENEMYS, DEFAULT_MAXENEMYS);
        if (wallet != null)
            wallet.LevelMoney = money;
        if (levelManager != null)
            levelManager.level = lvl;
        if (spawner != null)
            spawner.MaxCount = maxCount;
        Debug.Log("Load Data. Level: " + lvl);
    }
    
    public void ResetAllProgress()
    {
        // 1. Сбрасываем основные данные (уровень, деньги, враги)
        PlayerPrefs.DeleteKey(KEY_MONEY);
        PlayerPrefs.DeleteKey(KEY_LEVEL);
        PlayerPrefs.DeleteKey(KEY_MAXENEMYS);

        // 2. Сбрасываем уровни апгрейдов из магазина.
        // ВАЖНО: названия должны СТРОГО совпадать с именами в Enum (item.type)
        PlayerPrefs.DeleteKey("Health"); 
        PlayerPrefs.DeleteKey("Speed");
        PlayerPrefs.DeleteKey("TowerRotation"); 
        // Добавь сюда остальные типы, если они есть

        PlayerPrefs.Save();
        Debug.Log("Все данные стерты!");
    }
    
}