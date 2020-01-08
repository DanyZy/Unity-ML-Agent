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

    public override void AcademyReset()
    {
        totalScore = 0;
        timeTitle.text = "Time: 0";
    }

    public override void AcademyStep()
    {
        scoreText.text = string.Format(@"Score: {0}", totalScore);
        timeTitle.text = "Time: " + System.Math.Round(Time.timeSinceLevelLoad, 2);
    }
}
