using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private ControllerMove controller;
    private float offSet;
    public float speedMax;
    public float speedMin;
    public Camera camera;

    public void Start()
    {
        controller = GetComponent<ControllerMove>();
        offSet = transform.position.x;
    }

    public void FixedUpdate()
    {
        // Déplacement Horizontal du véhicule
        float rotation = Input.GetAxis("Horizontal");
        bool touched = transform.FindChild("Cube").GetComponent<TestPhysic>().Touched;

        if (!touched)
            controller.Turn(rotation, 1.5f);
        else {
            if (transform.position.x > 0) {
                controller.Turn(-0.3f, 50);
            }
            else {
                controller.Turn(0.3f, 50);
            }
        }

        // Accélération du véhicule
        float right = Input.GetAxis("Trigger Right");
        if (right > 0.1f)
            controller.Speed(right * speedMax * Time.fixedDeltaTime);
        else
            controller.StopSpeed(0.5f * speedMax * Time.fixedDeltaTime);

        // Décélération du véhicule
        float left = Input.GetAxis("Trigger Left");
        if (left > 0.1f)
            controller.Brake(left * speedMin * Time.fixedDeltaTime);
        else
            controller.StopBreak(0.5f * speedMin * Time.fixedDeltaTime);

        // La caméra suit de façon fixe le véhicule
        camera.transform.position = new Vector3(offSet, 5.5f, transform.FindChild("Cube").position.z - 20);
        camera.transform.LookAt(transform.FindChild("Cube"));

    }

}
