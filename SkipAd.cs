using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class SkipAd : MonoBehaviour, IUnityAdsListener
{
    public static SkipAd instance;

    string gameId = "3954343";
    bool testMode = true;
    string skipAd = "video";

    private void Awake()
    {
        instance = this;
       
    }
    

    public void SkipAds()
    {
        Advertisement.Initialize(gameId, testMode);
        if (Advertisement.IsReady(skipAd))
        {
            Advertisement.Show(skipAd);
        }
        else
        {
            Collision.instance.Restart();
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsReady(string placementId)
    {
        throw new System.NotImplementedException();
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Skipped)
        {

            if (MainMenu.instance.deathUI != null)
            {
                MainMenu.instance.deathUI.SetActive(false);
            }
            Time.timeScale = 1;
            Health.healthCount++;
            Collision.instance.Death();
        }
        else if(showResult == ShowResult.Finished)
        {
            if (MainMenu.instance.deathUI != null)
            {
                MainMenu.instance.deathUI.SetActive(false);
            }
            Time.timeScale = 1;
            Health.healthCount++;
            Collision.instance.Death();
        }
    }

  

}
