using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class EnemyController : MonoBehaviour {

	private ControllerMove controller;
	private Transform target;
	bool inScreen;
	bool alive = true;
	public float normalSpeed = 35f;
	public float attackSpeed = 35f;
	public float recoverySpeed = 20f;
	public float timeAttack = 1f;
	public float distanceView = 6f;
	public bool canAttack = true;
	public bool isAttacking = false;
    public GameObject Explosion;

    GameManager gm;
	public float maxRectif = 20;

	void Start () {
		controller = GetComponent<ControllerMove>();
		canAttack = false;
		controller.speed = normalSpeed;
        target = GameObject.Find("Car").transform;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void Update () {

		// Si devant le joueur
		if ((target.position.z - transform.position.z) <= -1) {
			canAttack = false;
			controller.speed = recoverySpeed;
		}

		// Si au niveau du joueur
		if ((target.position.z - transform.position.z) <= 1.5) {
			if (canAttack) {
				canAttack = false;
				StartCoroutine (Attacking ());
			}
		}

		// Si en retard après attack raté
		if ((target.position.z - transform.position.z) >= 13) {
			controller.speed = normalSpeed;
			canAttack = true;
		}

		// Esquiver voiture civile
		RaycastHit hit;
		if (Physics.BoxCast (transform.position, new Vector3 (2.5f, 2.5f, 2.5f), Vector3.forward, out hit, new Quaternion (0f, 0f, 0f, 0f), distanceView)) {
			if (!isAttacking) {
				if (hit.collider.CompareTag ("CivilianCar")) {
					Debug.Log ("Vehicule civile droit devant.");
				}
			}
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
                t.CarGameObject.transform.parent.GetComponent<ControllerMove>().Turn(-0.3f, 50);
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

	void straightenUp () {
		float direction = Mathf.Sign (transform.rotation.y) * -1;
		controller.Turn (direction, maxRectif);
	}

	public void getHit() {
		die ();
	}

	public void die() {
        GameObject tmp = GameObject.Instantiate(Explosion) as GameObject;
        tmp.transform.position = this.transform.position;
		GameObject.Find("TerrainPoolManager").GetComponent<TerrainManager>().nbrCops--;
		Destroy(this.gameObject);
	}

	IEnumerator Attacking () {
		Debug.Log ("début attaque");
		isAttacking = true;
		controller.speed = attackSpeed;
		changeDirection();
		yield return new WaitForSeconds(timeAttack);
		Debug.Log ("fin attaque");
		isAttacking = false;
		controller.speed = recoverySpeed;
		straightenUp ();
	}
}
