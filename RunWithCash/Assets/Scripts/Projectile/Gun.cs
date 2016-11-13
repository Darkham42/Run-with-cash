using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public Fire shoot; 
	float direction = 1; // possitif = droite, negatif = gauche
	RaycastHit hit;

    GameManager gm;

    GameObject leftMuzzle;
    GameObject rightMuzzle;
    void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        leftMuzzle = transform.FindChild("MuzzleLeft").gameObject;
        rightMuzzle = transform.FindChild("MuzzleRight").gameObject;
    }

	public void Fire(int _direction) {
		direction = Mathf.Sign(_direction);
		StartCoroutine (Animation (direction));

        gm.PlaySoundMulti(3);

		if (Physics.Raycast (transform.position + Vector3.back, Vector3.right * Mathf.Sign(direction), out hit)) {
			if (hit.collider.CompareTag ("CopCar")) {
				Transform hitParent = hit.collider.gameObject.transform.parent as Transform;
				hitParent.GetComponent <EnemyController> ().die ();
			}
		}
	}

	IEnumerator Animation(float _direction) {

        if (_direction < 0)
        {
            leftMuzzle.SetActive(true);
        }
        else
        {
            rightMuzzle.SetActive(true);
        }

        Fire gunFire = Instantiate (shoot, this.transform.position, this.transform.rotation, this.transform) as Fire;
		if (_direction >= 0) {
			gunFire.transform.position = this.transform.position + new Vector3 (1.05f, .5f, 0f);
		} else {
			gunFire.transform.position = this.transform.position + new Vector3 (-1.05f, .5f, 0f);
		}
		yield return new WaitForSeconds(1f);
        if (_direction < 0)
        {
            leftMuzzle.SetActive(false);
        }
        else
        {
            rightMuzzle.SetActive(false);
        }
        Destroy (gunFire.gameObject);
	}

}
