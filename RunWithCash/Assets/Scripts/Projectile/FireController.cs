using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Gun))]
public class FireController : MonoBehaviour {

	private ControllerMove controller;
	private Gun gun;
	public Projectile item;
	float RayDistance; 
	public float maxRange = 8;
	public float rectifSpeedCar = 30;
	int layerMask;

	// Use this for initialization
	void Start () {
		controller = GetComponent<ControllerMove>();
		gun = GetComponent<Gun> ();
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
		if (Input.GetButtonDown ("Fire2") ^ Input.GetButtonDown ("Fire3")) {
			int fireDirection = Input.GetButtonDown ("Fire2") ? 1 : -1;
			gun.Fire (fireDirection);
		}
	}
}