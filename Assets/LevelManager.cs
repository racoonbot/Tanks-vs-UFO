using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private RandomSpawner spawner;

    public int level = 1;
    public bool levelIncreased;

    private ShowMoney showMoney;


    private GameData gameData; // Для сохранения данных при старте уровня!


    public event Action OnLevelIncreased;
    public event Action OnLevelStarted;
    private ShowCanvas canvas;


    private void Start()
    {
        showMoney = FindObjectOfType<ShowMoney>();
        spawner = FindObjectOfType<RandomSpawner>();
        canvas = FindObjectOfType<ShowCanvas>();
        gameData = FindObjectOfType<GameData>();

        if (canvas != null && showMoney != null)
        {
            OnLevelIncreased += canvas.ActivateCanvas;
            OnLevelIncreased += showMoney.UpdateText;
            OnLevelStarted += gameData.SaveData;
        }
        else
        {
            Debug.Log("canvas == null || showMoney == null");
        }

        gameData.SaveData(); // сохраняем данные первого уровня при старте
    }

    private void Update()
    {
        if (spawner.Enemies.Count == 0 && !levelIncreased)
        {
            level++;
            levelIncreased = true;
            OnLevelIncreased?.Invoke();

            // Здесь можно добавить вызов обновления максимального количества врагов
            // spawner.IncreaseMaxCount();
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

            OnLevelStarted -= gameData.SaveData;
        }
    }

    // public void RestartLevel() пока не придумал
    // {
    //     canvas.DeactivateCanvas();
    //     Debug.Log("Restarting level");
    //     level--; 
    //     levelIncreased = false;
    //     // spawner.ResetSpawner(); // Предполагается, что вы добавите этот метод в класс RandomSpawner
    //     UpdateSpawnerMaxCount(); // Обновляем максимальное количество врагов
    // }

    public void NextLevel()
    {
        OnLevelStarted?.Invoke();
        canvas.DeactivateCanvas();
        // level++; // Увеличьте уровень здесь
        spawner.IncreaseMaxCount(); // Здесь обновляется максимальное количество врагов
    }

    public void ResetLevel()
    {
        canvas.DeactivateCanvas();
        spawner.IncreaseMaxCount();
        spawner.EnemySpawned();
    }
}