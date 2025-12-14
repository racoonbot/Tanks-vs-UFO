using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class EnemyBase : MonoBehaviour
{
    public int Health;
    public Action OnDeathEnemy;
    private SpawnEnemy spawner;

    
    
    
    public virtual string NickName => "Неизвестный";
    public virtual Color MyColor => Color.white;
    
    
    
    
    public int reward;


    private int TakeDamageAmount;
    private TankAttributes attributes;


    public GameObject bullet;
    public Transform bulletSpawn;
    public float bulletSpeed;
    public float shotPeriod;
    public float currentShotPeriod;

    public Radar radar;

    public BlinkEnemy blink;

    public float MaxSpeed;

    public Sounds audioSource;

    public Tank target;
    private Vector3 targetPos;
    private float retreatDistance;
    public bool isAttacking;

    public bool canMove;
    public bool canShoot;

    public float minDistanceToEnemy = 1.0f;
    private List<EnemyBase> allMobs;

    private Rigidbody rb;

    private void Awake()
    {
        spawner = FindObjectOfType<SpawnEnemy>();
        if (spawner == null) Debug.LogError("No RandomSpawner found");

        attributes = FindObjectOfType<TankAttributes>();
        if (attributes == null) Debug.LogError("No TankAttributes found");

        blink = FindObjectOfType<BlinkEnemy>();
        if (blink == null) Debug.LogError("No BlinkEnemy found");

        audioSource = FindObjectOfType<Sounds>();
        if (audioSource == null) Debug.LogError("No Sounds found");

        TakeDamageAmount = attributes.damage;
        OnDeathEnemy += DestroyEnemy;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = FindObjectOfType<Tank>();
        retreatDistance = Random.Range(3f, 10f);

        shotPeriod = currentShotPeriod;
        allMobs = new List<EnemyBase>(FindObjectsOfType<EnemyBase>());
    }

    void Update()
    {
        PreventOverlap();
        if (target != null)
        {
            if (canMove && !radar.isDodging)
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
            return;
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


    private void PreventOverlap() // чтобы мобы отталкивались друг от друга
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
                        transform.position =
                            Vector3.Lerp(transform.position, targetPosition, MaxSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }

    //Отталкивание от забора
    private void OnTriggerStay(Collider other)
    {
        // Проверяем, что это именно стена (по вашему тегу)
        if (other.CompareTag("Wall"))
        {
            // 1. Ищем ближайшую точку на поверхности стены
            Vector3 closestPoint = other.ClosestPoint(transform.position);

            // 2. Вычисляем вектор отталкивания (ОТ стены К врагу)
            Vector3 pushDirection = transform.position - closestPoint;

            // ЗАЩИТА: Если враг уже глубоко внутри стены, closestPoint может заглючить.
            // В этом случае толкаем его просто от центра стены.
            if (pushDirection.sqrMagnitude < 0.001f)
            {
                pushDirection = transform.position - other.transform.position;
            }

            // Убираем высоту (Y), чтобы враг не взлетал в небо
            pushDirection.y = 0;
            pushDirection.Normalize();

            // 3. Применяем отталкивание
            // Используем ту же логику Lerp, что и у вас, но с множителем силы
            float wallRepelForce = 3.0f; // Стена должна отталкивать сильнее, чем другие мобы!

            Vector3 targetPosition = transform.position + pushDirection * wallRepelForce;

            // Плавное, но сильное перемещение
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                MaxSpeed * 2 * Time.deltaTime // Умножаем скорость на 2 для резкости
            );
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

    // public Vector3 GetRandomTargetPosition()
    // {
    //     return new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
    // }

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
        if (ChatController.Instance != null)
        {
            ChatController.Instance.ShowEnemyHitMessage(NickName, MyColor);
        }
        
        blink.StartBlinking();
        Health -= damage;
        if (Health > 0)
        {
            audioSource.source[1].Play();
        }

        if (Health <= 0) OnDeathEnemy?.Invoke();
    }


    public void DestroyEnemy()
    {
        audioSource.source[2].Play();
        GameObject parent = transform.parent.gameObject;
        if (spawner.Enemies.Contains(parent))
        {
            spawner.Enemies.Remove(parent);
        }

        Destroy(parent);
    }

    public virtual void SendMessages()  {}
  

}