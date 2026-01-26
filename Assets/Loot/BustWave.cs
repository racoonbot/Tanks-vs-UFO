using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BustWave : Loot
{
    public GameObject bulletPrefab;
    private GameObject tank;
    public Transform bulletSpawn;

    private void Start()
    {
        tank = FindObjectOfType<Tank>().gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Tank>())
        {
            WaveShot();
            Destroy(gameObject);
        }
    }

    private void WaveShot()
    {
        int bulletsCount = 16; 
        float angleStep = 360f / bulletsCount;
        float explosionBulletSpeed = 22f; 

        for (int i = 0; i < bulletsCount; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody rbBullet = newBullet.GetComponent<Rigidbody>();
            if (rbBullet != null)
            {
                rbBullet.isKinematic = false;
                rbBullet.velocity = direction * explosionBulletSpeed;
            }
        }
    }

}