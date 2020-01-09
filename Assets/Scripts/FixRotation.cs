using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    Vector3 rotation;
    void Awake()
    {
        rotation = transform.eulerAngles;
    }

    void FixedUpdate()
    {
        rotation.y = transform.eulerAngles.y;

        transform.eulerAngles = rotation;
    }
}
