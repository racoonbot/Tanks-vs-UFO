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

    public int amountPerLevel = 3;

    public List<GameObject> loot = new List<GameObject>();

    private void Start()
    {
        LootSpawn();
    }

    public void LootSpawn()
    {
         // Оптимальное количество

        for (int i = 0; i < amountPerLevel; i++)
        {
            int roll = Random.Range(0, 100);
            GameObject selectedLoot;

            if (roll < 70) // 70% шанс на здоровье
            {
                selectedLoot = loot[0]; // Предположим, здоровье — первый элемент в списке
            }
            else if (roll < 85) // 15% шанс на заморозку
            {
                selectedLoot = loot[1];
            }
            else // 15% шанс на взрыв
            {
                selectedLoot = loot[2];
            }

            Vector3 spawnPosition = RandomSpawnPoint();
            Instantiate(selectedLoot, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 RandomSpawnPoint()
    {
        return new Vector3(Random.Range(MinSpawnPointX, MaxSpawnPointX), 0.5f,
            Random.Range(MinSpawnPointZ, MaxSpawnPointZ));
    }
}