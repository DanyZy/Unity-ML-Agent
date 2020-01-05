using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;

    [Header("UI elements")]
    public Text scoreTitle;
    public Text timeTitle;
    public GameObject gameOverMenu;

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
        gameOverMenu.SetActive(false);
        FromPoolSpawner("Coins");
        FromPoolSpawner("RedCoins");
        FromPoolSpawner("Obstacles");
    }

    private void Update()
    {
        TimeCounterUI();
    }

    public void CoinCounterUI()
    {
        scoreTitle.text = "Score: " + score.ToString();
    }

    private void TimeCounterUI()
    {
        timeTitle.text = "Time: " + System.Math.Round(Time.timeSinceLevelLoad, 2);
    }

    public void OnPlayerDeath()
    {
        Time.timeScale = 0.0f;
        gameOverMenu.SetActive(true);
    }

    public void QuitUI()
    {
        Application.Quit();
    }

    public void RetryUI()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
