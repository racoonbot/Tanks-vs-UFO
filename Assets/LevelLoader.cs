using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel() // рестарт уровня 
    {
        SceneManager.LoadScene(1);
        if (YG2.envir.deviceType == "desktop")
        {
            LockCursor();
        }
    }

    public void LoadStartScene() //Запускает главное меню (стартовое)
    {
        SceneManager.LoadScene(0);
        UnlockCursor();
    }

    public void StartNewGame() // Запускает гигру с начала
    {

        if (YG2.envir.deviceType == "desktop")
        {
            LockCursor();
        }
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

    private void UnlockCursor() 
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;                 
    }

    private void LockCursor() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;       
    }
}