using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel() // рестарт уровня  не  используется кнопка
    {
        SceneManager.LoadScene(1);
        LevelManager.LockCursor(this);
    }

    public void LoadStartScene() //Запускает главное меню (стартовое)
    {
        SceneManager.LoadScene(0);
        LevelManager.UnlockCursor(this);
    }

    public void StartNewGame() // Запускает гигру с начала
    {
        Time.timeScale = 1;
        /*if (YG2.envir.deviceType == "desktop")  брал пока работет из HideHelp
        {
            LockCursor();
        }*/
        YG2.MetricaSend("StartNewGame");

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


        SceneManager.LoadScene(1);
    }
}