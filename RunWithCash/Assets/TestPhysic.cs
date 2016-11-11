using UnityEngine;
using System.Collections;

public class TestPhysic : MonoBehaviour
{
    public bool Touched = false;

    public Vector3 Position;

    void OnTriggerEnter(Collider other)
    {
        Touched = true;
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
