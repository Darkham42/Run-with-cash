using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Animator))]
public class CarBehaviour : MonoBehaviour {

//	Rigidbody rb;
	Animator anim;
	float speed = 0;
	float direction = 0;

	public float actualSpeed;

	void Start () {
//		rb = GetComponent<Rigidbody > ();
		anim = GetComponent<Animator> ();
	}

	void Update () {
		transform.Translate (transform.InverseTransformDirection (Vector3.forward) * speed * Time.deltaTime);
		transform.Translate (transform.InverseTransformDirection (Vector3.right) * direction * Time.deltaTime);

		anim.SetFloat ("Direction", direction); // Rotation anim

		actualSpeed = speed; // Show speed in inspector for DEBUG
	}

	public void Speed (float _speed) {
		speed = _speed;
	}

	public void Turn (float _direction) {
		direction = _direction;
	}

	public void GetHit (bool hitted) {
		anim.SetBool ("GetHit", hitted);
	}

	public void Die () {
		Debug.Log ("Mort !!!");
	}

	void OnTriggerEnter(Collider other) {
		if (this.tag == "Player") {
			switch (other.tag) {
			case "CashBonus":
				Debug.Log ("Cash ramassé");
				break;
			}
		}
	}
}
