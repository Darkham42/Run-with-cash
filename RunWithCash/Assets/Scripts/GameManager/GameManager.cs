using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public bool GameOver = false;
    public float GamePaused = .0f;
    private int cash;
    private float timer;

    float startTimer = 3.0f;

    void Start()
    {
        cash = 50;
        UpdateCash();
    }

    void Update()
    {
        timer += Time.deltaTime * GamePaused;
        if (cash == 0)
            GameOver = true;

        startTimer -= Time.deltaTime;
        if (startTimer <= 0.0f)
        {
            GamePaused = 1.0f;
        }
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
