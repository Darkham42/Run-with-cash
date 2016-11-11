using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class Projectile : MonoBehaviour {

	Rigidbody rb;
	public int power;
	public float direction;
	public float strenght;
	float weight;
	Vector3 hTrajectory;
	Vector3 trajectory;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		float radian = direction * Mathf.Deg2Rad;
		hTrajectory = new Vector3 (Mathf.Cos (radian), 0, Mathf.Tan (radian));
		hTrajectory.Normalize ();
		trajectory = Vector3.Slerp (hTrajectory, Vector3.up, 0.5f);
		rb.AddForce(trajectory * strenght, ForceMode.Impulse);
	}

	void Update () {
//		Debug.DrawLine (Vector3.zero, hTrajectory, Color.blue);
//		Debug.DrawLine (Vector3.zero, trajectory, Color.green);
	}
}