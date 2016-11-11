using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private ControllerMove controller;
    private float offSet;
    public Camera camera;

    public void Start() {
        controller = GetComponent<ControllerMove>();
        offSet = transform.position.x;
    }

    public void FixedUpdate() {
        // Déplacement Horizontal du véhicule
        float rotation = Input.GetAxis("Horizontal");
        if (Mathf.Abs(rotation) > 0.1f) {
            controller.Turn(rotation);
        }

        // La caméra suit de façon fice le véhicule
        camera.transform.position = new Vector3(offSet, 5.5f, transform.position.z - 20);
        camera.transform.LookAt(transform);

    }
}
