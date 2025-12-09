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
        int simpleEnemyChance = levelManager.level + 1;
        int attackEnemyChance = levelManager.level + 5;
        int moveAttackEnemyChance = levelManager.level + 10;
        int totalChance = simpleEnemyChance + attackEnemyChance + moveAttackEnemyChance;
        int randomValue = Random.Range(0, totalChance);

        if (randomValue < simpleEnemyChance) return prefabs[0];
        else if (randomValue < simpleEnemyChance + attackEnemyChance) return prefabs[1];
        else return prefabs[2];
    }
}