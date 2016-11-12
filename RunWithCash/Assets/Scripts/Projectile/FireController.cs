using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Gun))]
public class FireController : MonoBehaviour {

	private ControllerMove controller;
	private Gun gun;
	public Projectile item;
	float RayDistance; 
	public float maxRange = 15;
	public float rectifSpeedCar = 30;
	int layerMask;

    GameManager gm;

	// Use this for initialization
	void Start () {
		controller = GetComponent<ControllerMove>();
		gun = GetComponent<Gun> ();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (transform.position, Vector3.back * maxRange, Color.red);
		/*
		RaycastHit hit2;
		if (Physics.BoxCast (transform.position, new Vector3 (1, 1, 1), Vector3.back, out hit2, new Quaternion (0, 0, 0, 0), maxRange)) {
			Debug.Log ("A toi de tirer !");
			Debug.Break ();
		}
		*/
		if (Input.GetButtonDown ("Fire1")) {
			RaycastHit hit;
			Projectile newProjectile = Instantiate (item, transform.position, transform.rotation) as Projectile;

            gm.PlaySoundMulti(2);

			newProjectile.GetComponent<Rigidbody> ().AddForce (Vector3.forward * rectifSpeedCar, ForceMode.Impulse);
			newProjectile.distance = maxRange;
			if (Physics.BoxCast (transform.position, new Vector3 (1, 1, 1), Vector3.back, out hit, new Quaternion (0, 0, 0, 0), maxRange)) {
				Debug.Log ("Police a portee.");
				newProjectile.distance = hit.distance;
			}
		}
		if (Input.GetButtonDown ("Fire2") ^ Input.GetButtonDown ("Fire3")) {
			int fireDirection = Input.GetButtonDown ("Fire2") ? 1 : -1;
			gun.Fire (fireDirection);
		}
	}
}