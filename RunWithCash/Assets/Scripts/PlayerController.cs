using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private ControllerMove controller;
    private float offSet;
    public float speedMax;
    public float speedMin;
    public Camera camera;

    GameManager gm;

    public void Start()
    {
        controller = GetComponent<ControllerMove>();
        offSet = transform.position.x;

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void FixedUpdate()
    {
        // Déplacement Horizontal du véhicule
        float rotation = Input.GetAxis("Horizontal");

        GameObject cube = transform.FindChild("Cube").gameObject;

        bool touched = cube.GetComponent<TestPhysic>().Touched;
        bool carTouched = cube.GetComponent<TestPhysic>().CarTouched;
        bool copTouched = cube.GetComponent<TestPhysic>().CopTouched;

        if (!touched && !carTouched && !copTouched) {
            controller.Turn(rotation, 1.5f);
        }
        else if (carTouched)
        {
            if (transform.position.x > cube.GetComponent<TestPhysic>().CarGameObject.transform.parent.position.x)
            {
                controller.Turn(0.3f, 50);
                cube.GetComponent<TestPhysic>().CarGameObject.transform.parent.GetComponent<ControllerMove>().Turn(-0.3f, 50);
                cube.GetComponent<TestPhysic>().CarGameObject.transform.parent.GetComponent<CivilianCar>().Touched = true;
            }
            else
            {
                controller.Turn(-0.3f, 50);
                cube.GetComponent<TestPhysic>().CarGameObject.transform.parent.GetComponent<ControllerMove>().Turn(0.3f, 50);
                cube.GetComponent<TestPhysic>().CarGameObject.transform.parent.GetComponent<CivilianCar>().Touched = true;
            }
        }
        else if (copTouched)
        {
            if (transform.position.x > cube.GetComponent<TestPhysic>().CopGameObject.transform.parent.position.x)
            {
                controller.Turn(0.3f, 50);
                cube.GetComponent<TestPhysic>().CopGameObject.transform.parent.GetComponent<EnemyController>().getHit();
            }
            else
            {
                controller.Turn(-0.3f, 50);
                cube.GetComponent<TestPhysic>().CopGameObject.transform.parent.GetComponent<EnemyController>().getHit();
            }
        }
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

        // Accélération du véhicule
        float right = Input.GetAxis("Trigger Right");
        if (right > 0.1f)
            controller.Speed(right * speedMax * Time.fixedDeltaTime * gm.GamePaused);
        else
        {
            controller.StopSpeed(0.5f * speedMax * Time.fixedDeltaTime * gm.GamePaused);
        }

        // Décélération du véhicule
        float left = Input.GetAxis("Trigger Left");
        if (left > 0.1f)
            controller.Brake(left * speedMin * Time.fixedDeltaTime * gm.GamePaused);
        else
            controller.StopBreak(0.5f * speedMin * Time.fixedDeltaTime * gm.GamePaused);

        // La caméra suit de façon fixe le véhicule
        Vector3 pos = transform.FindChild("Cube").position;
        pos.y = 7.5f;
        pos.z -= 20;
        camera.transform.position = Vector3.Lerp(camera.transform.position, pos, 0.1f);
        camera.transform.LookAt(transform.FindChild("Cube"));

        if (transform.FindChild("Cube").GetComponent<TestPhysic>().DeathWall)
        {
            SceneManager.LoadScene("menu");
            gm.GameOver = true;
        }

    }

}
