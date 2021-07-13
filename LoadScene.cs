using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadGameScene", 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadGameScene()
    {
        
        SceneManager.LoadScene(1);

    }
}
