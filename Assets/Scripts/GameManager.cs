using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;

    private void FromPoolSpawner(string poolTag)
    {
        Queue<Vector3> positions = new Queue<Vector3>();
        Queue<Quaternion> rotations = new Queue<Quaternion>();

        for (int i = 0; i < 100; i++)
        {
            positions.Enqueue(new Vector3(Random.Range(-45.0f, 45.0f), 0, Random.Range(-45.0f, 45.0f)));
            rotations.Enqueue(Quaternion.identity);
        }

        Pooler.Instance.SpawnEntirePool(poolTag, positions, rotations);
    }

    private void Start()
    {
        FromPoolSpawner("Coins");
        FromPoolSpawner("RedCoins");
        FromPoolSpawner("Obstacles");
    }
}
