using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPositions : MonoBehaviour, IPowerUps
{

    //Variables
    GameObject[] players;
    Vector3[] playerPos = new Vector3[2];

    //Audio
    private AudioManager audioManager;
    public AudioClip swapSound;
    [Range(0.0f, 1.0f)]
    public float volume = .5f;

    // Start is called before the first frame update
    void Start()
    {
        //Grab the players
        players = GameObject.FindGameObjectsWithTag("Player");
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void ActivatePower()
    {
        //Play sound
        audioManager.PlaySound(swapSound, volume);

        //Set the initial positions
        playerPos[0] = players[0].transform.position;
        playerPos[1] = players[1].transform.position;

        //Swap positions
        players[0].transform.position = playerPos[1];
        players[1].transform.position = playerPos[0];

        //Destroy pickup
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Make sure only the players can activate the powerup
        if (collision.CompareTag("Player"))
            ActivatePower();
    }
}
