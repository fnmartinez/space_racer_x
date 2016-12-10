using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCounts;
	public float spawnStart;
	public float spawnWait;

	public Text scoreText;

	public GameObject gameOverWindow;

	private int score = 0;
	private bool gameOver = false;

	void Start() {
		gameOverWindow.SetActive(false);
		StartCoroutine(SpawnHazards());
		UpdateScore();
	}

	IEnumerator SpawnHazards() {
		yield return new WaitForSeconds(spawnStart);
		while (!gameOver) {
			for (int i = 0; i < hazardCounts; i++) {
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
		}
	}

	void UpdateScore() {
		scoreText.text = score.ToString();
	}

	public void AddScore(int addingScore) {
		//Debug.Log("Adding Score: " + addingScore);
		score += addingScore;
		//Debug.Log("New Score: " + score);
		UpdateScore();
	}

	public void GameOver() {
		gameOverWindow.SetActive(true);
		gameOver = true;
	}
}
