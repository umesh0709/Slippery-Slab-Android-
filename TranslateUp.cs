using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateUp : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] float acceleration = 0.1f;
    float slowDownSpeed = 500f;
    [SerializeField] float speed = 0.0f;
    float maxSpeed = 0.35f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        speed += acceleration / slowDownSpeed;

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
    }
}
