using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool GameOver = false;
    public float GamePaused = .0f;
    private int cash;
    public int dynamite = 0;
    public int ammo = 0;
    private float timer;

    public List<AudioClip> Sounds;

    bool gameStarted = false;
    bool gameDefinitelyOver = false;

    GameObject player;

    float startTimer = 3.0f;
    GameObject UI;

    AudioSource oneShot;
    AudioSource multi;

    public void PlaySound(int nbr)
    {
        if (GetComponent<AudioSource>().isPlaying == false)
            GetComponent<AudioSource>().PlayOneShot(Sounds[nbr]);
    }

    public void PlaySoundMulti(int nbr)
    {
            GetComponent<AudioSource>().PlayOneShot(Sounds[nbr]);
    }

    void Start()
    {
        cash = 50;
        UI = GameObject.Find("CanvasUI");
        player = GameObject.Find("Car");
        UpdateStartTimeUI();

        oneShot = GetComponents<AudioSource>()[0];
        multi = GetComponents<AudioSource>()[1];
    }

    void UpdateCashUI()
    {
        UI.transform.FindChild("Cash").GetComponent<Text>().text = "$ " + (cash * 1000).ToString();
    }

    void UpdateAmmosUI()
    {
        UI.transform.FindChild("AmmoTommy").GetComponent<Text>().text = "x" + ammo.ToString();
        UI.transform.FindChild("AmmoDynamite").GetComponent<Text>().text = "x" + dynamite.ToString();
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
        UpdateAmmosUI();

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
                UI.transform.FindChild("DynamiteImage").gameObject.SetActive(false);
                UI.transform.FindChild("TommyImage").gameObject.SetActive(false);
                UI.transform.FindChild("AmmoDynamite").gameObject.SetActive(false);
                UI.transform.FindChild("AmmoTommy").gameObject.SetActive(false);
                gameDefinitelyOver = true;
                UI.transform.FindChild("GameOver").gameObject.SetActive(true);
                float highScore = PlayerPrefs.GetFloat("HighScore", 0);
                if (timer > highScore)
                {
                    PlayerPrefs.SetFloat("HighScore", timer);
                    UI.transform.FindChild("HighScore").gameObject.GetComponent<Text>().text = "You are the new High Score with " + string.Format("{0}'{1}", Mathf.Floor(timer / 60), timer % 60);
                }
                else
                {
                    UI.transform.FindChild("HighScore").gameObject.GetComponent<Text>().text = "The High Score : " + string.Format("{0}'{1}", Mathf.Floor(highScore / 60), highScore % 60);
                }
                UI.transform.FindChild("Score").gameObject.SetActive(true);
                UI.transform.FindChild("Score").gameObject.GetComponent<Text>().text = "The cops caught you in " + string.Format("{0}'{1}", Mathf.Floor(timer / 60), timer % 60);
                UI.transform.FindChild("HighScore").gameObject.SetActive(true);
                UI.transform.FindChild("Image").gameObject.SetActive(true);

            }
        }
    }

    public void AddCash(int value)
    {
        cash += value;
    }

    public void RemoveCash(int value)
    {
        if (player.GetComponent<PlayerController>().timerInvincible <= 0.0f)
        {
            cash -= value;
        }
        PlaySound(4);
        player.GetComponent<PlayerController>().SpawnParticles();
    }
}
