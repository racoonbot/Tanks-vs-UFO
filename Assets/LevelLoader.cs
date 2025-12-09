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
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }
}
