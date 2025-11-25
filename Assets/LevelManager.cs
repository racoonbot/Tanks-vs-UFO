using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private RandomSpawner spawner;
    public int level = 1;
    private bool levelIncreased;

    public event Action OnLevelIncreased;
    private ShowCanvas canvas;


    private void Start()
    {
        spawner = FindObjectOfType<RandomSpawner>();
        canvas = FindObjectOfType<ShowCanvas>();
        if (canvas != null)
            OnLevelIncreased += canvas.ActivateCanvas;
    }

    private void Update()
    {
        if (spawner.Enemies.Count == 0 && !levelIncreased)
        {
            level++;
            levelIncreased = true;
            OnLevelIncreased?.Invoke();
            UpdateSpawnerMaxCount();
        }
        else if (spawner.Enemies.Count > 0)
        {
            levelIncreased = false;
        }
    }


    private void OnDisable()
    {
        if (canvas != null)
            OnLevelIncreased -= canvas.ActivateCanvas;
    }


    private void UpdateSpawnerMaxCount()
    {
        if (spawner != null)
        {
            spawner.MaxCount = level + 1;
        }
    }
}