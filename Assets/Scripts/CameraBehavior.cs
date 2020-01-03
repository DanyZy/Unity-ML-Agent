using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 10.0f;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position, 
            target.position + offset, 
            followSpeed * Time.deltaTime
        );
    }
}
