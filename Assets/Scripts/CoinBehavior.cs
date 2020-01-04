using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    public int value;
    public GameManager gameManager;

    [Header("Visual effects parameters")]
    public float selfAxisRotationSpeed;
    public float animationSpeed = 1.0f;
    public float animationHeight = 0.01f;

    private void Start()
    {
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x, 
            Random.Range(0.0f, 90.0f), 
            transform.eulerAngles.z
        );
    }

    private void Update()
    {
        VisualEffects();
    }

    private void VisualEffects()
    {
        transform.Rotate(0.0f, selfAxisRotationSpeed, 0.0f, Space.World);
        transform.position += Vector3.up * Mathf.Sin(Time.time * animationSpeed) * animationHeight;
    }

    public void OnPlayerTouch()
    {
        gameManager.score += value;

        transform.position = new Vector3(
            Random.Range(-45.0f, 45.0f), 
            transform.position.y, 
            Random.Range(-45.0f, 45.0f)
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPlayerTouch();
    }
}
