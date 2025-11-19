using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootSpawner : MonoBehaviour
{
    private float MaxSpawnPointX = 28f;
    private float MaxSpawnPointZ = 28f;
    private float MinSpawnPointX = -28f;
    private float MinSpawnPointZ = -28f;

    public int LootAmount = 10;

    public List<GameObject> loot = new List<GameObject>();

    private void Start()
    {
        LootSpawn();
    }

    public void LootSpawn()
    {
        for (int i = 0; i < LootAmount; i++)
        {
            GameObject RandomLootItem = loot[Random.Range(0, loot.Count)];
            Vector3 spawnPosition = RandomSpawnPoint();
            Instantiate(RandomLootItem, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 RandomSpawnPoint()
    {
        return new Vector3(Random.Range(MinSpawnPointX, MaxSpawnPointX), 0f,
            Random.Range(MinSpawnPointZ, MaxSpawnPointZ));
    }
}