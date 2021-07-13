using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard instance;
    public static int score = 0;
    public static TMP_Text scoreText;
    int scoreCount = 1;
    MainMenu mainMenu;
    HighScore highScore;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        highScore = FindObjectOfType<HighScore>();
        mainMenu = FindObjectOfType<MainMenu>();
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = score.ToString();
        scoreText.enabled = false;
    }
    
   public int GameScore()
    { 
        score = score + scoreCount;
        scoreText.text = score.ToString();
        Debug.Log(score);
        highScore.HighScoreCount();
        return score;

    }
   
    
        
    
}
