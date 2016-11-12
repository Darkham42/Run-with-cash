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
		StartCoroutine (Animation (direction));
		if (Physics.Raycast (transform.position, Vector3.right * Mathf.Sign(direction), out hit)) {
			if (hit.collider.CompareTag ("CopCar")) {
				Transform hitParent = hit.collider.gameObject.transform.parent as Transform;
				hitParent.GetComponent <EnemyController> ().die ();
			}
		}
	}

	IEnumerator Animation(float _direction) {
		if (_direction >= 0) {
			Debug.Log ("Pew ! Pew ! Pew !");
		} else {
			Debug.Log ("Pan ! Pan ! Pan !");
		}
		yield return new WaitForSeconds(1f);
	}

}
