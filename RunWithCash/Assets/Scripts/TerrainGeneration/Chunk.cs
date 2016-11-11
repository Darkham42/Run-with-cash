using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour
{
    public GameObject LeftPart;
    public GameObject RightPart;

    public void Init()
    {
        if (LeftPart)
        {
            LeftPart.transform.parent = transform;
        }

        if (RightPart)
        {
            RightPart.transform.parent = transform;
        }
    }
}
