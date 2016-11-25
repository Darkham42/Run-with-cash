using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CarBehaviour))]
public class ControllerPlayer : MonoBehaviour {

	float speedUp;
	float turn;
	CarBehaviour carBehaviour;

	// Use this for initialization
	void Start () {
		carBehaviour = GetComponent <CarBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
		speedUp = Input.GetAxisRaw ("Vertical");
		turn = Input.GetAxisRaw ("Horizontal");

		carBehaviour.SpeedUp (speedUp);
		carBehaviour.Turn (turn);
	}
}
