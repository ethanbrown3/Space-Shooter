using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {

	public Animator animator;
    public GameObject explosion;
    public GameObject playerExplosion;
	public float health = 25.0f;
    public int scoreValue;
    public float hitRecovery;

    private GameController gameController;
    private float timeHit;
    private bool isHitable;


    void Start() {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null) {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void Update() {
        if (timeHit + hitRecovery < Time.time && GetComponent<BossMovement>().isInPosition == true) {
            isHitable = true;
        }
    }

    void OnTriggerEnter(Collider other) {
		if (!isHitable || other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("rapidPickup") ||  other.CompareTag("spreadPickup")) {
            return;
        }
			
        if (other.CompareTag("Player")) {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
            Destroy(other.gameObject);
        }

		if (other.CompareTag("Bolt")) {
			float damage = (other.transform.localScale.x * 2.0f);
			health -= damage;
			animator.SetTrigger ("TakingDamage");
            isHitable = false;
            timeHit = Time.time;
            gameController.AddScore(Mathf.RoundToInt(damage)*5);
        }
        
		if (health <= 0) {
			gameController.AddScore(scoreValue);
			Destroy(other.gameObject);
			Destroy(gameObject);
			if (explosion != null) {
				Instantiate(explosion, transform.position, transform.rotation);
			}
            gameController.Win();

		}

    }
}
