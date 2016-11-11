using UnityEngine;
using System.Collections;

public class ControllerMove : MonoBehaviour {

    // GameObject vide référence que la voiture suivra
    public Transform reference;
    public float speed;
    public float speedTurn;

    /*
    private Vector3 moveDirection = Vector3.zero;
    // Update is called once per frame
    void Update () {
        CharacterController controller = GetComponent<CharacterController>();

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        controller.Move(moveDirection * Time.deltaTime);
    }
    */

    public void FixedUpdate() {
        MoveForward();
        transform.LookAt(reference);
    }

    public void MoveForward() {
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    public void Turn(float rotation) {
        reference.transform.localPosition = new Vector3(rotation * speedTurn * Time.deltaTime, 0, 10);
    }

    public void Speed() {

    }

    public void Brake() {

    }

}