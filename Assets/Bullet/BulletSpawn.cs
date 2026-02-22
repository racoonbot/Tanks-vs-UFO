using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public ShotCounter shotCounter;
    private TankHealth tankHealth;
    public int waveLevel;
    public AudioSource audioSource;
    private int lastWaveShotLevel = 0; 

    void Start()
    {
        shotCounter = FindObjectOfType<ShotCounter>();
        tankHealth = FindObjectOfType<TankHealth>();
    }

    void Update()
    {
        // Обычная стрельба
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

        // Проверка на WaveShot
        if(tankHealth != null)
        {
            CheckAndFireWave();
        }
    }
    
    private void CheckAndFireWave()
    {
        int currentLevel = tankHealth.bonusLevel;
        if (currentLevel == 0)
        {
            lastWaveShotLevel = 0;
            return;
        }
        if (currentLevel > 0 && currentLevel % waveLevel == 0 && currentLevel > lastWaveShotLevel)
        {
            WaveShot();
            
            lastWaveShotLevel = currentLevel;
        }
    }

    private void WaveShot()
    {
        audioSource.Play();
        int bulletsCount = 8;
        float angleStep = 360f / bulletsCount;
        float explosionBulletSpeed = 15f;

        for (int i = 0; i < bulletsCount; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
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