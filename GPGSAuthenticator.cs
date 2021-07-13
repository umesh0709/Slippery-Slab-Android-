using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSAuthenticator : MonoBehaviour
{
    public static GPGSAuthenticator instance;
    public static PlayGamesPlatform platform;
    public bool isConnectedToPlayServices;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        if(platform == null)
        {
            PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(configuration);
            PlayGamesPlatform.DebugLogEnabled = true;

            platform = PlayGamesPlatform.Activate();
        }

        SignIn();
        
    }

    public void SignIn()
    {
        if(!Social.Active.localUser.authenticated)
        {
            Social.Active.localUser.Authenticate(success =>
            {
                if (success)
                {
                    Debug.Log("Logged in");
                    isConnectedToPlayServices = true;
                }
                else
                {
                    Debug.Log("Already Logged in");
                    isConnectedToPlayServices = false;
                }
            });
        }
    }
}
