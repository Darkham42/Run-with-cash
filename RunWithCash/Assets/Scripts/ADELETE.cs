using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class NewBehaviourScript : MonoBehaviour {
	public Transform target;
	int lifePoint = 1;
	bool inScreen;
	bool alive = true;
	float speed;

	public float maxRectif = 15;
	public float maxSpeed = 100;

	void Update () {
		if (lifePoint <= 0) {
			alive = false;
		}
	}

	void changeDirection () {
		float angleBetweenTarget = Vector3.Angle (transform.forward, transform.forward);
		if (angleBetweenTarget != Mathf.Abs(5.0f)) {
			// Rectifier la direction
		}

	}

	void getHit (Projectile projectile) {
		lifePoint -= projectile.power;
	}
}