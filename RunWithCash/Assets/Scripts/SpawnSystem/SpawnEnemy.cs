using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {
	float[] posCars = new float[2];
//	List<float> posCars2;

	void Start () {
		posCars [0] = -3;
		posCars [1] = 6;
		Debug.Log (posCars[0] + " / " + posCars[1]);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.R))
			Debug.Log ("Test choose option : " + choosePosition (-10f, 10f));
	}

	float choosePosition (float min, float max) {
		float rand = Random.Range(min, max - posCars.Length * 2) ;
		Debug.Log ("Rand : " + rand);
		foreach (float car in posCars) {
			if (rand > car) {
				rand += 2;
			} else {
				break;
			}
		}
		return rand;
	}
}
