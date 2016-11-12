﻿using UnityEngine;
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
                float highScore = PlayerPrefs.GetFloat("HighScore", 0);
                if (timer > highScore) {
                    PlayerPrefs.SetFloat("HighScore", timer);
                    UI.transform.FindChild("HighScore").gameObject.GetComponent<Text>().text = "You are the new High Score with " + string.Format("{0}'{1}", Mathf.Floor(timer / 60), timer % 60);
                } else {
                    UI.transform.FindChild("HighScore").gameObject.GetComponent<Text>().text = "The High Score : " + string.Format("{0}'{1}", Mathf.Floor(highScore / 60), highScore % 60);
                }
                UI.transform.FindChild("Score").gameObject.SetActive(true);
                UI.transform.FindChild("Score").gameObject.GetComponent<Text>().text = "The cops caught you in " + string.Format("{0}'{1}", Mathf.Floor(timer / 60), timer % 60);
                UI.transform.FindChild("HighScore").gameObject.SetActive(true);

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
        player.GetComponent<PlayerController>().SpawnParticles();
    }

    void UpdateCash()
    {
        // Texte à afficher
    }
}
