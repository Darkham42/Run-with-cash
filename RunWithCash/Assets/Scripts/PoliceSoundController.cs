using UnityEngine;
using System.Collections;

public class PoliceSoundController : MonoBehaviour {

    private AudioSource source;
    private GameObject player;
    private float playerDistance;
    private bool playAudio;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        player = GameObject.Find("Car").gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        source.volume = (0.3f - (Vector3.Distance(transform.position, player.transform.position) / 100)) * 4;
    }
}
