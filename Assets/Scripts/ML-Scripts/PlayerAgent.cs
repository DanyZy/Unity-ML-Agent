using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

[RequireComponent(typeof(Rigidbody))]
public class PlayerAgent : Agent
{
    private Rigidbody rb;
    private bool isGrounded = false;

    public float speed = 10.0f;
    public float jumpForce = 5.0f;
    public float deathZone = -1.0f;

    private CollectorAcademy myAcademy;
    private CollectorArea myArea;
    public GameObject area;

    public override void InitializeAgent()
    {
        base.InitializeAgent();
        rb = GetComponent<Rigidbody>();
        myArea = area.GetComponent<CollectorArea>();
        myAcademy = FindObjectOfType<CollectorAcademy>();
    }

    public override void CollectObservations()
    {
        var localVelocity = transform.InverseTransformDirection(rb.velocity);
        AddVectorObs(localVelocity.x);
        AddVectorObs(localVelocity.z);
        AddVectorObs(isGrounded);
    }

    public void MoveAgent(float[] act)
    {
        var movementVector = Vector3.zero;

        var action = Mathf.FloorToInt(act[0]);
        switch (action)
        {
            case 1:
                movementVector = transform.forward * 1f;
                break;
            case 2:
                movementVector = transform.forward * -1f;
                break;
            case 3:
                movementVector = transform.right * 1f;
                break;
            case 4:
                movementVector = transform.right * -1f;
                break;
            case 5:
                Jump();
                break;
        }
        rb.AddForce(movementVector * speed, ForceMode.Acceleration);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public override float[] Heuristic()
    {
        if (Input.GetKey(KeyCode.D))
        {
            return new float[] { 3 };
        }
        if (Input.GetKey(KeyCode.W))
        {
            return new float[] { 1 };
        }
        if (Input.GetKey(KeyCode.A))
        {
            return new float[] { 4 };
        }
        if (Input.GetKey(KeyCode.S))
        {
            return new float[] { 2 };
        }
        if (Input.GetKey(KeyCode.Space))
        {
            return new float[] { 5 };
        }
        return new float[] { 0 };
    }

    public override void AgentAction(float[] vectorAction)
    {
        AddReward(-1f / agentParameters.maxStep);
        MoveAgent(vectorAction);
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

    #region Trigger handler
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            AddReward(other.GetComponent<CoinBehavior>().value);
            myAcademy.totalScore += other.GetComponent<CoinBehavior>().value;
            if (myAcademy.totalScore >= 20)
            {
                Done();
            }
        }

        if (other.gameObject.tag == "death zone")
        {
            AgentReset();
        }
    }
    #endregion

    public override void AgentReset()
    {
        myArea.ResetCollectorArea();

        myArea.FromPoolSpawner("Coins");
        myArea.FromPoolSpawner("RedCoins");
        myArea.FromPoolSpawner("Obstacles");

        rb.velocity = Vector3.zero;
        transform.position = new Vector3(Random.Range(-myArea.spawnRange, myArea.spawnRange),
            2f, Random.Range(-myArea.spawnRange, myArea.spawnRange))
            + area.transform.position;
    }
}
