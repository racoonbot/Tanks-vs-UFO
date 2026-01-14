using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private SpawnEnemy spawner;
    private LootSpawner lootSpawner;
    public int level = 1;
    public bool levelIncreased; 

    private ShowMoney showMoney;
    public ShowNumLevel showLevel;
    private GameData gameData;
    private ShowCanvas canvas;

    public event Action OnLevelIncreased; 
    public event Action OnLevelStarted;  

    private void Start()
    {
        showMoney = FindObjectOfType<ShowMoney>();
        spawner = FindObjectOfType<SpawnEnemy>();
        canvas = FindObjectOfType<ShowCanvas>();
        gameData = FindObjectOfType<GameData>();
        lootSpawner =  FindObjectOfType<LootSpawner>();

        if (canvas != null && showMoney != null && gameData != null && lootSpawner != null)
        {
            OnLevelIncreased += canvas.ActivateCanvas;
            OnLevelIncreased += showMoney.UpdateText;
            OnLevelStarted += showLevel.UpdateText;
            OnLevelStarted += gameData.SaveData;
            OnLevelStarted += lootSpawner.LootSpawn;
        }
    }

    private void Update()
    {
        if (spawner.Enemies.Count == 0 && !levelIncreased)
        {
            WinLevel();
        }
        else if (spawner.Enemies.Count > 0)
        {
            levelIncreased = false;
        }
    }

    private void WinLevel()
    {
        levelIncreased = true; 
        level++;              
        OnLevelIncreased?.Invoke(); 
    }

    private void OnDisable()
    {
        if (canvas != null && showMoney != null)
        {
            OnLevelIncreased -= canvas.ActivateCanvas;
            OnLevelIncreased -= showMoney.UpdateText;
            OnLevelStarted -= showLevel.UpdateText;
            OnLevelStarted -= gameData.SaveData;
            OnLevelStarted -= lootSpawner.LootSpawn;
        }
    }
    public void NextLevel()
    {
        OnLevelStarted?.Invoke(); 
        canvas.DeactivateCanvas();
        spawner.StartWave(); 
        LockCursor();
    }

    public void ResetLevel()
    {
        LockCursor();
        gameData.LoadData();
        canvas.DeactivateCanvas();
        spawner.StartWave();
    }
    private void LockCursor() 
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false; 
    }
}