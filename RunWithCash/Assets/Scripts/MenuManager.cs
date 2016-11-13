using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {
    GameObject UI;
    bool creditsShown = false;

    // Use this for initialization
    void Start () {
        UI = GameObject.Find("Canvas");
    }
	
	// Update is called once per frame
	void Update () {
        if ( Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Fire2") )
        {
            creditsShown = false;
            UI.transform.FindChild("Credits").gameObject.SetActive(false);

            UI.transform.FindChild("ButtonPlay").gameObject.SetActive(true);
            UI.transform.FindChild("Button How to Play").gameObject.SetActive(true);
            UI.transform.FindChild("Button Credits").gameObject.SetActive(true);
            UI.transform.FindChild("Button Exit").gameObject.SetActive(true);
        }

    }

    public void StartGame() {
        SceneManager.LoadScene("main");
    }

    public void HowToPlay() {
        //TODO
    }

    public void DisplayCredits() {
        creditsShown = true;
        UI.transform.FindChild("ButtonPlay").gameObject.SetActive(false);
        UI.transform.FindChild("Button How to Play").gameObject.SetActive(false);
        UI.transform.FindChild("Button Credits").gameObject.SetActive(false);
        UI.transform.FindChild("Button Exit").gameObject.SetActive(false);

        UI.transform.FindChild("Credits").gameObject.SetActive(true);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
