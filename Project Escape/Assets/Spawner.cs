using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Need a reference to the asteroid game object
    [SerializeField] GameObject Asteroid;
    [SerializeField] float UpperSpawnBound = 7;
    [SerializeField] float LowerSpawnBound = 10;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAsteroid());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnAsteroid()
    {
        // wait for 1 second before spawning an asteroid. 100 iterations should be
        // more than enough time to finish up the entire level
        for (int i = 0; i < 100; i++)
        {
            // Wait for one second in between asteroid spawns. Spawn asteroids between the range
            // of the spawners y position - LowerSpawnBound and y position + UpperSpawnBound
            yield return new WaitForSeconds(1);
            float verticalPosition = Random.Range(transform.position.y - LowerSpawnBound, transform.position.y + UpperSpawnBound);
            Instantiate(Asteroid, new Vector3(transform.position.x, verticalPosition, transform.position.z), Quaternion.identity);
        }
    }
}
