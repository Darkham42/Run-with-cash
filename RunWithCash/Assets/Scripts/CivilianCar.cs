using UnityEngine;
using System.Collections;

public class CivilianCar : MonoBehaviour {

    public bool Touched = false;

    private float timer = 0.0f;
    GameObject player;
    GameManager gm;

	void Start () {
        player = GameObject.Find("Car");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	void Update () {
	    if (Touched)
        {
            timer += Time.deltaTime;
        }

        if (timer > 5 ||
            player.transform.position.z - transform.position.z > 50)
        {
            Destroy(this.gameObject);
        }
	}
}
