using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private ControllerMove controller;
    private float offSet;
    public float speedMax;
	public float speedMin;
	public bool speedUp;
	public bool speedDown;
    public Camera camera;

    public GameObject pe;

//NUGameObject leftTrail;
//NUGameObject rightTrail;
    GameManager gm;

    [HideInInspector]
    public float timerInvincible = 0.0f;

    public void Start()
    {
        controller = GetComponent<ControllerMove>();
        offSet = transform.position.x;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
//nu    leftTrail = transform.FindChild("trailLeft").gameObject;
//nu    rightTrail = transform.FindChild("trailRight").gameObject;
//nu	ActivateTrails(false);
    }
	/*
    void ActivateTrails(bool active)
    {
        //leftTrail.SetActive(active);
        //rightTrail.SetActive(active);
    }
	*/
    void AddInvicibility()
    {
        if (timerInvincible <= 0.0f)
        {
            timerInvincible = 2.0f;
        }
    }

    GameObject ptc = null;

    public void SpawnParticles()
    {
        if (ptc)
        {
            return;
        }
        GameObject tmp = GameObject.Instantiate(pe) as GameObject;
        tmp.transform.parent = this.transform;
        tmp.transform.localPosition = new Vector3(0, -0.46f, -2.4f);
        ptc = tmp;
        StartCoroutine(deleteParticle(tmp));
    }

    IEnumerator deleteParticle(GameObject p)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(p);
        ptc = null;
    }

    public void FixedUpdate()
    {
        // Déplacement Horizontal du véhicule
        float rotation = Input.GetAxis("Horizontal");

        GameObject cube = transform.FindChild("Cube").gameObject;

        bool touched = cube.GetComponent<TestPhysic>().Touched;
        bool carTouched = cube.GetComponent<TestPhysic>().CarTouched;
        bool copTouched = cube.GetComponent<TestPhysic>().CopTouched;
        bool cashTouched = cube.GetComponent<TestPhysic>().TouchedCashBonus;
        bool ammoTouched = cube.GetComponent<TestPhysic>().TouchedAmmoBonus;
        bool dynamiteTouched = cube.GetComponent<TestPhysic>().TouchedDynamiteBonus;

        if ((!touched && !carTouched && !copTouched))
        {
            controller.Turn(rotation, 1.5f);
			/*
            if (Mathf.Abs(rotation) > 0.1f)
            {
                ActivateTrails(true);
            }
            else
            {
                ActivateTrails(false);
            }
            */
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
            gm.RemoveCash(5);
            AddInvicibility();
        }
        else if (copTouched)
        {
            if (cube.GetComponent<TestPhysic>().CopGameObject && transform.position.x > cube.GetComponent<TestPhysic>().CopGameObject.transform.parent.position.x)
            {
                controller.Turn(0.3f, 50);
                cube.GetComponent<TestPhysic>().CopGameObject.transform.parent.GetComponent<EnemyController>().getHit();
            }
            else
            {
                controller.Turn(-0.3f, 50);
                if (cube.GetComponent<TestPhysic>().CopGameObject)
                    cube.GetComponent<TestPhysic>().CopGameObject.transform.parent.GetComponent<EnemyController>().getHit();
            }
            cube.GetComponent<TestPhysic>().CopTouched = false;
            gm.RemoveCash(10);
            AddInvicibility();
        }
        else
        {
            gm.RemoveCash(1);
            if (transform.position.x > 0)
            {
                controller.Turn(-0.3f, 50);
            }
            else
            {
                controller.Turn(0.3f, 50);
            }
            AddInvicibility();
        }

        if (cashTouched)
        {
            gm.AddCash(cube.GetComponent<TestPhysic>().TouchedCashGameObject.GetComponent<CashBonus>().CashEarned);
            gm.PlaySoundMulti(0);
            Destroy(cube.GetComponent<TestPhysic>().TouchedCashGameObject.transform.parent.gameObject);
            cube.GetComponent<TestPhysic>().TouchedCashBonus = false;
        }

        if (ammoTouched)
        {
            if (gm.ammo < 99)
                gm.ammo++;
            gm.PlaySoundMulti(0);
            Destroy(cube.GetComponent<TestPhysic>().TouchedAmmoGameObject.transform.parent.gameObject);
            cube.GetComponent<TestPhysic>().TouchedAmmoBonus = false;
        }

        if (dynamiteTouched)
        {
            if (gm.dynamite < 99)
                gm.dynamite++;
            gm.PlaySoundMulti(0);
            Destroy(cube.GetComponent<TestPhysic>().TouchedDynamiteGameObject.transform.parent.gameObject);
            cube.GetComponent<TestPhysic>().TouchedDynamiteBonus = false;
        }

        // Accélération du véhicule
        bool up = Input.GetKey(KeyCode.Z);
        float right = Input.GetAxis("Trigger Right");
		if (right > 0.1f) {
			controller.Speed (right * speedMax * Time.fixedDeltaTime * gm.GamePaused);
			speedUp = true;
		} else if (up) {
			controller.Speed(1f * speedMax * Time.fixedDeltaTime * gm.GamePaused);
			speedUp = true;
        } else {
			controller.StopSpeed(0.5f * speedMax * Time.fixedDeltaTime * gm.GamePaused);
			speedUp = false;
		}

        // Décélération du véhicule
        bool down = Input.GetKey(KeyCode.S);
        float left = Input.GetAxis("Trigger Left");
		if (left > 0.1f) {
			controller.Brake (left * speedMin * Time.fixedDeltaTime * gm.GamePaused);
			speedDown = true;
		} else if (down) {
			controller.Brake (1f * speedMax * Time.fixedDeltaTime * gm.GamePaused);
			speedDown = true;
		} else {
			controller.StopBreak (0.5f * speedMin * Time.fixedDeltaTime * gm.GamePaused);
			speedDown = false;
		}

        // La caméra suit de façon fixe le véhicule
        Vector3 pos = transform.FindChild("Cube").position;
        pos.y = 7.5f;
        pos.z -= 20;
        camera.transform.position = Vector3.Lerp(camera.transform.position, pos, 0.1f);
        camera.transform.LookAt(transform.FindChild("Cube"));

        if (transform.FindChild("Cube").GetComponent<TestPhysic>().DeathWall)
        {
            GetComponent<ControllerMove>().enabled = false;
            gm.GameOver = true;
        }

        timerInvincible -= Time.fixedDeltaTime * gm.GamePaused;
        if (timerInvincible <= 0.0f)
        {
            timerInvincible = 0.0f;
        }
    }

}
