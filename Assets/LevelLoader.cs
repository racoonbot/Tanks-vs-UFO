using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

public class LevelLoader : MonoBehaviour
{
    public GameData gameData;

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void StartNewGame()
    {
        Debug.Log("Начинаем сброс данных...");

        PlayerPrefs.DeleteKey("Level");
        PlayerPrefs.DeleteKey("LevelMoney"); 
        PlayerPrefs.DeleteKey("MaxCount");

        foreach (UpgradeType type in Enum.GetValues(typeof(UpgradeType)))
        {
            PlayerPrefs.DeleteKey(type.ToString()); 
        }

        PlayerPrefs.DeleteKey("Value_MaxHealth");
        PlayerPrefs.DeleteKey("Value_MaxSpeed");
        PlayerPrefs.DeleteKey("Value_TowerRotation");

        PlayerPrefs.Save();
        
        Debug.Log("Данные стерты. Загрузка уровня...");
        SceneManager.LoadScene(1);
    }
}