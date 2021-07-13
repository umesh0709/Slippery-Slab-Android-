using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerper : MonoBehaviour
{
    public static Lerper instance;
    [SerializeField] Vector3 startPos, endPos;
    [SerializeField] float speed, duration;
    float startTime, totalDistance;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
       
        startTime = Time.time;
        totalDistance = Vector3.Distance(startPos, endPos);

    }

    private void Update()
    {
       
        float currentDuration = (Time.time - startTime) * speed;
        float journeyFraction = currentDuration / totalDistance;

        transform.position = Vector3.Lerp(startPos, endPos, journeyFraction);

    }
 
}