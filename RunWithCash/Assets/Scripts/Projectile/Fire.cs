using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	void Start () {
//		StartCoroutine (Dying ());
	}

	IEnumerator Dying() {
		yield return new WaitForSeconds(1f);
		Destroy (this.gameObject);
	}
}
