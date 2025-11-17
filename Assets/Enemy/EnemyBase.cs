using System;
using UnityEngine;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public abstract class EnemyBase : MonoBehaviour
{
    
    public int Health;
    public Action OnDeathEnemy;
    private RandomSpawner spawner;
    
    [SerializeField]
    private int damage;

    private TankAttributes attributes;
    
    
    
    
    public GameObject bullet;
    public Transform bulletSpawn;
    public float bulletSpeed;
    public float shotPeriod;
    public float currentShotPeriod = 3f;


    public float speed;
    public GameObject target;
    private Vector3 targetPos;
    private float retreatDistance;
    private bool isAttacking;

    public bool canMove;
    public bool canShoot;


    private void Awake()
    {
        spawner = FindObjectOfType<RandomSpawner>();
        if (spawner == null) Debug.LogError("No RandomSpawner found");
        attributes = FindObjectOfType<TankAttributes>();
        damage =  attributes.damage;
    }
    
    void Start()
    {
        target = FindObjectOfType<Tank>().gameObject;
        retreatDistance = Random.Range(2f, 12f);
        shotPeriod = currentShotPeriod;
    }

    void Update()
    {
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
        if (Health <= 0)
        {
            OnDeathEnemy?.Invoke();
        }
    }
    
 
    private void OnEnable()
    {
        OnDeathEnemy += DestroyEnemy;
      
    }

    private void OnDisable()
    {
        OnDeathEnemy -= DestroyEnemy;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullets>())
        {
            TakeDamage(damage);
        }
    }
    
    protected void Shooting()
    {
        Vector3 direction = (target.transform.position - bulletSpawn.position).normalized;
        GameObject newBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        if (rb == null) { Debug.LogError("No Rigidbody on bullet prefab"); return; }
        rb.isKinematic = false;
        rb.velocity = direction * bulletSpeed;
    }

    protected void ShotTimer()
    {
        shotPeriod -= Time.deltaTime;
        if (shotPeriod <= 0f)
        {
            Shooting();
            shotPeriod = currentShotPeriod;
        }
    }


    protected void UpdateDirection()
    {
        targetPos = target.transform.position;
        float distance = Vector3.Distance(transform.position, targetPos);

        if (isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            Vector3 retreatDirection = (transform.position - targetPos).normalized;
            transform.position += retreatDirection * speed * Time.deltaTime;
        }
    }

    protected void UpdateDistance()
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
        Health -= damage;;
    }

    public void DestroyEnemy()
    {
        spawner.Enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}