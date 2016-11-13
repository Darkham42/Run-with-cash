using UnityEngine;
using System.Collections;

public class DynamiteBonus : MonoBehaviour {

    public int DynamiteEarned = 3;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
