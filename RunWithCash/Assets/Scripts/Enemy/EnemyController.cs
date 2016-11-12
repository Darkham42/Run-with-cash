using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class EnemyController : MonoBehaviour {

    bool inScreen;
    bool alive = true;
    float speed;
    public float maxRectif = 8;

    private ControllerMove controller;
	private Transform target;

    GameManager gm;

    void Start () {
		controller = GetComponent<ControllerMove>();
        target = GameObject.Find("Car").transform;
	}

	void Update () {

		// Si au niveau du joueur
		if ((target.position.z - transform.position.z) <= 1.5) {
			changeDirection();
		}
        // Destruction de la voiture de Police
        if (target.position.z - transform.position.z > 50) {
            Destroy(this.gameObject);
            GameObject.Find("TerrainPoolManager").GetComponent<TerrainManager>().nbrCops--;
            gm.RemoveCash(5);
        }
	}

	void changeDirection() {
		float angleBetweenTarget = Vector3.Angle (transform.forward, target.position - transform.position);
		if (angleBetweenTarget >= Mathf.Abs (2.0f)) {
			float onRight = Vector3.Dot (transform.right, target.position - transform.position);
			controller.Turn (Mathf.Sign(onRight), maxRectif);
		}
	}

	public void getHit() {
        GetComponent<ControllerMove>().speed = 0;
	}
}
