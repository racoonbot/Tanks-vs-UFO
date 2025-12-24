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
        // PlayerPrefs.DeleteAll(); // Стирает всё: уровни, деньги и апгрейды
        // PlayerPrefs.Save();      // Принудительно записывает "пустоту" на диск
    
        // Удаляем только игровые данные
        PlayerPrefs.DeleteKey("Level");
        PlayerPrefs.DeleteKey("Money");
        PlayerPrefs.DeleteKey("MaxCount");

        // Удаляем апгрейды (имена из твоего Enum)
        PlayerPrefs.DeleteKey("Health"); 
        PlayerPrefs.DeleteKey("Speed");
        PlayerPrefs.DeleteKey("TowerRotation");
        
        //Удаляем прокачку
        PlayerPrefs.DeleteKey("Value_MaxHealth");
        PlayerPrefs.DeleteKey("Value_MaxSpeed");
        PlayerPrefs.DeleteKey("Value_TowerRotation");
        
        
        Debug.Log("Данные полностью стерты. Загрузка сцены...");
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
        // gameData.ResetAllProgress(); // Вызываем очистку
        // SceneManager.LoadScene(1);   // Запускаем игру
        //
        // PlayerPrefs.DeleteKey("Value_MaxHealth");
        // PlayerPrefs.DeleteKey("Value_MaxSpeed");
        // PlayerPrefs.DeleteKey("Value_TowerRotation");
        
        // // PlayerPrefs.DeleteKey(gameData.KEY_LEVEL);
        //
        // PlayerPrefs.DeleteKey("Health");
        // PlayerPrefs.DeleteKey("Speed");
        // PlayerPrefs.DeleteKey("Rotation");
        //
        // PlayerPrefs.DeleteKey("Money");

        
    }
}