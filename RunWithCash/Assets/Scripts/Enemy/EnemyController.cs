using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class EnemyController : MonoBehaviour {

	private ControllerMove controller;
	private Transform target;
	bool inScreen;
	bool alive = true;
	float speed;

    GameManager gm;
	public float maxRectif = 8;

	void Start () {
		controller = GetComponent<ControllerMove>();
        target = GameObject.Find("Car").transform;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void Update () {

		// Si au niveau du joueur
		if ((target.position.z - transform.position.z) <= 1.5) {
			changeDirection();
		}
        // Destruction de la voiture de Police
		if (target.position.z - transform.position.z > 50) {
			die();
        }

        TestPhysic t = GetComponentInChildren<TestPhysic>();

        //Debug.Log(GetComponentInChildren<TestPhysic>().Touched);
        //Debug.Log(GetComponentInChildren<TestPhysic>().CopTouched);
        if (t.CarTouched)
        {
            getHit();
            if (transform.position.x > t.CarGameObject.transform.parent.position.x)
            {

            }
            else
            {
                t.CarGameObject.transform.parent.GetComponent<ControllerMove>().Turn(0.3f, 50);
            }
        }
        if (t.Touched &&
            !t.CopTouched &&
            !t.CarTouched)
        {
            getHit();
        }
        if (t.CopTouched)
        {
            getHit();
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
		controller.speed = 0;
	}

	public void die() {
		Destroy(this.gameObject);
		GameObject.Find("TerrainPoolManager").GetComponent<TerrainManager>().nbrCops--;
	}
}
