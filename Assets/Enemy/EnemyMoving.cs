using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    public float speed;
    public GameObject target;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Tank>().gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
    }

    public void UpdateDirection()
    {
        targetPos = target.transform.position;
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

    }
}
