using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class EnemyController : MonoBehaviour {

	private ControllerMove controller;
	private Transform target;
	public float normalSpeed;
	public float attackSpeed;
	public float recoverySpeed;
	public float timeAttack = 1f;
	public bool canAttack = true;
	public bool isAttacking = false;
	public float speedTarget;
    public GameObject Explosion;

//NUGameManager gm;
	public float maxRectif = 20;

	void Start () {
		controller = GetComponent<ControllerMove>();
		canAttack = false;
		controller.speed = normalSpeed;
        target = GameObject.Find("Car").transform;
//NU	gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void Update () {
		
		speedTarget = 
			1 +
			target.GetComponent<ControllerMove> ().boostSpeed -
			target.GetComponent<ControllerMove> ().brakeSpeed;
		
		// si joueur accelère
		if (canAttack)
			controller.speed = normalSpeed * speedTarget;

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
			controller.speed = normalSpeed * speedTarget;
			canAttack = true;
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
            //if (transform.position.x > t.CopGameObject.transform.parent.position.x)
            //{
            //    t.CopGameObject.transform.parent.GetComponent<ControllerMove>().Turn(-0.3f, 50);
            //}
            //else
            //{
            //    t.CopGameObject.transform.parent.GetComponent<ControllerMove>().Turn(0.3f, 50);
            //}
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
		StartCoroutine (dying ());
	}

	IEnumerator dying () {
		yield return new WaitForSeconds (0.1f);
		die ();
	}

	public void die() {
        GameObject tmp = GameObject.Instantiate(Explosion) as GameObject;
        tmp.transform.position = this.transform.position;
		GameObject.Find("TerrainPoolManager").GetComponent<TerrainManager>().nbrCops--;
		Destroy(this.gameObject);
	}

	IEnumerator Attacking () {
		isAttacking = true;
		controller.speed = attackSpeed * speedTarget;
		changeDirection();
		yield return new WaitForSeconds(timeAttack);
		isAttacking = false;
		controller.speed = recoverySpeed;
		straightenUp ();
	}
}
