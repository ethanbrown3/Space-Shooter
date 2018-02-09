using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

	public GameObject shot;
	public GameObject chargeShot;
	public Transform shotSpawn;
	public Transform shotSpawn2;
	public Transform shotSpawn3;
	public float fireRate;
	public AudioSource weaponAudio;
	public float upgradeTime = 10;
	public float maxCharge;
	public float chargeRate;
	public float chargeDamage;
	public float startCharge;

	public bool isRapid = false;
	public bool isSpread = false;
	public bool canCharge = false;

	private float nextFire = 0.0f; // make sure you can fire again until time has passed this value
	private float timeRapid = 0.0f;
	private float timeSpread = 0.0f;

	void Start() {
		weaponAudio = GetComponent<AudioSource>();
	}

	void Update() {
		// shots fired
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
				fireGun ();
		} else if (canCharge ) {
			chargeUp ();
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

	void chargeUp() {
		if (Input.GetMouseButtonDown(1)) {
			startCharge = Time.time;
		}
		if (Input.GetMouseButtonUp (1)) {
			chargeDamage = (Time.time - startCharge) * chargeRate;
			if (chargeDamage > maxCharge) {
				chargeDamage = maxCharge;
			}
			createBolt ();
			GameObject bolt = Instantiate(chargeShot, shotSpawn.position, shotSpawn.rotation);
			bolt.transform.localScale = new Vector3(chargeDamage/2.0f, chargeDamage/2.0f, chargeDamage/2.0f);
			shot.transform.position = shotSpawn.position;
			weaponAudio.Play();
		}		
	}


	private Rigidbody createBolt() {
		GameObject bolt = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		bolt.transform.position = shotSpawn.position;
		return bolt.GetComponent<Rigidbody>();
	}

}

