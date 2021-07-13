using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreHighScore : MonoBehaviour
{
    public static int highScore;
    public static TMP_Text highScoreText;
    // Start is called before the first frame update
    void Awake()
    {
        highScoreText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        highScore = HighScore.highScore;
        highScoreText.text = highScore.ToString();
    }
}
