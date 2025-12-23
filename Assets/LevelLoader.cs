using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

public class LevelLoader : MonoBehaviour
{
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
        PlayerPrefs.DeleteKey("Value_MaxHealth");
        PlayerPrefs.DeleteKey("Value_MaxSpeed");
        PlayerPrefs.DeleteKey("Value_TowerRotation");

        SceneManager.LoadScene(1);
    }
}
