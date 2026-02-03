using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject inGameUi;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        LevelManager.LockCursor(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume() // Продолжить игру
    {
        Time.timeScale = 1;
        inGameUi.SetActive(true);
        pauseMenuUI.SetActive(false);
        LevelManager.LockCursor(this);
    }

    public void Pause() 
    {
        Time.timeScale = 0;
        inGameUi.SetActive(false);
        pauseMenuUI.SetActive(true);
        LevelManager.UnlockCursor(this);
    }
}