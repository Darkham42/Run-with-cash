using UnityEngine;
using System.Collections;

public class CashBonus : MonoBehaviour
{
    public int CashEarned = 1;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
