using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public int scoreValue;

	private GameController gameController;

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			this.gameController = gameControllerObject.GetComponent<GameController>();
		} else {
			Debug.Log("Cannot find GameController");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Boundary") ||
		    other.CompareTag("Hazard") ||
		    other.CompareTag("Enemy")) {
			return;
		}
		if (explosion != null) {
			Instantiate(explosion, transform.position, transform.rotation);
		}
		if (other.CompareTag("Player")) {
			other.GetComponentInParent<PlayerController>().Destroy();
		}
		gameController.AddScore(scoreValue);
		Destroy(other.gameObject);
		Destroy(this.gameObject);
	}
}
