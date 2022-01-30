using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapColors : MonoBehaviour, IPowerUps
{

    //Variables
    [Header("Player Variables")]
    PlayerController controller;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePower()
    {
        controller.StartCoroutine(SwitchColor());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Make sure only the players can activate the powerup
        if (collision.CompareTag("Player"))
        {
            //Makes sure that the powerup can't be grabbed twice
            sprite.enabled = false;
            circleCollider.enabled = false;

            //Activate Power
            controller = collision.gameObject.GetComponent<PlayerController>();
            ActivatePower();

            //Get rid of powerup
            Destroy(gameObject);
        }
    }

    private IEnumerator SwitchColor()
    {
        controller.colorMode = controller.GetOppositeColor();
        controller.transform.GetChild(0).gameObject.SetActive(false);
        controller.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        controller.colorMode = controller.GetOppositeColor();
        controller.transform.GetChild(0).gameObject.SetActive(true);
        controller.transform.GetChild(1).gameObject.SetActive(false);
    }
}
