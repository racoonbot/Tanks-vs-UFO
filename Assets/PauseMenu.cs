using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject inGameUi;
    public GameObject ControlHelpUi;
    public GameObject EndLevelUi;
    public GameObject GameoverUi;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        LevelManager.LockCursor(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !ControlHelpUi.activeSelf && !EndLevelUi.activeSelf && !GameoverUi.activeSelf)
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
        Debug.Log(" Resume");
        Time.timeScale = 1;
        inGameUi.SetActive(true);
        pauseMenuUI.SetActive(false);
        LevelManager.LockCursor(this);
    }

    public void Pause()
    {
        if (pauseMenuUI != null)
        {
            Debug.Log(" Pause");
            pauseMenuUI.SetActive(true);
        }
        Time.timeScale = 0;
        inGameUi.SetActive(false);
        LevelManager.UnlockCursor(this);
    }


}