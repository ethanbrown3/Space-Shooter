using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
	public GameObject[] powerups;
    public GameObject boss;
    public Vector3 spawnValues;
    public float spawnWait;
    public float startWait;
    public float waveWait;
	public float pickUpSpawnFrequency;
    public int hazardCount;
    public int lastWave;
	public Gun playerGun;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text bossText;

    private float gameOverTime;
    private bool isBoss = false;
    private bool gameOver;
    private bool restart;
    private int score;

    void Start () {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        bossText.text = "";
        score = 0;
        StartCoroutine(SpawnWaves());
        UpdateScoreText();
    }

    void Update () {
        if (lastWave <= 0 && !isBoss) {
            isBoss = true;
            hazardCount = 0;
            SpawnBoss();
        } else if (isBoss && playerGun.chargesLeft <= 0) {
            bossText.text = "Out of charges";  
        }
        if (gameOver && (Time.time > gameOverTime + 5.0f)) {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator SpawnWaves () {
        yield return new WaitForSeconds(startWait);
        while (!gameOver) {
            for (int i = 0; i < hazardCount; i++) {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);

				if (Random.value > 1-pickUpSpawnFrequency) {
					SpawnPickup ();
				}
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            --lastWave;
            
        }
    }

    void SpawnBoss() {
        playerGun.canCharge = true;
        bossText.text = "Boss Fight! Hold Right Click to Charge Shot";
        Vector3 spawnPosition = new Vector3(0.0f, 0.0f, 22.25f);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(boss, spawnPosition, spawnRotation);
    }

	private void SpawnPickup () {
		GameObject pickup = powerups[Random.Range(0, powerups.Length)];
		Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
		Quaternion spawnRotation = Quaternion.identity;
		UpgradePickup upgrade = pickup.GetComponent<UpgradePickup>();
		upgrade.gun = playerGun;
		Instantiate(pickup, spawnPosition, spawnRotation);
	}

    void UpdateScoreText () {
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddScore (int newScoreValue) {
        score += newScoreValue;
        UpdateScoreText();
    }

    public void GameOver () {
        gameOverText.text = "Game Over";
        gameOver = true;
        gameOverTime = Time.time;
    }

    public void Win() {
        gameOverText.text = "You Win\n" + "Your Score: " + scoreText.text;
        gameOver = true;
        gameOverTime = Time.time;
    }
}
