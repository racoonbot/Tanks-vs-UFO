using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        // Обычно при старте игры курсор должен быть скрыт
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
        pauseMenuUI.SetActive(false);
        LevelManager.LockCursor(this);
    }

    public void Pause() 
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        LevelManager.UnlockCursor(this);
    }
}