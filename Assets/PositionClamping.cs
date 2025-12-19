using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionClamping : MonoBehaviour
{
    public float MaxSpawnPointX = 28f;
    public float MaxSpawnPointZ = 28f;
    public float MinSpawnPointX = -28f;
    public float MinSpawnPointZ = -28f;


    private void Update()
    {
        float x = transform.position.x;
        float z = transform.position.z;
        if (x > MaxSpawnPointX || x < MinSpawnPointX ||  z > MaxSpawnPointZ || z < MinSpawnPointZ)
        {
            PositionClamp();
        }
    }

    private void PositionClamp()
    {
        float xClamp =  Mathf.Clamp(transform.position.x, MinSpawnPointX, MaxSpawnPointX);
        float zClamp = Mathf.Clamp(transform.position.z, MinSpawnPointZ, MaxSpawnPointZ);
        transform.position = new Vector3(xClamp, transform.position.y, zClamp);
    }
    
}
