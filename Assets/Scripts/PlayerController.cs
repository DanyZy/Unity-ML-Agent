using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded = false;

    public float speed = 10.0f;
    public float jumpForce = 5.0f;
    public float deathZone = -1.0f;

    private void Start()
    { 
        rb = GetComponent<Rigidbody>();
    }

    public void Move(float verticalInput, float horizontalInput)
    {
        Vector3 movementVector = new Vector3(verticalInput, 0.0f, horizontalInput);
        rb.AddForce(movementVector * speed, ForceMode.Acceleration);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        DeathCheck();
    }

    private void DeathCheck()
    {
        if (transform.position.y < deathZone)
        {
            Debug.Log("game over");
        }
    }

    #region Collision handler
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }
    #endregion
}
