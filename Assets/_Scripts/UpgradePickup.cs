using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePickup : MonoBehaviour {
	public Gun gun;

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			gun.Upgrade (this);
			Destroy (gameObject);
		}
	}
}