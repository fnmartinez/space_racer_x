using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float speedMultiplier;

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			GameController gameController = gameControllerObject.GetComponent<GameController>();
			GetComponent<Rigidbody>().velocity = GetComponent<Transform>().forward * gameController.Speed * speedMultiplier;
		} else {
			Debug.Log("Cannot find GameController");
		}
	}
}
