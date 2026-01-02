using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustTime : Loot
{
    public override IEnumerator StartBust(Tank tank)
    {
        if (tank == null) yield break;
        FreezeBullet.OnGlobalFreeze?.Invoke();
        SpawnEnemy spawner = FindObjectOfType<SpawnEnemy>();
        if (spawner != null)
        {
            foreach (GameObject enemyObj in spawner.Enemies)
            {
                EnemyBase enemy = enemyObj.GetComponentInChildren<EnemyBase>();
                if (enemy != null) enemy.MaxSpeed = 0f;
            }
        }
        yield return new WaitForSeconds(boostDuration);
        if (spawner != null)
        {
            foreach (GameObject enemyObj in spawner.Enemies)
            {
                EnemyBase enemy = enemyObj.GetComponentInChildren<EnemyBase>();
                if (enemy != null) enemy.MaxSpeed = 3f;
            }
        }
    }
}