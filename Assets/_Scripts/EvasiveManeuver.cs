﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public float tilt;
    public float smoothing;
    public float dodge;

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWiat;
    public Boundary boundary;

	private float wave;
    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
		if (Random.value < 0.5f) {
			StartCoroutine (Evade ());
		} else {
			StartCoroutine (Wave ());
		}
	}
	
    IEnumerator Evade () {
        yield return new WaitForSeconds (Random.Range(startWait.x, startWait.y));

        while (true) {
            targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x); 
			Debug.Log (targetManeuver);
            yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range (maneuverTime.x, maneuverTime.y));
        }
    }

	void Update () {
		wave = Mathf.Sin(Time.time * 3) * dodge;
	}

	IEnumerator Wave () {
 		float startDirection = Mathf.Sign (transform.position.x);
		while (true) {
			targetManeuver = wave * startDirection;
			Debug.Log (targetManeuver);
			yield return new WaitForSeconds(.001f);
		}
	}

	void FixedUpdate () {
		Debug.Log (targetManeuver);
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, (rb.velocity.x * -tilt));
    }

	Vector3 DiagonalMove () {
	
		return new Vector3 (-2.0f, 0.0f, currentSpeed);

	}
}
