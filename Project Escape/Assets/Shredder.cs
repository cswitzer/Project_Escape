using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // destroy asteroids that crash into us
        Debug.Log("Asteroid Destroyed");
        Destroy(other.gameObject);
    }
}
