using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Gun))]
public class FireController : MonoBehaviour {

	private Gun gun;
	public Projectile item;
	float RayDistance; 
	public float maxRange = 15;
	public float rectifSpeedCar = 30;
	int layerMask;

    GameManager gm;

	// Use this for initialization
	void Start () {
		gun = GetComponent<Gun> ();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (transform.position, Vector3.back * maxRange, Color.red);
        /*
		RaycastHit hit2;
		if (Physics.BoxCast (transform.position, new Vector3 (1, 1, 1), Vector3.back, out hit2, new Quaternion (0, 0, 0, 0), maxRange)) {
			Debug.Log ("A toi de tirer !");
			Debug.Break ();
		}
		*/
        bool dynamite = Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.L);
        bool shootLeft = Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.K);
        bool shootRight = Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.M);

        if (dynamite && gm.GamePaused > 0 && gm.dynamite > 0) {
			RaycastHit hit;
			Projectile newProjectile = Instantiate (item, transform.position, transform.rotation) as Projectile;

            gm.PlaySoundMulti(2);
            gm.dynamite--;
			newProjectile.GetComponent<Rigidbody> ().AddForce (Vector3.forward * rectifSpeedCar, ForceMode.Impulse);
			newProjectile.distance = maxRange;
			if (Physics.BoxCast (transform.position, new Vector3 (1, 1, 1), Vector3.back, out hit, new Quaternion (0, 0, 0, 0), maxRange)) {
				newProjectile.distance = hit.distance;
			}
		}
		if (shootLeft && gm.GamePaused > 0 && gm.ammo > 0)
        {
            gun.Fire(-1);
            gm.ammo--;
        }
        if (shootRight && gm.GamePaused > 0 && gm.ammo > 0)
        {
            gun.Fire(1);
            gm.ammo--;
        }
    }
}