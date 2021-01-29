using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    // adjustable parameters
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip failure;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem failureParticles;
    [SerializeField] ParticleSystem successParticles;

    // rocket components
    Rigidbody rocketRigidBody;
    AudioSource rocketAudioSource;

    enum State { Alive, Dying, Transcending }
    // set rocket to alive at the beginning of each game
    State state = State.Alive;

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
        if (state == State.Alive)
        {
            RespondToThrustInput();
            Rotate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // When we hit something, we do not want redundant execution of this code
        if (state != State.Alive) { return; }

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Complete":
                StartSuccessSequence();
                break;
            default:
                StartFailSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        rocketAudioSource.Stop();
        rocketAudioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextScene", levelLoadDelay); // parameterize time
    }

    private void StartFailSequence()
    {
        state = State.Dying;
        rocketAudioSource.Stop();
        rocketAudioSource.PlayOneShot(failure);
        failureParticles.Play();
        Invoke("LoadFirstScene", levelLoadDelay);
    }

    private void LoadNextScene()
    {
        // Every level is meant to be played in order
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void RespondToThrustInput()
    {
        // GetKeyDown() is different. It only applies for jumping or one time firing
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else if (Input.GetKeyUp("space"))
        {
            rocketAudioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        // add force to the direction the rocket is facing
        rocketRigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        // do not layer the audio source
        if (!rocketAudioSource.isPlaying)
        {
            // How we will play more than one sound
            rocketAudioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            // we do not want to play particle effects for every second we are pressing space bar
            mainEngineParticles.Play();
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
