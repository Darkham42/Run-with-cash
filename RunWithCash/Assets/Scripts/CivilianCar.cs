using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CivilianCar : MonoBehaviour {

    public bool Touched = false;
    public List<Material> Colors;

    private float timer = 0.0f;
    GameObject player;
//nuGameManager gm;

	void Start () {
        player = GameObject.Find("Car");
//nu	gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        int rnd = Random.Range(0, 3);
        transform.FindChild("Cube").GetComponent<MeshRenderer>().material = Colors[rnd];
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
