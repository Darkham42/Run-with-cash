using UnityEngine;
using System.Collections;

public class ControllerMove : MonoBehaviour {

    // GameObject vide référence que la voiture suivra
    public Transform reference;
    public float speed;
    public float speedTurn;
    public float turnLimit;
    public float speedMax;
    public float speedMin;

    private GameObject lookAtPoint;
    private float lookAtPointOffset;
    private float boostSpeed = 0;
    private float brakeSpeed = 0;

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

    public void Start() {
        lookAtPoint = transform.FindChild("LookAtPoint").gameObject;
        lookAtPoint.transform.parent = null;
    }

    public void FixedUpdate() {
        turning = false;
        MoveForward();
        transform.LookAt(reference);
    }

    public void MoveForward() {
        transform.position += (transform.forward * speed * Time.fixedDeltaTime) + transform.forward * boostSpeed - transform.forward * brakeSpeed;
        lookAtPoint.transform.position = transform.position + new Vector3(lookAtPointOffset, 0, 7);
    }

    public void Turn(float rotation, float speed) {
        Vector3 turnVector = new Vector3(rotation * (speedTurn * speed) * Time.fixedDeltaTime, 0, 10);

        if (Mathf.Abs(rotation) < 0.3f) {
            if (lookAtPointOffset > 0) {
                lookAtPointOffset -= speedTurn * Time.fixedDeltaTime;
            }

            if (lookAtPointOffset < 0) {
                lookAtPointOffset += speedTurn * Time.fixedDeltaTime;
            }
        }
        
        lookAtPointOffset += turnVector.x;
        
        if (lookAtPointOffset > turnLimit) {
            lookAtPointOffset = turnLimit;
        }

        if (lookAtPointOffset < -turnLimit) {
            lookAtPointOffset = -turnLimit;
        }

    }

    public void Speed(float value) {
        boostSpeed += value;
        if (boostSpeed > speedMax)
            boostSpeed = speedMax;

    }

    public void StopSpeed(float value) {
        boostSpeed -= value;
        if (boostSpeed < 0)
            boostSpeed = 0;
    }

    public void Brake(float value) {
        brakeSpeed += value;
        if (brakeSpeed > speedMin)
            brakeSpeed = speedMin;
    }

    public void StopBreak(float value) {
        brakeSpeed -= value;
        if (brakeSpeed < 0)
            brakeSpeed = 0;
    }


    bool turning = false;
}