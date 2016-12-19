using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class BackgroundScroller : MonoBehaviour {

	public float speedMultiplier;
	public float tileSizeZ;

	private Vector3 startPosition;
	private GameController gameController;

	void Start() {
		startPosition = transform.position;
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		} else {
			Debug.Log("Cannot find GameController");
		}

	}

	void Update() {
		float newPosition = Mathf.Repeat(Time.time * speedMultiplier, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition; 
		//Debug.Log(String.Format("time={0} tileSizeZ={1} speed={2} speedMultiplier={3} "));
	}
}
