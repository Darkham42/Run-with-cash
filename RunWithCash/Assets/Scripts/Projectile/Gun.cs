using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	float direction = 1; // possitif = droite, negatif = gauche
	RaycastHit hit;

	void Start () {
	}

	public void Fire() {
		if (Physics.Raycast (transform.position, transform.position + Vector3.right * direction, out hit)) {
			if (hit.collider.CompareTag ("Copcar")) {
				StartCoroutine (Animation ());
				hit.collider.gameObject.GetComponent <EnemyController> ().die ();
			}
		}
	}

	IEnumerator Animation() {
		Debug.Log ("Pew ! Pew ! Pew !");
		yield return new WaitForSeconds(1f);
		Debug.Log ("Reloading !");
	}

}
