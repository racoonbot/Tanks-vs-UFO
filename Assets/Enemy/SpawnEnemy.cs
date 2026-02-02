using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>(); // 0: Зеленый, 1: Желтый, 2: Красный
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
        if(levelManager == null) levelManager = FindObjectOfType<LevelManager>();
        
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

        int currentLevel = levelManager.level;
        int spawnedCount = 0;

        // --- 1. Гарантированный спавн (Hardcoded logic) ---
        // Начиная с 6-го уровня: хотя бы 1 Красный (prefabs[2])
        if (currentLevel >= 6 && spawnedCount < MaxCount)
        {
            CreateEnemy(prefabs[2]);
            spawnedCount++;
        }

        // Начиная с 4-го уровня: хотя бы 1 Желтый (prefabs[1])
        // (Логика "и один желтый, и один красный" для 6 уровня тут тоже работает, 
        // так как if (currentLevel >= 4) сработает и для 6 уровня тоже)
        if (currentLevel >= 4 && spawnedCount < MaxCount)
        {
            CreateEnemy(prefabs[1]);
            spawnedCount++;
        }

        // --- 2. Заполнение остатка по весам (Random logic) ---

        // Спавним остальных, пока не достигнем MaxCount
        while (spawnedCount < MaxCount)
        {
            GameObject randomPrefab = GetWeightedEnemyPrefab();
            CreateEnemy(randomPrefab);
            spawnedCount++;
        }
    }

    // Изменили метод: теперь он принимает префаб как аргумент
    private void CreateEnemy(GameObject prefabToSpawn)
    {
        GameObject newEnemy = Instantiate(prefabToSpawn, GetRandomSpawnPosition(), Quaternion.identity);
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

    // Переименовали для ясности, логика осталась прежней
    private GameObject GetWeightedEnemyPrefab()
    {
        int level = levelManager.level;

        // 1. Зеленый
        int simpleChance = Mathf.Max(100 - (level * 4), 20); 

        // 2. Желтый
        int attackChance = 0;
        if (level >= 4)
        {
            attackChance = Mathf.Min((level - 3) * 3, 40);
        }

        // 3. Красный
        int moveAttackChance = 0;
        if (level >= 8)
        {
            moveAttackChance = Mathf.Min((level - 7) * 4, 40);
        }

        int totalChance = simpleChance + attackChance + moveAttackChance;
        int randomValue = UnityEngine.Random.Range(0, totalChance);

        if (randomValue < simpleChance) 
            return prefabs[0]; // Зеленый

        if (randomValue < simpleChance + attackChance) 
            return prefabs[1]; // Желтый
    
        return prefabs[2]; // Красный
    }
}