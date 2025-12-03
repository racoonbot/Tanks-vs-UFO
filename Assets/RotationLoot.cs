using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLoot : MonoBehaviour
{
    
    public float rotateSpeed = 5f;
    void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
    }
}
