using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/* This class is responsible for score updates, depending on the number of kills the user gets */
public class ScoreScript : MonoBehaviour
{
    public static int scoreValue = 0;
    public Text myCurrentscore;
    public Text gameOverFinalScore;

    // Start is called before the first frame update
    void Start()
    {
        myCurrentscore = GetComponent<Text>();
        gameOverFinalScore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        myCurrentscore.text = scoreValue.ToString();
        gameOverFinalScore.text = scoreValue.ToString();
    }
}
