using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {

	public Animator animator;
    public GameObject explosion;
    public GameObject playerExplosion;
	public float health = 25.0f;
    public int scoreValue;
    private GameController gameController;

    void Start() {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null) {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("rapidPickup") ||  other.CompareTag("spreadPickup")) {
            return;
        }
			
        if (other.CompareTag("Player")) {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

		if (other.CompareTag("Bolt")) {
			float damage = (other.transform.localScale.x * 2.0f);
			health -= damage;
			animator.SetTrigger ("TakingDamage");
		}
        
		if (health <= 0) {
			gameController.AddScore(scoreValue);
			Destroy(other.gameObject);
			Destroy(gameObject);
			if (explosion != null) {
				Instantiate(explosion, transform.position, transform.rotation);
			}

		}

    }
}
