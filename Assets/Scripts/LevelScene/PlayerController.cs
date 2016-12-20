using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public float horizontalSpeedMultiplier;
	public float verticalSpeedMultiplier;
	public float tilt;
	public Boundary boundary;

	public GameObject explosion;

	public float fireRate;
	public GameObject shot;
	public Transform shotSpawn;

	private float nextFire;
	private Rigidbody rigidBody;
	private AudioSource audioSource;
	private GameController gameController;

	public void Destroy() {
		Instantiate(explosion, transform.position, transform.rotation);
		gameController.GameOver = true;
		Destroy(gameObject);
	}

	void Start() {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			this.gameController = gameControllerObject.GetComponent<GameController>();
		} else {
			Debug.Log("Cannot find GameController");
		}	
	}

	void Update() {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play();
		}
		speed = this.gameController.Speed;
	}

	void FixedUpdate() {
		if (!gameController.GameOver) {
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertically = 1.0f;

			Vector3 movement = new Vector3(moveHorizontal * horizontalSpeedMultiplier, 
			                               0.0f, 
			                               moveVertically * verticalSpeedMultiplier);
			rigidBody.velocity = movement * this.gameController.Speed;

			Clamp();

			rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
			Debug.Log(String.Format("moveHorizontal={0} moveVertically={1} movement={2} velocity={3} rotation={4}",
			                        moveHorizontal, moveVertically, movement, rigidBody.velocity, rigidBody.rotation));
			//Debug.Log(rigidBody);
		}
	}

	private void Clamp() {
		rigidBody.position = new Vector3(
			Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
		);
	}
}
