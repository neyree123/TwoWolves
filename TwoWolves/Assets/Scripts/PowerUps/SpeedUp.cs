using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour, IPowerUps
{
    //Variables
    [Header("Player Variables")]
    GameObject player;
    PlayerController controller;
    [SerializeField] float speedMultiplier;
    float initialSpeed;

    [Header("Game Object Variables")]
    SpriteRenderer sprite;
    CircleCollider2D circleCollider;
    [SerializeField] float duration;

    //Audio
    private AudioManager audioManager;
    public AudioClip speedSound;
    [Range(0.0f, 1.0f)]
    public float volume = .5f;

    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void ActivatePower()
    {
        //Play audio clip
        audioManager.PlaySound(speedSound, volume);

        //Player moves faster
        controller = player.GetComponent<PlayerController>();
        initialSpeed = controller.speed;

        controller.speed *= speedMultiplier;

        controller.StartCoroutine(Timer());

        //Return to regular speed after the time is up
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        //Make sure only the players can activate the powerup
        if(collision.CompareTag("Player"))
        {
            //Makes sure that the powerup can't be grabbed twice
            sprite.enabled = false;
            circleCollider.enabled = false;

            //Activate Power
            player = collision.gameObject;
            ActivatePower();

            //Get rid of powerup
            Destroy(gameObject);
        }
    }

    IEnumerator Timer()
    {
        controller.speed *= speedMultiplier;
        yield return new WaitForSeconds(duration);
        controller.speed /= speedMultiplier;
    }
}
