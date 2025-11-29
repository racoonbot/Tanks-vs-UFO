using UnityEngine;

public class GameData : MonoBehaviour
{
    public Wallet wallet;
    public LevelManager levelManager;
    public RandomSpawner spawner;

    private const string KEY_MONEY = "Money";
    private const string KEY_LEVEL = "Level";
    private const string KEY_MAXENEMYS= "MaxCount";
    private const int DEFAULT_MONEY = 0;
    private const int DEFAULT_LEVEL = 1;
    private const int DEFAULT_MAXENEMYS = 1;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SaveData()
    {
        if (wallet != null)
            PlayerPrefs.SetInt(KEY_MONEY, wallet.LevelMoney);
        if (levelManager != null)
            PlayerPrefs.SetInt(KEY_LEVEL, levelManager.level);
        if (spawner != null)
            PlayerPrefs.SetInt(KEY_MAXENEMYS, spawner.MaxCount );

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
}