using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    Transform playerParent;
    // Start is called before the first frame update
    void Start()
    {
     
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            transform.parent = playerParent.transform;
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Osc")
        {
            transform.parent = null;
        }
    }
}
