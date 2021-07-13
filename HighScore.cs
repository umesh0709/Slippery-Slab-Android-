using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class HighScore : MonoBehaviour
{
    public static HighScore instance;
    public static int highScore;
    public static TMP_Text highScoreText;

    private void Awake()
    {
        highScore = 0;
        instance = FindObjectOfType<HighScore>();
        highScoreText = GetComponent<TMP_Text>();
        if (PlayerPrefs.HasKey("LastHighScore"))
        {
            highScore = PlayerPrefs.GetInt("LastHighScore", highScore);
            highScoreText.text = highScore.ToString();
        }
        else
        {
           
        }
        highScoreText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        HighScoreCount();
    }

    public int HighScoreCount()
    {
      
        if (ScoreBoard.score > highScore)
        {
            
            highScore = ScoreBoard.score;
            highScoreText.text = highScore.ToString();
            PlayerPrefs.SetInt("LastHighScore", highScore);
            UpdateLeaderboard();
        
        }

        return highScore;
    }

    public void UpdateLeaderboard()
    {
        if((highScore == 0))
        {
            return;
        }
        else
        {
            if (GPGSAuthenticator.instance.isConnectedToPlayServices)
            {
                Social.ReportScore(PlayerPrefs.GetInt("LastHighScore"), GPGSIds.leaderboard_high_score, (success) =>
                {
                    if (!success)
                    {
                        Debug.Log("Can't post high score");
                    }
                });
            }
            else
            {
                Debug.Log("Not signed in");
            }
        }

       
    }


    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("LastHighScore");
        highScore = 0;
        highScoreText.text = highScore.ToString();
    }
}
