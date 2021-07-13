using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    Vector3 offset;
    [SerializeField] float period;
    [SerializeField] Vector3 movementVector = new Vector3(0,0,0);
    float movementFactor;
    float movementOffset = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }
        float cycle = Time.time / period;
        const float tau = Mathf.PI * 2f;

        float sineWave = Mathf.Sin(cycle * tau);
        movementFactor = sineWave / movementOffset;
        offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                collision.transform.SetParent(transform);
               
                break;

        }
    }

    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }

}
