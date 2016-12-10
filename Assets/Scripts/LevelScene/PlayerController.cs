using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
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
		gameController.GameOver();
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
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play();
		}
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
		rigidBody.velocity = movement * speed;

		rigidBody.position = new Vector3(
			Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
		);

		rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
}
