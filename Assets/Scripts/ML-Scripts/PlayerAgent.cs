using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.Sensor;

public class PlayerAgent : Agent
{
    private Rigidbody rb;

    public float speed = 40.0f;
    public float deathZone = -1.0f;

    private CollectorAcademy myAcademy;
    private CollectorArea myArea;
    private RayPerception3D myRayPer;
    public GameObject area;

    public override void InitializeAgent()
    {
        base.InitializeAgent();
        rb = GetComponent<Rigidbody>();
        myArea = area.GetComponent<CollectorArea>();
        myRayPer = GetComponent<RayPerception3D>();
        myAcademy = FindObjectOfType<CollectorAcademy>();
    }

    public override void CollectObservations()
    {
        const float rayDistance = 35f;
        float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
        float[] rayAngles1 = { 25f, 95f, 165f, 50f, 140f, 75f, 115f };
        float[] rayAngles2 = { 15f, 85f, 155f, 40f, 130f, 65f, 105f };

        string[] detectableObjects = { "coin", "wall" };
        AddVectorObs(myRayPer.Perceive(rayDistance, rayAngles, detectableObjects));
        AddVectorObs(myRayPer.Perceive(rayDistance, rayAngles1, detectableObjects, 0f, 5f));
        AddVectorObs(myRayPer.Perceive(rayDistance, rayAngles2, detectableObjects, 0f, 10f));
        AddVectorObs(transform.InverseTransformDirection(rb.velocity));
    }

    public void MoveAgent(float[] act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = Mathf.FloorToInt(act[0]);
        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
        }
        transform.Rotate(rotateDir, Time.deltaTime * 200f);
        rb.AddForce(dirToGo * 2f, ForceMode.VelocityChange);
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
        if (collision.gameObject.tag == "wall")
        {
            AddReward(-1f);
        }
    }
    #endregion

    #region Trigger handler
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            AddReward(1f);
            myAcademy.totalScore += other.GetComponent<CoinBehavior>().value;
        }

        if (other.gameObject.tag == "death zone")
        {
            Done();
            AddReward(-5f);
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
