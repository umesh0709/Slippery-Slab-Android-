using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class Health : MonoBehaviour
{
    public static int healthCount;
   
  
    string bannerAd = "bannerad";
    float timer = 20f;
    
 
    // Start is called before the first frame update
     void Start()
    {
        timer = Time.deltaTime;
        healthCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (healthCount < 0)
        {
            healthCount = 0;
        }
        print(healthCount);
    }
}
