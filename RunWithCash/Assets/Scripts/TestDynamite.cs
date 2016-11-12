using UnityEngine;
using System.Collections;

public class TestDynamite : MonoBehaviour {

	public Projectile item;


	
	// Update is called once per frame
	void Update () {

		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
		float rayDistance;

		if (Input.GetButtonDown ("Fire1")) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (groundPlane.Raycast (ray, out rayDistance)) {
				Vector3 point = ray.GetPoint (rayDistance);
				Debug.DrawLine (ray.origin, point, Color.black, 2f);
				// Instance du projectile + définition distance.
				Projectile newProjectile = Instantiate (item, transform.position, transform.rotation) as Projectile;
				newProjectile.distance = Vector3.Distance (Vector3.zero, point);
			}
		}
	}
}