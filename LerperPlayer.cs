using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerperPlayer : MonoBehaviour
{
    [SerializeField] Vector3 startPos, endPos;
    [SerializeField] float speed, duration;
    float startTime, totalDistance;
    public static LerperPlayer thisScript;


    // Start is called before the first frame update
    void Start()
    {
        UpwardMovement.isControlEnable = false;
        thisScript = GetComponent<LerperPlayer>();
        thisScript.enabled = true;
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