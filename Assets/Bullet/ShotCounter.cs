using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCounter : MonoBehaviour
{
    public float ShotInterval = 2f;
    public bool isShooted = false;

    void Start()
    {
        Debug.Log("ShotCounter запущен. Интервал между выстрелами: " + ShotInterval + " секунд.");
    }

    void Update()
    {
        ShotInterval -= Time.deltaTime;

        if (ShotInterval <= 0)
        {
            isShooted = true;
            Debug.Log("Выстрел выполнен!");
            ShotInterval = 2f; 
        }
    }
}