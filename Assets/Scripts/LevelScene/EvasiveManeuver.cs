using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Range {
	public float min, max;
}

public class EvasiveManeuver : MonoBehaviour {
	
	public float dodge;
	public float smoothing;
	public float speedMultiplier;
	public float tilt;
	public Range startWait;
	public Range maneuverTime;
	public Range maneuverWait;
	public Boundary boundary;

	private float targetManeuver;
	private float currentSpeed;
	private Rigidbody rigidBody;
	private GameController gameController;

	void Start() {
		rigidBody = GetComponent<Rigidbody>();
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			this.gameController = gameControllerObject.GetComponent<GameController>();
		} else {
			Debug.Log("Cannot find GameController");
		}
		StartCoroutine(Evade());
	}

	IEnumerator Evade() {
		yield return new WaitForSeconds(Random.Range(startWait.min, startWait.max));
		while (true) {
			targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds(Random.Range(maneuverTime.min, maneuverTime.max));
			targetManeuver = 0;
			yield return new WaitForSeconds(Random.Range(maneuverWait.min, maneuverWait.max));
		}
	}

	void FixedUpdate() {
		float newManeuver = Mathf.MoveTowards(rigidBody.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		currentSpeed = gameController.Speed * speedMultiplier;
		rigidBody.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
		rigidBody.position = new Vector3(
			Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
		);
		rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * -tilt);
	}
}
