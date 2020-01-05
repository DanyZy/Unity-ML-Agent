using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        playerController.Move(xInput, zInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.Jump();
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            playerController.gameManager.OnPause();
        }
    }
}
