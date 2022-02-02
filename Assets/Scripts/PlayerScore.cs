using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{
    public float timeLeft;
    private int playerScore;
    public GameObject timeLeftUI;
    public GameObject playerScoreUI;
    public string resetSceneName;

    bool trigger = false;
 
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeLeftUI.gameObject.GetComponent<Text>().text = ("Time Left: " + (int)timeLeft);
        playerScoreUI.gameObject.GetComponent<Text>().text = ("Score: " + playerScore);

        if (timeLeft < 0.1f)
        {
            SceneManager.LoadScene(resetSceneName);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            if(trigger == false)
            {
                CountScore();
                trigger = true;
            }  
        }      
    }

    public void addScore(int value)
    {
        playerScore += value;
    }

    void CountScore()
    {
        playerScore = playerScore + (int)(timeLeft * 10);
        finishScore.score = playerScore;
    }
}

public static class finishScore
{
    public static int score;
}

