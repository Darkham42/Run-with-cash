using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Animator))]
public class CarBehaviour : MonoBehaviour {

	Rigidbody rb;
	Animator anim;
	float speed = 0;
	float crusingSpeed = 15;
	float direction = 0;
	float maxSpeed = 5; // Vitesse du véhicule
	float maxTurn = 5; // Vitesse staff horizontal du véhicule
	float turnSpeed = 60f; // dégrée par seconde
	float maxAngle = 30f; // en degrée
	float speedUp = 0;

	public float actualSpeed;

	void Start () {
		rb = GetComponent<Rigidbody > ();
		anim = GetComponent<Animator> ();
	}

	void Update () {
		speed = crusingSpeed + speedUp;
		transform.Translate (transform.InverseTransformDirection (Vector3.forward) * speed * Time.deltaTime);
		transform.Translate (transform.InverseTransformDirection (Vector3.right) * direction * maxTurn * Time.deltaTime);

		if (direction != 0) {
			Rotate (direction);
		}

		anim.SetFloat ("Direction", direction); // Rotation anim

		actualSpeed = speed; // Show speed in inspector
	}

	void Rotate (float _direction) {
		transform.Rotate (Vector3.up * turnSpeed * _direction * Time.deltaTime);
		float newAngle = transform.rotation.eulerAngles.y <= 180 ? transform.rotation.eulerAngles.y : transform.rotation.eulerAngles.y - 360;
		if (newAngle > maxAngle) {
			transform.eulerAngles = new Vector3 (transform.rotation.eulerAngles.x, maxAngle, transform.rotation.eulerAngles.z);
		}
		if (newAngle < -maxAngle) {
			transform.eulerAngles = new Vector3 (transform.rotation.eulerAngles.x, -maxAngle, transform.rotation.eulerAngles.z);
		}
	}

	public void SpeedUp (float _speed) {
		speedUp = _speed * maxSpeed;
	}

	public void Turn (float _direction) {
		direction = _direction;
	}
}
