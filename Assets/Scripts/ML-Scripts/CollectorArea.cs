using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class CollectorArea : Area
{
    public float spawnRange = 45.0f;

    private void Start()
    {
        FromPoolSpawner("Coins");
        FromPoolSpawner("RedCoins");
        FromPoolSpawner("Obstacles");
    }

    public void FromPoolSpawner(string poolTag)
    {
        Queue<Vector3> positions = new Queue<Vector3>();
        Queue<Quaternion> rotations = new Queue<Quaternion>();

        for (int i = 0; i < 100; i++)
        {
            positions.Enqueue(new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)));
            rotations.Enqueue(Quaternion.identity);
        }

        Pooler.Instance.SpawnEntirePool(poolTag, positions, rotations);
    }

    public void ResetCollectorArea()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("container"))
            {
                foreach(Transform childOfChild in child.transform)
                {
                    childOfChild.gameObject.SetActive(false);
                }
            }
        }
    }

    public override void ResetArea()
    {
    }
}
