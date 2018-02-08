using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpread : MonoBehaviour {
	public Gun gun;

	private void OnTriggerEnter(Collider other) {
		if (other.tag.Equals ("Player")) {
			gun.PickupSpread ();
			Destroy (gameObject);
		}
	}
}