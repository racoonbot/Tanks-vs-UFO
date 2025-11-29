using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private RandomSpawner spawner;
    public int level = 1;
    public bool levelIncreased;
    private ShowMoney showMoney;


    public event Action OnLevelIncreased;
    private ShowCanvas canvas;


    private void Start()
    {
        showMoney = FindObjectOfType<ShowMoney>();
        spawner = FindObjectOfType<RandomSpawner>();
        canvas = FindObjectOfType<ShowCanvas>();
        if (canvas != null && showMoney != null)
        {
            OnLevelIncreased += canvas.ActivateCanvas;
            OnLevelIncreased += showMoney.UpdateText;
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
        canvas.DeactivateCanvas();
        spawner.IncreaseMaxCount();
        spawner.EnemySpawned();
    }
}