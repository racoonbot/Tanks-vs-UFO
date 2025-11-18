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
        Debug.Log(other.gameObject.name);
        if (other.GetComponent<Tank>())
        {
            WaveShot();
            Destroy(gameObject);
        }
    }


    private void WaveShot()
    {
        float force = 220f; 

        // center forward (forward XZ)
        Rigidbody b0 = Instantiate(bulletPrefab, bulletSpawn).GetComponent<Rigidbody>();
        b0.AddForce(Vector3.Scale(transform.forward, new Vector3(1f, 0f, 1f)).normalized * force);

        // right
        Rigidbody b1 = Instantiate(bulletPrefab).GetComponent<Rigidbody>();
        b1.AddForce(Vector3.Scale(transform.right, new Vector3(1f, 0f, 1f)).normalized * force);

        // left
        Rigidbody b2 = Instantiate(bulletPrefab, bulletSpawn).GetComponent<Rigidbody>();
        b2.AddForce(Vector3.Scale(-transform.right, new Vector3(1f, 0f, 1f)).normalized * force);

        // back
        Rigidbody b3 = Instantiate(bulletPrefab, bulletSpawn).GetComponent<Rigidbody>();
        b3.AddForce(Vector3.Scale(-transform.forward, new Vector3(1f, 0f, 1f)).normalized * force);

        Rigidbody b4 = Instantiate(bulletPrefab, bulletSpawn).GetComponent<Rigidbody>();
        b4.AddForce(new Vector3(1f, 0f, 1f).normalized * force);

        Rigidbody b5 = Instantiate(bulletPrefab, bulletSpawn).GetComponent<Rigidbody>();
        b5.AddForce(new Vector3(-1f, 0f, 1f).normalized * force);

        Rigidbody b6 = Instantiate(bulletPrefab, bulletSpawn).GetComponent<Rigidbody>();
        b6.AddForce(new Vector3(1f, 0f, -1f).normalized * force);

        Rigidbody b7 = Instantiate(bulletPrefab, bulletSpawn).GetComponent<Rigidbody>();
        b7.AddForce(new Vector3(-1f, 0f, -1f).normalized * force);
    }
}