using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class Projectile : MonoBehaviour {

	Rigidbody rb;
	public int power;
	float weight;
	Vector3 direction;
	float strenght;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		Vector3 throwVector = new Vector3 (strenght * Mathf.Cos(45),strenght * Mathf.Sin(45), 0);
		transform.Rotate(direction);
		rb.AddForce(throwVector);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
