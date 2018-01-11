using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public int hazardCount;

    public Text scoreText;
    private int score;

    void Start () {
        StartCoroutine(SpawnWaves());
        UpdateScoreText();
    }

    IEnumerator SpawnWaves () {
        yield return new WaitForSeconds(startWait);
        while (true) {
            for (int i = 0; i < hazardCount; i++) {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    void UpdateScoreText () {
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddScore (int newScoreValue) {
        score += newScoreValue;
        UpdateScoreText();
    }
}
