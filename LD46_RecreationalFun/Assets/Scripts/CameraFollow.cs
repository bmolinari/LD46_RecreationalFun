using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.075f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 offsetPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = offsetPosition;
    }
}
