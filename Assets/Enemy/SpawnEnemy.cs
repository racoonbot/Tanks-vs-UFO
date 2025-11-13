using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int Count;
    public int MaxCount;
    public float MaxSpawnPointX = 28f;
    public float MaxSpawnPointZ = 28f;
    public float MinSpawnPointX = -28f;
    public float MinSpawnPointZ = -28f;

    public List<GameObject> Enemies = new List<GameObject>();

    void Start()
    {
        
    }

    private void Update()
    {
        if (Count < MaxCount)
        {
            EnemySpawned();
        }
    }

    private void EnemySpawned()
    {
        if (Enemies.Count < MaxCount && prefab != null)
        {
            GameObject EnemyObject =  Instantiate(prefab, GetRandomSpawnPosition(),  Quaternion.identity);
            Enemies.Add(EnemyObject);
            Count = Enemies.Count;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(MinSpawnPointX, MaxSpawnPointX);
        float z = Random.Range(MinSpawnPointZ, MaxSpawnPointZ);
        return new Vector3(x, 0, z);
    }
}