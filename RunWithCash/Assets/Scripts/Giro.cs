using UnityEngine;
using System.Collections;

public class Giro : MonoBehaviour {

    GameObject LeftLight;
    GameObject RightLight;
    float timer = 0;

	// Use this for initialization
	void Start () {
        LeftLight = transform.FindChild("GyroBlue").gameObject;
        RightLight = transform.FindChild("GyroRed").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 0.5f) {
            LeftLight.SetActive(!LeftLight.activeSelf);
            RightLight.SetActive(!RightLight.activeSelf);
            timer = 0;
        }
	}
}
