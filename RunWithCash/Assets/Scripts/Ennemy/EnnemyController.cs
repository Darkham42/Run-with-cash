using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class EnnemyController : MonoBehaviour {

	private ControllerMove controller;
	public Transform target;
	int lifePoint = 1;
	bool inScreen;
	bool alive = true;
	float speed;

	public float maxRectif = 15;
	public float maxSpeed = 100;

	void Start () {
		controller = GetComponent<ControllerMove>();
	}

	void Update () {
		changeDirection ();
		Debug.DrawLine (transform.FindChild("Cube").transform.position, controller.reference.transform.position) ;
		if (lifePoint <= 0) {
			alive = false;
		}
	}

	void changeDirection () {
		float angleBetweenTarget = Vector3.Angle (transform.forward, target.position);
		if (angleBetweenTarget >= Mathf.Abs (2.0f)) {
			float onRight = Vector3.Dot (transform.right, target.position);
<<<<<<< HEAD
			Debug.Log ("onRight : " + Mathf.Sign(onRight));
			float turnSpeed = (Mathf.Abs (angleBetweenTarget) < maxRectif) ? Mathf.Abs (angleBetweenTarget) : maxRectif;
			controller.Turn (Mathf.Sign(onRight), turnSpeed);
=======
			controller.Turn (maxRectif * Mathf.Abs(onRight), 1);

		} else {
			controller.Turn(0, 1);
>>>>>>> origin/master
		}
	}

	void getHit (Projectile projectile) {
		lifePoint -= projectile.power;
	}
}
