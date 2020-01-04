using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleBehavior : MonoBehaviour, IPooled
{
    public float pushForce = 10.0f;

    public void OnObjectSpawn()
    {
        Text name = GetComponentInChildren<Text>();

        transform.position = new Vector3(
            transform.position.x,
            0.1f,
            transform.position.z
        );

        if (Random.value < 0.5f)
        {
            gameObject.tag = "jump obstacle";
            name.text = "U";
        }
        else
        {
            gameObject.tag = "forward obstacle";
            name.text = "F";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Rigidbody playerRB = other.gameObject.GetComponentInParent<Rigidbody>();

            if (gameObject.tag == "jump obstacle")
            {
                playerRB.AddForce(Vector3.up * pushForce, ForceMode.Impulse);
            }

            if (gameObject.tag == "forward obstacle")
            {
                playerRB.AddForce(playerRB.velocity.normalized * pushForce, ForceMode.Impulse);
            }
        }
    }
}
