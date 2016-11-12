using UnityEngine;
using System.Collections;

public class AmmoBonus : MonoBehaviour {

    public int AmmoEarned = 1;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
