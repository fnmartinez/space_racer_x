using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float speed;

	void Start() {
		GetComponent<Rigidbody>().velocity = GetComponent<Transform>().forward * speed;
	}
}
