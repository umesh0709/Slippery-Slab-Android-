using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOscillation : MonoBehaviour
{
    float movementFactor;
    float movementOffset = 2f;
    [SerializeField] Vector3 movementVector = new Vector3(0,0,0);
    Vector3 startingPos;
    [SerializeField] float period;
    [SerializeField] Vector3 offset;
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
        const float tau = Mathf.PI * 2;
        float sineWave = Mathf.Sin(cycle * tau);
        movementFactor = sineWave / movementOffset;
        offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
    }
}
