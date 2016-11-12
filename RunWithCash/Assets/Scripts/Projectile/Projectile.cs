using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class Projectile : MonoBehaviour {

	// Après avoir instantié, définir la distance jusqu'à la cible.

	Rigidbody rb;
	public float distance;
	float angle = 20f;
	Vector3 trajectory;
	Vector3 debugTarget;
    GameManager gm;

	void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		debugTarget = new Vector3 (transform.position.x, 0, transform.position.z - distance);
		rb = GetComponent<Rigidbody> ();
		trajectory = Vector3.Slerp (Vector3.back, Vector3.up, angle / 90f);
		rb.AddForce(trajectory * setThrow (distance), ForceMode.Impulse);
		rb.AddTorque (Vector3.left * 800);
	}

	void Update () {
		if (transform.position.y < 0) {
			Destroy (this.gameObject);
		}
//		Debug.DrawLine (transform.position, debugTarget, Color.green);
	}

	// distance = écart en X avec la cible à l'arrière
	float setThrow (float _distance) {
		float g =  Physics.gravity.y * -1;
		float o = angle;
		float x = _distance;
		float v = (Mathf.Sqrt (g) * Mathf.Sqrt (x) * Mathf.Sqrt ((Mathf.Tan (o) * Mathf.Tan (o)) + 1f)) / Mathf.Sqrt (2f * Mathf.Tan (o) - (2f * g * 0f) / x);
		return v;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("CopCar")) {
			Debug.Log ("Cop touche par dynamite");
            gm.PlaySoundMulti(1);
			Transform hitParent = other.gameObject.transform.parent as Transform;
			hitParent.GetComponent <EnemyController> ().die ();
			Destroy (this.gameObject);
		}
		/*
		if (other.CompareTag ("Ground")) {
			Destroy (this.gameObject);
		}
		*/
	}
}