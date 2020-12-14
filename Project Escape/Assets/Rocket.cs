using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // adjustable parameters
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    // rocket components
    Rigidbody rocketRigidBody;
    AudioSource rocketAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        // get the reference to the rocket ship's rigid body
        rocketRigidBody = GetComponent<Rigidbody>();
        rocketAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Fuel":
                print("Added Fuel");
                break;
            default:
                print("Dead");
                break;
        }
    }

    private void Thrust()
    {
        // GetKeyDown() is different. It only applies for jumping or one time firing
        if (Input.GetKey(KeyCode.Space))
        {
            // add force to the direction the rocket is facing
            rocketRigidBody.AddRelativeForce(Vector3.up * mainThrust);
            // do not layer the audio source
            if (!rocketAudioSource.isPlaying)
            {
                rocketAudioSource.Play();
            }
        }
        else if (Input.GetKeyUp("space"))
        {
            rocketAudioSource.Stop();
        }
    }

    private void Rotate()
    {
        // so the rocket doesn't spin out of control
        rocketRigidBody.freezeRotation = true; // take manual control of rotation
        
        // make speed of rotation the same for all computers
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {       
            // rocket will rotate around the Z axis
            transform.Rotate(Vector3.forward * rotationThisFrame);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rocketRigidBody.freezeRotation = false; // resume physics control of rotation
    }
}
