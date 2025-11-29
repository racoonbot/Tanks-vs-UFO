using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSpawner : MonoBehaviour
{
    public List<GameObject> prefabs = new List<GameObject>();

    public int Count;
    public LevelManager levelManager;

    public Wallet wallet;

    private int _maxCount;

    public int MaxCount
    {
        get => _maxCount;
        set
        {
            _maxCount = value;
            Count = _maxCount;
        }
    }

    public float MaxSpawnPointX = 28f;
    public float MaxSpawnPointZ = 28f;
    public float MinSpawnPointX = -28f;
    public float MinSpawnPointZ = -28f;

    public List<GameObject> Enemies = new List<GameObject>();

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        IncreaseMaxCount();

    }

    private void Update()
    {
        if (Enemies.Count < Count && levelManager.levelIncreased == false)
        {
            EnemySpawned();
            Count--;
        }
    }

    public void EnemySpawned()
    {
        if (Enemies.Count < MaxCount)
        {
            GameObject EnemyObject = Instantiate(GetEnemyPrefab(), GetRandomSpawnPosition(), Quaternion.identity);
            Enemies.Add(EnemyObject);
            EnemyBase e = EnemyObject.GetComponent<EnemyBase>(); //я не понял эту муть с подспиской)))
            if (e != null && wallet != null)
            {
                int rewardCopy = e.reward;
                e.OnDeathEnemy += () => wallet.AddMoney(rewardCopy);
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(MinSpawnPointX, MaxSpawnPointX);
        float z = Random.Range(MinSpawnPointZ, MaxSpawnPointZ);
        return new Vector3(x, 0, z);
    }

    private GameObject GetEnemyPrefab()
    {
        int simpleEnemyChance = levelManager.level + 1;
        int attackEnemyChance = levelManager.level + 5;
        int moveAttackEnemyChance = levelManager.level + 10;

        int totalChance = simpleEnemyChance + attackEnemyChance + moveAttackEnemyChance;

        int randomValue = Random.Range(0, totalChance);

        if (randomValue < simpleEnemyChance)
        {
            return prefabs[0];
        }
        else if (randomValue < simpleEnemyChance + attackEnemyChance)
        {
            return prefabs[1];
        }
        else
        {
            return prefabs[2];
        }
    }

    public void IncreaseMaxCount()
    {
        MaxCount = levelManager.level + 1;
    }

    
}