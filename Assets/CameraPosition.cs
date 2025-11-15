using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 3, -5);
    public Vector3 rotationOffset = new Vector3(19, 0, 0);

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
            Quaternion rotation = player.rotation * Quaternion.Euler(rotationOffset);
            transform.rotation = rotation;
        }
    }
}