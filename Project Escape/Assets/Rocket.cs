using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocketRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        // get the reference to the rocket ship's rigid body
        rocketRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        // GetKeyDown() is different. It only applies for jumping or one time firing
        if (Input.GetKey(KeyCode.Space))
        {
            // add force to the direction the rocket is facing
            rocketRigidBody.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // rocket will rotate around the Z axis
            transform.Rotate(Vector3.forward);
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }
    }
}
