using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    
    public static float timer;
    float adDuration = 180f;
    string gameId = "3954343";
    bool testMode = true;
    string bannerAd = "bannerad";
    string skipAd = "video";
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        timer = adDuration;
        Advertisement.Initialize(gameId, testMode);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.fixedDeltaTime;
       
    }

    public IEnumerator BannerAd()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
        while (!Advertisement.IsReady(bannerAd))
            yield return null;
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerAd);
    }

    public void SkipAd()
    {
        
        if ((timer <= 0) && (Advertisement.IsReady(skipAd)))
            {
                Advertisement.Show(skipAd);
                timer = adDuration;
                Time.timeScale = 0f;
            }
       
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
       
    }

    public void OnUnityAdsDidStart(string placementId)
    {
      
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        
                timer = adDuration;
                Time.timeScale = 0f;
          
    }

}
