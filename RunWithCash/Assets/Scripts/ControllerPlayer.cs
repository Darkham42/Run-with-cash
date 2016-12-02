using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CarBehaviour))]
public class ControllerPlayer : MonoBehaviour {

	float speedUp;
	float turn;
	float turnSpeed = 5;
	float crusingSpeed = 0;
	CarBehaviour carBehaviour;

	// Use this for initialization
	void Start () {
		carBehaviour = GetComponent <CarBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
		speedUp = Input.GetAxisRaw ("Vertical") * 5;
		turn = Input.GetAxisRaw ("Horizontal");

		carBehaviour.Speed (speedUp + crusingSpeed);
		carBehaviour.Turn (turn * turnSpeed);
	}
}
