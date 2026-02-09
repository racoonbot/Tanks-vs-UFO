using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

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
        lootSpawner = FindObjectOfType<LootSpawner>();


        if (canvas != null && showMoney != null && gameData != null && lootSpawner != null)
        {
            OnLevelIncreased += canvas.ActivateCanvas;
            OnLevelIncreased += showMoney.UpdateText;
            OnLevelIncreased += DestroyAllEnemyBullets;
            OnLevelStarted += showLevel.UpdateText;
            OnLevelStarted += gameData.SaveData;
            OnLevelStarted += lootSpawner.LootSpawn;
        }
    }

    private void Update()
    {
        if (spawner.Enemies.Count == 0 && !levelIncreased)
        {
            Debug.Log("WinLevel");
            WinLevel();
        }
        else if (spawner.Enemies.Count > 0)
        {
            levelIncreased = false;
        }
    }

    private void WinLevel()
    {
        UnlockCursor(this); 
        levelIncreased = true;
        Time.timeScale = 0;
        level++;
        OnLevelIncreased?.Invoke();
    }

    private void OnDisable()
    {
        if (canvas != null && showMoney != null)
        {
            OnLevelIncreased -= DestroyAllEnemyBullets;
            OnLevelIncreased -= canvas.ActivateCanvas;
            OnLevelIncreased -= showMoney.UpdateText;
            OnLevelStarted -= showLevel.UpdateText;
            OnLevelStarted -= gameData.SaveData;
            OnLevelStarted -= lootSpawner.LootSpawn;
        }
    }

    public void DestroyAllEnemyBullets()
    {
        Bullets[] allBullets = FindObjectsOfType<Bullets>();

        foreach (var bullet in allBullets)
        {
            Destroy(bullet.gameObject);
        }
    }


    public void NextLevel() 
    {
        LockCursor(this); // как будто не работает здес
        Time.timeScale = 1f;
        OnLevelStarted?.Invoke();
        canvas.DeactivateCanvas();
        spawner.StartWave();
    }

    public void ResetLevel() //не используется
    {
        LockCursor(this);
        
        gameData.LoadData();
        canvas.DeactivateCanvas();
        spawner.StartWave();
    }

    public static void LockCursor(object sender)
    {
        if (YG2.envir.deviceType == "desktop")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public static void UnlockCursor(object sender) 
    {
        if (YG2.envir.deviceType == "desktop")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}