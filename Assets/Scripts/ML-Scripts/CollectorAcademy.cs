using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using MLAgents;

public class CollectorAcademy : Academy
{
    public int totalScore;
    public Text scoreText;
    public Text timeTitle;
    public Text finishTime;
    public GameObject gameOverMenu;
    public Agent playerAgent;

    public override void AcademyReset()
    {
        totalScore = 0;
        timeTitle.text = "Time: 0";
        timeTitle.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public override void AcademyStep()
    {
        scoreText.text = string.Format(@"Score: {0}", totalScore);
        timeTitle.text = "Time: " + System.Math.Round(Time.timeSinceLevelLoad, 2);

        if (totalScore >= 20)
        {
            finishTime.text = "Collection time: " + System.Math.Round(Time.timeSinceLevelLoad, 2);
            timeTitle.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RetryUI()
    {
        Time.timeScale = 1.0f;
        AcademyReset();
        playerAgent.AgentReset();
    }

    public void QuitUI()
    {
        Application.Quit();
    }
}
