using UnityEngine;
using System.Collections;

public class TestPhysic : MonoBehaviour
{
    public bool Touched = false;
    public bool CarTouched = false;
    public bool DeathWall = false;
    public bool CopTouched = false;
    public GameObject CarGameObject = null;
    public GameObject CopGameObject = null;


    public Vector3 Position;

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
    }
}
