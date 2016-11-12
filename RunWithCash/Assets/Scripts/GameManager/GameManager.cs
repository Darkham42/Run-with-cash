using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public bool GameOver = false;
    private int cash;
    private float timer;
 
    void Start()
    {
        cash = 50;
        UpdateCash();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (cash == 0)
            GameOver = true;
    }

    public void AddCash(int value)
    {
        cash += value;
        UpdateCash();
    }

    public void RemoveCash(int value)
    {
        cash -= value;
        UpdateCash();
    }

    void UpdateCash()
    {
        // Texte à afficher
        Debug.Log("$" + cash * 1000);
    }
}
