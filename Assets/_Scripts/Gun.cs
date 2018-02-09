using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

	public GameObject shot;
	public Transform shotSpawn;
	public Transform shotSpawn2;
	public Transform shotSpawn3;
	public float fireRate;
	public AudioSource weaponAudio;
	public float upgradeTime = 10;

	public bool isRapid = false;
	public bool isSpread = false;

	private float nextFire = 0.0f; // make sure you can fire again until time has passed this value
	private float timeRapid = 0.0f;
	private float timeSpread = 0.0f;

	void Start() {
		weaponAudio = GetComponent<AudioSource>();
	}

	void Update() {
		// shots fired
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			fireGun ();
		}
		if (Time.time > timeSpread) {
			isSpread = false;
		}
		if (Time.time > timeRapid) {
			isRapid = false;
		}

	}

	// doubles the fire rate
	public void Upgrade(UpgradePickup pickup) {
		if (pickup.CompareTag("rapidPickup")) {
			isRapid = true;
			timeRapid = Time.time + upgradeTime;
		} else if (pickup.CompareTag("spreadPickup")) {
			isSpread = true;
			timeSpread = Time.time + upgradeTime;
		}
	}

	void fireGun() {
		nextFire = (isRapid) ? Time.time + (fireRate/2) : Time.time + fireRate; // update the time we can fire again
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

