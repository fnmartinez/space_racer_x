using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public PlayerController playerController;
	public Vector3 spawnValues;
	public int hazardCounts;
	public int randomSeed;
	public float spawnStart;
	public float spawnWait;
	public float incrementTimeDelta;
	public float incrementSpeedDelta;
	public float baseSpeed;
	public float maxSpeed;

	public Text scoreText;

	public GameObject gameOverWindow;

	private int score = 0;
	private float speed;
	private bool gameOver = false;
	private float nextSpeedChange;

	public void AddScore(int addingScore) {
		score += addingScore;
		UpdateScore();
	}

	public float Speed {
		get { return speed; }
	}

	public bool GameOver {
		get { return gameOver; }
		set {
			gameOver = value;
			if (gameOver) {
				gameOverWindow.SetActive(true);
				//speed = 0.0f;	
			}
		}
	}

	void Start() {
		speed = baseSpeed;
		nextSpeedChange = 0.0f;
		UnityEngine.Random.InitState(randomSeed);
		gameOverWindow.SetActive(false);
		StartCoroutine(SpawnHazards());
		UpdateScore();
	}

	IEnumerator SpawnHazards() {
		yield return new WaitForSeconds(spawnStart);
		while (!gameOver) {
			for (int i = 0; i < hazardCounts; i++) {
				GameObject hazard = hazards[UnityEngine.Random.Range(0, hazards.Length)];
				Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
		}
	}

	void UpdateScore() {
		scoreText.text = score.ToString();
	}

	void Update() {
		if (Time.time > nextSpeedChange && speed < maxSpeed) {
			AlterSpeed(incrementSpeedDelta);
			nextSpeedChange = Time.time + incrementTimeDelta;
			Debug.Log(String.Format("speed={0} nextSpeedChange={1}", speed, nextSpeedChange));
		}
	}

	private void AlterSpeed(float speedDelta) {
		speed += speedDelta;
	}
}
