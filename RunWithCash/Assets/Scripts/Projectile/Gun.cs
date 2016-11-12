using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	float direction = 1; // possitif = droite, negatif = gauche
	RaycastHit hit;

	void Start () {
	}

	public void Fire(int _direction) {
		direction = Mathf.Sign(_direction);
		Debug.Log ("directon : " + direction);
		StartCoroutine (Animation ());
		Debug.DrawRay (transform.position, Vector3.right * Mathf.Sign(direction) * 5, Color.cyan, 2f);
		Debug.Break ();
		if (Physics.Raycast (transform.position, Vector3.right * Mathf.Sign(direction), out hit)) {
			if (hit.collider.CompareTag ("CopCar")) {
				Debug.Log ("Touché !");
				Transform hitParent = hit.collider.gameObject.transform.parent as Transform;
//				hit.collider.gameObject.GetComponent <EnemyController> ().die ();
			}
		}
	}

	IEnumerator Animation() {
		Debug.Log ("Pew ! Pew ! Pew !");
		yield return new WaitForSeconds(1f);
	}

}
