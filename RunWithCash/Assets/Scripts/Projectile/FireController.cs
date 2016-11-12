using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour {

	private ControllerMove controller;
	public Projectile item;
	float RayDistance; 
	public float maxRange = 8;
	public float rectifSpeedCar = 30;
	int layerMask;

	// Use this for initialization
	void Start () {
		controller = GetComponent<ControllerMove>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			RaycastHit hit;
			Projectile newProjectile = Instantiate (item, transform.position, transform.rotation) as Projectile;

			newProjectile.GetComponent<Rigidbody> ().AddForce (Vector3.forward * rectifSpeedCar, ForceMode.Impulse);
			newProjectile.distance = maxRange;
			if (Physics.Raycast (transform.position, Vector3.back, out hit, maxRange)) {
				newProjectile.distance = hit.distance;
			}
		}
	}
}