using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;
	public float finalTime = 0.0f;
	public float currentTime = 0.0f;

	public int acornCount = 0;

	Matrix4x4 baseMatrix = Matrix4x4.identity;

	private Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		Calibrate ();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Input.acceleration.y);
		move ();
	}

	void FixedUpdate() {
		adjustedAccelerometer ();
		Vector2 movement = new Vector2 (0, Input.acceleration.y * speed);
		if (rb.position.y >= -10.15) {
		rb.velocity = movement;
		}
	}

	void move() {
		rb.position += new Vector2(0.02f, 0);
	}

	public void Calibrate() {
		Quaternion rotate = Quaternion.FromToRotation(new Vector3(0f, 0f, 0f), Input.acceleration);
		Matrix4x4 matrix = Matrix4x4.TRS (Vector3.zero, rotate, new Vector3 (1f, 1f, 0f));
		this.baseMatrix = matrix.inverse;

	}

	public Vector2 adjustedAccelerometer() {
		return this.baseMatrix.MultiplyVector (Input.acceleration);
	}
}
