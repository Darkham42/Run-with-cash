using UnityEngine;
using System.Collections;

public class TestPhysic : MonoBehaviour
{
    [HideInInspector]
    public bool Touched = false;
    [HideInInspector]
    public bool CarTouched = false;
    [HideInInspector]
    public bool DeathWall = false;
    [HideInInspector]
    public bool CopTouched = false;
    [HideInInspector]
    public GameObject CarGameObject = null;
    [HideInInspector]
    public GameObject CopGameObject = null;
    [HideInInspector]
    public Vector3 Position;

    [HideInInspector]
    public bool TouchedCashBonus = false;
    [HideInInspector]
    public GameObject TouchedCashGameObject = null;

    [HideInInspector]
    public bool TouchedAmmoBonus = false;
    [HideInInspector]
    public GameObject TouchedAmmoGameObject = null;

    [HideInInspector]
    public bool TouchedDynamiteBonus = false;
    [HideInInspector]
    public GameObject TouchedDynamiteGameObject = null;

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "DeathWall")
        {
            DeathWall = true;
        }
        else if (other.tag == "CivilianCar")
        {
            CarTouched = true;
            CarGameObject = other.gameObject;
        }
        else if (other.tag == "CopCar")
        {
            CopTouched = true;
            CopGameObject = other.gameObject;
		}
		else if (other.tag == "Projectile")
		{
		}
        else if (other.tag == "CashBonus")
        {
            TouchedCashBonus = true;
            TouchedCashGameObject = other.gameObject;
        }
        else if (other.tag == "AmmoBonus")
        {
            TouchedAmmoBonus = true;
            TouchedAmmoGameObject = other.gameObject;
        }
        else if (other.tag == "DynamiteBonus")
        {
            TouchedDynamiteBonus = true;
            TouchedDynamiteGameObject = other.gameObject;
        }
        else
        {
            Touched = true;
        }
    }

    //void OnTriggerStay(Collider other)
    //{
    //    Touched = true;
    //}

    void OnTriggerExit(Collider other)
    {
        Touched = false;
        CarTouched = false;
        CopTouched = false;
        TouchedCashBonus = false;
        TouchedAmmoBonus = false;
        TouchedDynamiteBonus = false;
    }
}
