using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.SocialPlatforms;

public class MainMenu : MonoBehaviour, IUnityAdsListener 
{
    public static MainMenu instance;
    Collision collision;
    AdManager adManager;
    UpwardMovement upwardMovement;
    HighScore highScore;

    float lerpDisableDelay = 1.6f;
    float movementEnableDelay = 1.2f;

    public GameObject pause;
    public GameObject deathUI;
    public GameObject titleUI;
    public GameObject finishUI;
    public GameObject highScoreUI;
    public GameObject howToPlayUI;
    public GameObject pauseUI;
    public GameObject soundOn;
    public GameObject soundOff;
    public GameObject creditUI;
    public GameObject musicOn;
    public GameObject musicOff;
    public GameObject m_musicOn;
    public GameObject m_musicOff;
    bool soundButtonEnable = true;
    bool musicButtonEnable = true;
    public GameObject settingsUI;
    [SerializeField] GameObject mainMenuUI;
    AudioSource audioSource;
    [SerializeField] AudioClip touchButton;

    public Material[] skybox = new Material[11];

    string rewarded = "rewardedVideo";
    string gameId = "3954343";
    bool testMode = true;
    public static bool isPaused = true;
    [SerializeField] [Range(0.01f, 1f)] float volumeOn;
    float volumeOff = 0f;
    int scoreLimit = 20;
    int volControl;
    int souControl;
  
    private void Awake()
    {
        instance = this;

        int index = Random.Range(0, 10);
        RenderSettings.skybox = skybox[index];

        LastGameVolume();

        LastSoundVolume();

        if(Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }
    }

    private void LastSoundVolume()
    {
        if (PlayerPrefs.HasKey("LastSound"))
        {
            souControl = PlayerPrefs.GetInt("LastSound");
        }
        else
        {
            souControl = 1;
        }
    }

    private void LastGameVolume()
    {
        if (PlayerPrefs.HasKey("LastVolume"))
        {
            volControl = PlayerPrefs.GetInt("LastVolume");
        }
        else
        {
            volControl = 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
      
        Screen.orientation = ScreenOrientation.Portrait;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        Time.timeScale = 0f;
        titleUI.SetActive(true);
        finishUI.SetActive(false);
        highScoreUI.SetActive(false);
        howToPlayUI.SetActive(false);
        mainMenuUI.SetActive(true);
        pauseUI.SetActive(false);
        pause.SetActive(false);
        soundButtonEnable = true;
        settingsUI.SetActive(false);
        creditUI.SetActive(false);
        deathUI.SetActive(false);

        

        upwardMovement = FindObjectOfType<UpwardMovement>();
        adManager = FindObjectOfType<AdManager>();
        collision = FindObjectOfType<Collision>();
        highScore = FindObjectOfType<HighScore>();

        Advertisement.Initialize(gameId, testMode);
        upwardMovement.enabled = false;

        VolumeController();
        soundController();
    }

    // Update is called once per frame
    void Update()
    {
        

        GameSound();

        MusicControlInGame();

        MusicControlMainMenu();

        Death();

        if (Time.timeScale == 1)
        {
            isPaused = false;
        }
        else if (Time.timeScale == 0)
        {
            isPaused = true;
        }

       
        
    }
  
  void VolumeController()
    {
        if(volControl == 1)
        {
            musicButtonEnable = true;
        }
        else if(volControl == 0)
        {
            musicButtonEnable = false;
        }
    }
    void soundController()
    {
        if (souControl == 1)
        {
            soundButtonEnable = true;
        }
        if(souControl == 0)
        {
            soundButtonEnable = false;
        }
    }

    private void MusicControlMainMenu()
    {
        if (musicButtonEnable)
        {
            volControl = 1;
          
            audioSource.volume = volumeOn;
            m_musicOn.SetActive(true);
            m_musicOff.SetActive(false);
            PlayerPrefs.SetInt("LastVolume", volControl);
        }
        else if (!musicButtonEnable)
        {
            volControl = 0;
        
            audioSource.volume = volumeOff;
            m_musicOn.SetActive(false);
            m_musicOff.SetActive(true);
            PlayerPrefs.SetInt("LastVolume", volControl);
        }
    }

    private void MusicControlInGame()
    {
        if (musicButtonEnable)
        {
            volControl = 1;
          
            audioSource.volume = volumeOn;
            musicOn.SetActive(true);
            musicOff.SetActive(false);
            PlayerPrefs.SetInt("LastVolume", volControl);
        }
        else if (!musicButtonEnable)
        {
            volControl = 0;
          
            audioSource.volume = volumeOff;
            musicOn.SetActive(false);
            musicOff.SetActive(true);
            PlayerPrefs.SetInt("LastVolume", volControl);
        }
    }

    private void GameSound()
    {
        if (soundButtonEnable)
        {
            souControl = 1;
            Collision.instance.AudioOn();
            soundOn.SetActive(true);
            soundOff.SetActive(false);
            PlayerPrefs.SetInt("LastSound", souControl);
        }
        if (!soundButtonEnable)
        {
            souControl = 0;
            Collision.instance.AudioOff();
            soundOff.SetActive(true);
            soundOn.SetActive(false);
            PlayerPrefs.SetInt("LastSound", souControl);
        }
    }

    public void StartGame()
    {
        
        StartCoroutine(adManager.BannerAd());
        UpwardMovement.isControlEnable = false;
        mainMenuUI.SetActive(false);
        titleUI.SetActive(false);
        pause.SetActive(true);
        HighScore.highScoreText.enabled = true;
        ScoreBoard.scoreText.enabled = true;
        Invoke("LerperDisable", lerpDisableDelay);
        Invoke("UpwardMovementEnable", movementEnableDelay);
        Time.timeScale = 1f;
    }
  
    void LerperDisable()
    {
        LerperPlayer.thisScript.enabled = false;
        Debug.Log("Script Disabled");
    }

    void UpwardMovementEnable()
    {
        upwardMovement.enabled = true;
    }
    public void Pause()
    {
       
        UpwardMovement.isControlEnable = false;
        Time.timeScale = 0f;
        audioSource.PlayOneShot(touchButton);
        pauseUI.SetActive(true);
    }
    public void Restart()
    {
      
        adManager.SkipAd();
        ScoreBoard.score = 0;
        audioSource.PlayOneShot(touchButton);
        ScoreBoard.scoreText.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
    }
    public void Resume()
    {
        UpwardMovement.isControlEnable = false;
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        audioSource.PlayOneShot(touchButton);
        UpwardMovement.isControlEnable = true;
    }
    public void settings()
    {
        adManager.SkipAd();
        UpwardMovement.isControlEnable = false;
        audioSource.PlayOneShot(touchButton);
        mainMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }
    public void Quit()
    {
        audioSource.PlayOneShot(touchButton);
        Application.Quit();
        Debug.Log("Quit!!");
    }
    public void Sound()
    {
        audioSource.PlayOneShot(touchButton);
        soundButtonEnable = !soundButtonEnable;
    }
    public void Music()
    {
        audioSource.PlayOneShot(touchButton);
        musicButtonEnable = !musicButtonEnable;
    }
    public void Credit()
    {
        adManager.SkipAd();
        audioSource.PlayOneShot(touchButton);
        titleUI.SetActive(false);
        settingsUI.SetActive(false);
        mainMenuUI.SetActive(false);
        creditUI.SetActive(true);
    }
    public void CreditBack()
    {
        adManager.SkipAd();
        audioSource.PlayOneShot(touchButton);
        titleUI.SetActive(true);
        settingsUI.SetActive(true);
        creditUI.SetActive(false);
    }
    public void SettingsBack()
    {
        adManager.SkipAd();
        audioSource.PlayOneShot(touchButton);
        settingsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void HowToPlay()
    {

        adManager.SkipAd();
        titleUI.SetActive(false);
        audioSource.PlayOneShot(touchButton);
        howToPlayUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }
    public void HowToPlayBack()
    {
        adManager.SkipAd();
        audioSource.PlayOneShot(touchButton);
        titleUI.SetActive(true);
        howToPlayUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
    public void HighScoreUI()
    {
        audioSource.PlayOneShot(touchButton);
        if (GPGSAuthenticator.instance.isConnectedToPlayServices)
        {
            Social.ShowLeaderboardUI();
            Debug.Log("Leaderboard");
        }
        else if(!GPGSAuthenticator.instance.isConnectedToPlayServices)
        {
            GPGSAuthenticator.instance.SignIn();
        }
       
    }
 
    public void ClearHighScore()
    {
        adManager.SkipAd();
        audioSource.PlayOneShot(touchButton);
        highScore.ResetScore();
    }
    public IEnumerator Finish()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0f;
        finishUI.SetActive(true);
        
    }
    public void FinishBack()
    {
        adManager.SkipAd();
        audioSource.PlayOneShot(touchButton);
        finishUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void RateGame()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Taksh.SlipperySlab");
    }
    public void Death()
    {
        
        if ((ScoreBoard.score < scoreLimit) && (Health.healthCount < 1))
        {
            adManager.SkipAd();
            collision.Restart();
            Debug.Log("Low score");
        }

        else if((ScoreBoard.score >= scoreLimit) && (Health.healthCount < 1))
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, testMode);
            if (Advertisement.IsReady(rewarded))
            {
                Time.timeScale = 0;
                deathUI.SetActive(true);
                Debug.Log("High Score");
                return;
            }
            else if (!Advertisement.IsReady(rewarded))
            {
               
                collision.Restart();
            }
        }
    }

    public void DeathBack()
    {
        
        adManager.SkipAd();
        ScoreBoard.score = 0;
        audioSource.PlayOneShot(touchButton);
        ScoreBoard.scoreText.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 0f;
    }
    public void AdCheck()
    {
            Advertisement.Show(rewarded);
            print("Ads showing");
    }

    public void OnUnityAdsReady(string placementId)
    {
       
    }

    public void OnUnityAdsDidError(string message)
    {
        message = "Ads Not Ready :(";
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        UpwardMovement.isControlEnable = false;
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
      if (showResult == ShowResult.Finished && placementId == rewarded)
        {
            
            if (deathUI != null)
            {
                deathUI.SetActive(false);
            }
            Time.timeScale = 1;
            Health.healthCount ++;
            collision.Death();
        }
      
        else if (showResult == ShowResult.Failed && placementId == rewarded)
        {
            Time.timeScale = 0;
            collision.Restart();
        }
    }
}


