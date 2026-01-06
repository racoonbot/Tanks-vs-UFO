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
           

            shotCounter.isShooted = false;
        }
    }
}