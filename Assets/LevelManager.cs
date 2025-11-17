using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private RandomSpawner spawner;
    public int level = 1;
    public bool levelIncreased;

    private void Start()
    {
        spawner = FindObjectOfType<RandomSpawner>();
    }

    private void Update()
    {
        if (spawner.Enemies.Count == 0 && !levelIncreased)
        {
            level++;
            levelIncreased = true; 
            UpdateSpawnerMaxCount(); 
        }
        else if (spawner.Enemies.Count > 0)
        {
            levelIncreased = false; 
        }
    }

    private void UpdateSpawnerMaxCount()
    {
        if (spawner != null)
        {
            spawner.MaxCount = level + 1; 
        }
    }
}