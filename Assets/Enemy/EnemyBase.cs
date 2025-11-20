using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public abstract class EnemyBase : MonoBehaviour
{
    public int Health;
    public Action OnDeathEnemy;
    private RandomSpawner spawner;


    private int TakeDamageAmount;
    private TankAttributes attributes;


    public GameObject bullet;
    public Transform bulletSpawn;
    public float bulletSpeed;
    public float shotPeriod;
    public float currentShotPeriod;


    public float MaxSpeed;
    
    public Tank target;
    private Vector3 targetPos;
    private float retreatDistance;
    public bool isAttacking;

    public bool canMove;
    public bool canShoot;

    public float minDistanceToEnemy = 1.0f;
    private List<EnemyBase> allMobs;
    
    private void Awake()
    {
        spawner = FindObjectOfType<RandomSpawner>();
        if (spawner == null) Debug.LogError("No RandomSpawner found");
        attributes = FindObjectOfType<TankAttributes>();
        TakeDamageAmount = attributes.damage;
        OnDeathEnemy += DestroyEnemy;
    }

    void Start()
    {
        target = FindObjectOfType<Tank>();
        retreatDistance = Random.Range(2f, 12f);
        shotPeriod = currentShotPeriod;
        allMobs = new List<EnemyBase>(FindObjectsOfType<EnemyBase>());
    }

    void Update()
    {
        PreventOverlap();
        if (target != null)
        {
            if (canMove)
            {
                UpdateDirection();
                UpdateDistance();
            }

            if (canShoot)
            {
                ShotTimer();
            }
        }
        else
        {
            Debug.Log("target == null");
        }

        if (Health <= 0)
        {
            OnDeathEnemy?.Invoke();
        }
    }

    private void OnDisable()
    {
        OnDeathEnemy -= DestroyEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullets>())
        {
            TakeDamage(TakeDamageAmount);
        }
    }

    private void PreventOverlap()
    {
        foreach (var otherMob in allMobs)
        {
            if (otherMob != this) // Игнорируем самого себя
            {
                if (otherMob != null)
                {
                    float distance = Vector3.Distance(transform.position, otherMob.transform.position);

                    // Если объекты слишком близко друг к другу
                    if (distance < minDistanceToEnemy)
                    {
                        // Вычисляем вектор от этого мобы к другому
                        Vector3 direction = (transform.position - otherMob.transform.position).normalized;
                        Vector3 targetPosition = transform.position + direction;
                        // Перемещаем этот моб в сторону от другого
                        transform.position = Vector3.Lerp(transform.position, targetPosition, MaxSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }

    public void Shooting()
    {
        Vector3 direction = (target.transform.position - bulletSpawn.position).normalized;
        GameObject newBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody on bullet prefab");
            return;
        }

        rb.isKinematic = false;
        rb.velocity = direction * bulletSpeed;
    }

    public void ShotTimer()
    {
        shotPeriod -= Time.deltaTime;
        if (shotPeriod <= 0f)
        {
            shotPeriod = currentShotPeriod;
            Shooting();
        }
    }


    public void UpdateDirection()
    {
        targetPos = target.transform.position;
        float distance = Vector3.Distance(transform.position, targetPos);

        if (isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, MaxSpeed * Time.deltaTime);
        }
        else
        {
            if (target.rb.velocity.magnitude > 0.1)
            {
                Vector3 retreatDirection = (transform.position - targetPos).normalized;
                transform.position += retreatDirection * MaxSpeed * Time.deltaTime;
            }
        }
    }

    public void UpdateDistance()
    {
        targetPos = target.transform.position;
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance < retreatDistance)
        {
            isAttacking = false;
        }
        else
        {
            isAttacking = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (Health <= 0) return;
        Health -= damage;
        if (Health <= 0) OnDeathEnemy?.Invoke();
    }

    public void DestroyEnemy()
    {
        spawner.Enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}