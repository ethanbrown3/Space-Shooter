using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeRapid : MonoBehaviour {
	public Gun gun;

	private void OnTriggerEnter(Collider other) {
		if (other.tag.Equals ("Player")) {
			gun.PickupRapid ();
			Destroy (gameObject);
		}
	}
}