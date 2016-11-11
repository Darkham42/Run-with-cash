using UnityEngine;
using System.Collections;

public class TestPhysic : MonoBehaviour
{
    public bool Touched = false;
    public bool DeathWall = false;

    public Vector3 Position;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "DeathWall")
        {
            Touched = true;
        }
        else
        {
            DeathWall = true;
        }
    }

    //void OnTriggerStay(Collider other)
    //{
    //    Touched = true;
    //}

    void OnTriggerExit(Collider other)
    {
        Touched = false;
    }
}
