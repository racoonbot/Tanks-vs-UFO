using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>(); 
    
    public LevelManager levelManager;
    public Wallet wallet;


    public int MaxCount; 

    public float MaxSpawnPointX = 28f;
    public float MaxSpawnPointZ = 28f;
    public float MinSpawnPointX = -28f;
    public float MinSpawnPointZ = -28f;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        StartWave();
    }


    public void StartWave()
    {
        CalculateEnemyCount();
        SpawnAllEnemies();
    }

    private void CalculateEnemyCount()
    {
        MaxCount = levelManager.level + 1;
    }

    private void SpawnAllEnemies()
    {

        Enemies.RemoveAll(item => item == null); 

        for (int i = 0; i < MaxCount; i++)
        {
            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        GameObject newEnemy = Instantiate(GetEnemyPrefab(), GetRandomSpawnPosition(), Quaternion.identity);
        Enemies.Add(newEnemy);
        EnemyBase e = newEnemy.GetComponentInChildren<EnemyBase>();
        if (e != null && wallet != null)
        {
            int rewardCopy = e.reward + levelManager.level;
            e.OnDeathEnemy += () => 
            {
                wallet.AddMoney(rewardCopy);
                RemoveEnemyFromList(newEnemy);
            };
        }
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        if(Enemies.Contains(enemy))
        {
            Enemies.Remove(enemy);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(MinSpawnPointX, MaxSpawnPointX);
        float z = Random.Range(MinSpawnPointZ, MaxSpawnPointZ);
        return new Vector3(x, 1f, z);
    }

    private GameObject GetEnemyPrefab()
    {
        int level = levelManager.level;

        // 1. Зеленый (с 1 уровня): Плавно падает со 100 до 20 к 20-му уровню.
        // Коэффициент 4.2 примерно дает 20 на 20-м уровне.
        int simpleChance = Mathf.Max(100 - (level * 4), 20); 

        // 2. Желтый (с 4 уровня): Растет с 0 до 40 к 20-му уровню.
        int attackChance = 0;
        if (level >= 4)
        {
            // Шаг 2.5 дает 40 очков за 16 уровней (с 4 по 20)
            attackChance = Mathf.Min((level - 3) * 3, 40);
        }

        // 3. Красный (с 8 уровня): Растет с 0 до 40 к 20-му уровню.
        int moveAttackChance = 0;
        if (level >= 8)
        {
            // Шаг 3.3 дает 40 очков за 12 уровней (с 8 по 20)
            moveAttackChance = Mathf.Min((level - 7) * 4, 40);
        }

        int totalChance = simpleChance + attackChance + moveAttackChance;
        int randomValue = UnityEngine.Random.Range(0, totalChance);

        if (randomValue < simpleChance) 
            return prefabs[0];

        if (randomValue < simpleChance + attackChance) 
            return prefabs[1];
    
        return prefabs[2];
    }
}