using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool GameOver = false;
    public float GamePaused = .0f;
    private int cash;
    private float timer;

    bool gameStarted = false;
    bool gameDefinitelyOver = false;

    GameObject player;

    float startTimer = 3.0f;
    GameObject UI;

    void Start()
    {
        cash = 50;
        UpdateCash();
        UI = GameObject.Find("CanvasUI");
        player = GameObject.Find("Car");
        UpdateStartTimeUI();
    }

    void UpdateCashUI()
    {
        UI.transform.FindChild("Cash").GetComponent<Text>().text = "$ " + (cash * 1000).ToString();
    }

    void UpdateStartTimeUI()
    {
        int timer = (int)startTimer + 1;
        UI.transform.FindChild("StartTimer").GetComponent<Text>().text = timer.ToString();
    }

    void Update()
    {
        timer += Time.deltaTime * GamePaused;
        if (cash <= 0)
        {
            GameOver = true;
            cash = 0;
        }

        startTimer -= Time.deltaTime;

        UpdateStartTimeUI();
        UpdateCashUI();

        if (startTimer <= 0.0f && gameStarted == false)
        {
            GamePaused = 1.0f;
            gameStarted = true;
            UI.transform.FindChild("StartTimer").gameObject.SetActive(false);
        }

        if (GameOver && gameDefinitelyOver == false)
        {
            GamePaused -= Time.deltaTime;
            if (GamePaused <= 0.0f)
            {
                GamePaused = 0.0f;
                gameDefinitelyOver = true;
                UI.transform.FindChild("GameOver").gameObject.SetActive(true);
            }
        }
    }

    public void AddCash(int value)
    {
        cash += value;
        UpdateCash();
    }

    public void RemoveCash(int value)
    {
        if (player.GetComponent<PlayerController>().timerInvincible <= 0.0f)
        {
            cash -= value;
        }
        UpdateCash();
    }

    void UpdateCash()
    {
        // Texte à afficher
        Debug.Log("$" + cash * 1000);
    }
}
