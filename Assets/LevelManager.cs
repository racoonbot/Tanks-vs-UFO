using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private SpawnEnemy spawner;

    public int level = 1;
    public bool levelIncreased;

    private ShowMoney showMoney;
    public ShowNumLevel showLevel;

    private GameData gameData; // Для сохранения данных при старте уровня! Возможно уже не нужно )))). Переделывал
    private ShowCanvas canvas;


    public event Action OnLevelIncreased;
    public event Action OnLevelStarted;


    private void Start()
    {
        showMoney = FindObjectOfType<ShowMoney>();
        spawner = FindObjectOfType<SpawnEnemy>();
        canvas = FindObjectOfType<ShowCanvas>();
        gameData = FindObjectOfType<GameData>();

        if (canvas != null && showMoney != null && gameData != null)
        {
            OnLevelIncreased += canvas.ActivateCanvas;
            OnLevelIncreased += showMoney.UpdateText;
            OnLevelStarted += showLevel.UpdateText;
            OnLevelStarted += gameData.SaveData;
        }
        else
        {
            Debug.Log("canvas == null || showMoney == null");
        }

    }

    private void Update()
    {
        if (spawner.Enemies.Count == 0 && !levelIncreased)
        {
            level++;
            levelIncreased = true;
            OnLevelIncreased?.Invoke();

        }
        else if (spawner.Enemies.Count > 0)
        {
            levelIncreased = false;
        }
    }


    private void OnDisable()
    {
        if (canvas != null && showMoney != null)
        {
            OnLevelIncreased -= canvas.ActivateCanvas;
            OnLevelIncreased -= showMoney.UpdateText;
            OnLevelStarted -= showLevel.UpdateText;
            OnLevelStarted -= gameData.SaveData;
        }
    }

  

    public void NextLevel()
    {
        OnLevelStarted?.Invoke();
        canvas.DeactivateCanvas();
        spawner.IncreaseMaxCount(); 
    }

    public void ResetLevel()
    {
        gameData.LoadData();
        canvas.DeactivateCanvas();
        spawner.IncreaseMaxCount();
        spawner.EnemySpawned();
    }
}