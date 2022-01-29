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

    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    public void ActivatePower()
    {
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
