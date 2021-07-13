using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Collision : MonoBehaviour
{
   
    [Header("GENERAL")]
    [SerializeField] Transform camera;
    [SerializeField] Transform deathGround;

    [Header("OFFSET")]
    [SerializeField] Vector3 offsetLeft;
    [SerializeField] Vector3 offsetRight;
    [SerializeField] Vector3 offsetCentre;
    [SerializeField] Vector3 deathGroundOffset;
     Vector3 currentPos;

    public GameObject finishFX;

    float camMovDelay = 0.6f;
    public static bool isGrounded;
    ScoreBoard scoreBoard;
   
    Rigidbody rb;
    Health health;
    public GameObject playerPrefab;
    
    AudioSource audioSource;
    MainMenu mainMenu;
    [Header("AUDIO")]
    [SerializeField] AudioClip touchSound;
    [SerializeField] AudioClip coinSound;

    string gameId = "3954343";
  
    bool testMode = true;


    public static Collision instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        mainMenu = FindObjectOfType<MainMenu>();
        scoreBoard = FindObjectOfType<ScoreBoard>();
        health = GetComponent<Health>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        
    }

    public void AudioOff()
    {
        audioSource.volume = 0;
    }
    public void AudioOn()
    {
        audioSource.volume = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            UpwardMovement.isControlEnable = true;
        }
        
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
      
            
       
    }

    private void OnCollisionExit(UnityEngine.Collision collision)
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        switch (other.gameObject.tag)
        {
            case "Support":
                UpwardMovement.isControlEnable = true;
                StartCoroutine(camPos(other));
                break;

            case "Osc":
                UpwardMovement.isControlEnable = true;
                transform.parent = other.transform;
                StartCoroutine(camPos(other));
                break;

            case "CurrentPosition":
                currentPos = other.transform.position;
                Debug.Log("Current pos triggered");
                Destroy(other.gameObject);
                break;

            case "DeathGround":
                Health.healthCount--;
                UpwardMovement.isControlEnable = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                transform.rotation = Quaternion.Euler(0, 45, 0);
                Debug.Log("DeathGround triggered");
                break;

            case "Enemy":
                other.isTrigger = false;
                Health.healthCount--;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                transform.rotation = Quaternion.Euler(0, 45, 0);
                break;


            case "Finish":
                finishFX.SetActive(true);
                StartCoroutine(mainMenu.Finish());
                break;

            case "Score":
                scoreBoard.GameScore();
                audioSource.PlayOneShot(touchSound);
                Destroy(other.gameObject);
                Debug.Log("Score object destroyed");
                break;

            case "Coin":
                deathGround.transform.position = deathGround.transform.position + deathGroundOffset;
                Debug.Log(deathGround.transform.position);
                audioSource.PlayOneShot(coinSound);
                Destroy(other.gameObject);
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                Health.healthCount--;
                other.isTrigger = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                transform.rotation = Quaternion.Euler(0, 45, 0);
                break;

            case "Support":
                UpwardMovement.isControlEnable = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
        switch (other.gameObject.tag)
        {

            case "OscRight":
                transform.parent = null;
                break;

            case "Support":
                UpwardMovement.isControlEnable = false;
                break;

            case "Enemy":
                other.isTrigger = true;
                break;

        }
    }

    public void Restart()
    {
        UpwardMovement.isControlEnable = false;
        ScoreBoard.score = 0;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 45, 0);
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Death()
    {
     
        transform.position = new Vector3(currentPos.x, currentPos.y, 0);
        deathGround.transform.position = transform.position + deathGroundOffset;
        Debug.Log("Death");
        UpwardMovement.isControlEnable = false;
    }
   

    IEnumerator camPos(Collider other)
    {
        yield return new WaitForSeconds(camMovDelay);
        Vector3 target = new Vector3(0, other.transform.position.y + 1, -10f);
        camera.transform.position = target;
       
    }

}
