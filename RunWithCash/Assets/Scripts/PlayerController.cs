using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private ControllerMove controller;
    private float offSet;
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
        //if (Mathf.Abs(rotation) > 0.1f) {

        bool touched = transform.FindChild("Cube").GetComponent<TestPhysic>().Touched;

        if (!touched)
            controller.Turn(rotation, 1.5f);
        else
        {
            if (transform.position.x > 0)
            {
                controller.Turn(-0.3f, 50);
            }
            else
            {
                controller.Turn(0.3f, 50);
            }
        }
        //}

        // La caméra suit de façon fice le véhicule
        camera.transform.position = new Vector3(offSet, 5.5f, transform.FindChild("Cube").position.z - 20);
        camera.transform.LookAt(transform.FindChild("Cube"));

    }

}
