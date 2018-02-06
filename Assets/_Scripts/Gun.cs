﻿using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

	public GameObject shot;
	public Transform shotSpawn;
	public Transform shotSpawn2;
	public Transform shotSpawn3;
	public float fireRate;
	public AudioSource weaponAudio;

	public bool isRapid = false;
	public bool isSpread = false;
	private float nextFire = 0.0f; // make sure you can fire again until time has passed this value
	private float timeRapid, timeSpread;
	private float defaultFireRate;
	void Start() {
		weaponAudio = GetComponent<AudioSource>();
		defaultFireRate = fireRate;
	}

	void Update() {
		// shots fired
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			fireGun ();
		}


	}

	// doubles the fire rate
	public void pickupRapid() {
		isRapid = true;
		fireRate = fireRate/2.0f;
		timeRapid = 0;
	}

	public void pickupSpread() {
		isSpread = true;
		timeSpread = 0;
	}

	void fireGun() {
		nextFire = Time.time + fireRate; // update the time we can fire again
		createBolt();
		weaponAudio.Play();
		if (isSpread) {
			Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);
			Instantiate(shot, shotSpawn3.position, shotSpawn3.rotation);
		}
	}

	private Rigidbody createBolt() {
		GameObject bolt = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		shot.transform.position = shotSpawn.position;
		return shot.GetComponent<Rigidbody>();
	}

}
