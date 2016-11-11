using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour
{

    void Start()
    {

    }

    void FixedUpdate()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * 25;
    }
}
