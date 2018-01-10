using System.Collections;
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

    // shooting publics
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire = 0.0f; // make sure you can fire again until time has passed this value


    void Start() {
        // get the Rigidbody from the object
        rb = GetComponent<Rigidbody>();

    }

    void Update() {

        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate; // update the time we can fire again
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
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
