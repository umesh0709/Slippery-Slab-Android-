using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public static ColorChange instance;
    Color startColor = Color.red;
    Color endColor = Color.green;
    Renderer renderer = new Renderer();
    float startTime;
    [SerializeField] float speed = 80f;
    public bool isRepeatable = true;
    GameObject enemyScore;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        renderer = GetComponent<Renderer>();
        startTime = Time.time;
        enemyScore = GameObject.FindGameObjectWithTag("Score");
    }

    
    void Update()
    {
        ColorSwap();
        TagChange();
    }

    private void ColorSwap()
    {
        if (!isRepeatable)
        {
            float t = (Time.time - startTime) * speed;
            renderer.material.color = Color.Lerp(startColor, endColor, t);
        }
        else
        {
            float t = Mathf.Cos(Time.time - startTime) * speed;
            renderer.material.color = Color.Lerp(startColor, endColor, t);
        }
    }
   private void TagChange()
    {
        if (renderer.material.color == Color.red)
        {
            gameObject.tag = "Enemy";
            Debug.Log("Enemy tag");
            enemyScore.SetActive(false);
        }
        if (renderer.material.color == Color.green)
        {
            gameObject.tag = "Support";
            enemyScore.SetActive(true);
            
        }
    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if ((this.renderer.material.color == Color.green) && (ScoreBoard.score < 300))
            {

                isRepeatable = false;
                Debug.Log("Repeatable color off");

            }
            else
            {
                isRepeatable = true;
                Debug.Log("Repeatable color on");
            }
        }
       
    }

    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isRepeatable = true;
        }
    }

 

}
