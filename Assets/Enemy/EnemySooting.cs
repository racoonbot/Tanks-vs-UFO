using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawn;
    public float bulletSpeed;

    private Transform target;
    public float shotPeriod;
    private float currentShotPeriod = 3f;

    private void Start()
    {
        target = FindObjectOfType<Tank>()?.transform;
        if (target == null) Debug.LogError("No target found");
        shotPeriod = currentShotPeriod;
    }

    private void Update()
    {
        ShotTimer();
    }

    private void Shooting()
    {
        Vector3 direction = (target.position - bulletSpawn.position).normalized;
        GameObject newBullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
    }

    private void ShotTimer()
    {
        shotPeriod -= Time.deltaTime;
        if (shotPeriod <= 0f)
        {
            Shooting();
            shotPeriod = currentShotPeriod;
        }
    }
}