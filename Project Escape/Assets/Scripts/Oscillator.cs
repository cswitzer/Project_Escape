using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f; // 2 seconds

    // todo remove from inspector later
    [Range(0,1)] [SerializeField]
    float movementFactor;  // 0 for not moved, 1 for fully moved

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // set movement factor/Epsilon is closest float to 0
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // grows continually from 0
        
        // pi is 3.14 radian, but tau is 6.28 radians, or the full circumference of a circle
        const float tau = Mathf.PI * 2f; // about 6.28
        float rawSineWave = Mathf.Sin(cycles * tau);

        // we only want to movement factor to be from 0-1. Halving sine wave
        // puts us between -0.5 and 0.5, which needs to 0.5f added to be between 0-1
        movementFactor = rawSineWave / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
