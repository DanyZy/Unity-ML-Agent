using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour, IPooled
{
    public int value;
    private GameManager gameManager;

    [Header("Visual effects parameters")]
    public float selfAxisRotationSpeed;
    public float animationSpeed = 1.0f;
    public float animationHeight = 0.01f;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void OnObjectSpawn()
    {
        transform.position = new Vector3(
            transform.position.x,
            0.9f,
            transform.position.z
        );

        transform.eulerAngles = new Vector3(
            90.0f,
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
