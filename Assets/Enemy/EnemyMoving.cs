using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    public float speed;
    public GameObject target;
    private Vector3 targetPos;
    private bool isAttacking;

    void Start()
    {
        target = FindObjectOfType<Tank>().gameObject;
    }

    void Update()
    {
        UpdateDistance();
        if (isAttacking)
        {
            UpdateDirection();
        }
    }

    public void UpdateDirection()
    {
        targetPos = target.transform.position;  // Получаем позицию цели
        float distance = Vector3.Distance(transform.position, targetPos);  // Расчитываем расстояние

        if (isAttacking)
        {
            // Двигаемся к цели
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            // Двигаемся в обратном направлении от цели
            Vector3 retreatDirection = (transform.position - targetPos).normalized;  // Направление от цели
            transform.position += retreatDirection * speed * Time.deltaTime;  // Двигаемся назад
        }
       
    }
    public void UpdateDistance()
    {
        targetPos = target.transform.position;
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance < 10f) 
        {
            isAttacking = false; 
        }
        else
        {
            isAttacking = true; 
        }
    }
}