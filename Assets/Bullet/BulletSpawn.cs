using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public ShotCounter shotCounter;

    void Start()
    {
        shotCounter = FindObjectOfType<ShotCounter>();
        Debug.Log("ShotCounter найден: " + (shotCounter != null));
    }

    void Update()
    {
        if (shotCounter.isShooted)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = bulletSpawnPoint.forward * 20f;
            }
            else
            {
                Debug.LogError("Ошибка: Rigidbody не найден на пуле!");
            }

            shotCounter.isShooted = false;
        }
    }
}