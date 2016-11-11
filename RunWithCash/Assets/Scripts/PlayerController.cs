using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private ControllerMove controller;

    public void Start() {
        controller = GetComponent<ControllerMove>();
    }

    public void Update() {
        float rotation = Input.GetAxis("Horizontal");
        if (Mathf.Abs(rotation) > 0.1f) {
            controller.Turn(rotation);
        }
    }
}
