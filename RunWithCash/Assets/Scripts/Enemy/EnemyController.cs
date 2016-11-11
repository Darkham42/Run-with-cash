using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class EnemyController : MonoBehaviour {

	private ControllerMove controller;
	public Transform target;
	int lifePoint = 1;
	bool inScreen;
	bool alive = true;
	float speed;

	public float maxRectif = 8;
	public float maxSpeed = 100;

	void Start () {
		controller = GetComponent<ControllerMove>();
	}

	void Update () {

		// Si au niveau du joueur
		if ((target.position.z - transform.position.z) <= 1.5) {
			changeDirection ();
		}
		if (lifePoint <= 0) {
			alive = false;
		}
	}

	void changeDirection () {
		float angleBetweenTarget = Vector3.Angle (transform.forward, target.position - transform.position);
		if (angleBetweenTarget >= Mathf.Abs (2.0f)) {
			float onRight = Vector3.Dot (transform.right, target.position - transform.position);
			controller.Turn (Mathf.Sign(onRight), maxRectif);
		}
	}

	void getHit (int dammage) {
		lifePoint -= dammage;
	}
}
