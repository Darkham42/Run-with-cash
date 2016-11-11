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
	}

	void Update () {
		changeDirection ();
		if (lifePoint <= 0) {
			alive = false;
		}
	}

	void changeDirection () {
		float angleBetweenTarget = Vector3.Angle (transform.forward, target.position);
		if (angleBetweenTarget >= Mathf.Abs (5.0f)) {
			float onRight = Vector3.Dot (transform.right, target.position);
			controller.Turn (maxRectif * Mathf.Abs(onRight), 1);

		} else {
			controller.Turn(0, 1);
		}
	}

	void getHit (Projectile projectile) {
		lifePoint -= projectile.power;
	}
}
