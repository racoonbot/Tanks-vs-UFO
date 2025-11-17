using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int Count;

    public LevelManager levelManager;

    private int _maxCount;

    public int MaxCount
    {
        get => _maxCount;
        set
        {
            _maxCount = levelManager.level + 1;
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
        MaxCount = levelManager.level + 1; // Установите MaxCount в начале игры
        Count = MaxCount;
    }

    private void Update()
    {
        if (Enemies.Count < Count && prefab != null)
        {
            EnemySpawned();
            Count--;
        }
    }

    private void EnemySpawned()
    {
        if (Enemies.Count < MaxCount)
        {
            GameObject EnemyObject = Instantiate(prefab, GetRandomSpawnPosition(), Quaternion.identity);
            Enemies.Add(EnemyObject);
            // Count = Enemies.Count;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(MinSpawnPointX, MaxSpawnPointX);
        float z = Random.Range(MinSpawnPointZ, MaxSpawnPointZ);
        return new Vector3(x, 0, z);
    }
}