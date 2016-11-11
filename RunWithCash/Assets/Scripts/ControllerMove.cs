using UnityEngine;
using System.Collections;

public class ControllerMove : MonoBehaviour {

    public float speed;
    private Vector3 moveDirection = Vector3.zero;

    // Update is called once per frame
    void Update () {
        CharacterController controller = GetComponent<CharacterController>();
      
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        controller.Move(moveDirection * Time.deltaTime);
    }
  
}