﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public Rigidbody rb;

    // movement publics
    public float speed;
    public Boundary boundary;
    public float tilt;

    void Start() {
        // get the Rigidbody from the object
        rb = GetComponent<Rigidbody>();
    }
		
    // Used for physics, called automatically just before each fixed physics step
    void FixedUpdate() {
        // get input values
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement*speed;

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f, 
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        ); 

        // tilt the ship when moving
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, (rb.velocity.x * -tilt));
    }
 	
}
