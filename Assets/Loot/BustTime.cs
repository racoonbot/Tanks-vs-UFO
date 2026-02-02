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
        if (spawner == null) yield break;

        // Останавливаем всех врагов временно
        foreach (GameObject enemyObj in spawner.Enemies)
        {
            if (enemyObj == null) continue;
            EnemyBase enemy = enemyObj.GetComponentInChildren<EnemyBase>();
            if (enemy == null) continue;

            /*Debug.Log($"Найдено {spawner.Enemies.Count} врагов");
            Debug.Log($"Враг {enemy.NickName}");
            Debug.Log($"Скорость ДО {enemy.currentSpeed}");*/

            enemy.StopMovementImmediateTemporary();
            
            /*Debug.Log($"Скорость ПОСЛЕ {enemy.currentSpeed}");*/
        }

        // Ждём длительность эффекта
        yield return new WaitForSeconds(boostDuration);

        // Дополнительно: если нужно выполнить какие-то действия после первого ожидания,
        // можно поместить их здесь. Сейчас просто восстановим движение.

        foreach (GameObject enemyObj in spawner.Enemies)
        {
            if (enemyObj == null) continue;
            EnemyBase enemy = enemyObj.GetComponentInChildren<EnemyBase>();
            if (enemy == null) continue;

            enemy.ResumeMovementFromStop();
        }
    }
}