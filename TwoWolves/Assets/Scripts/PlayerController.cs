using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveDir;
    private Rigidbody2D rb;
    public float speed = 5f;
    Forces forces;
    public ColorMode colorMode;
    private Animator anim;
    private bool isFlipped = false;

    //Audio for using the force ability
    private AudioSource playerAudio;
    public AudioClip forceSound;
    public AudioClip reversedForceSound;
    public AudioClip forceSoundCooldown;
    [HideInInspector]
    public AudioClip currentForceSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        forces = GetComponent<Forces>();
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        currentForceSound = forceSound;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * speed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
        anim.SetFloat("Speed", Mathf.Abs(moveDir.magnitude));
        if(moveDir.x < 0 && !isFlipped)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            isFlipped = true;
        }
        else if (moveDir.x > 0 && isFlipped)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            isFlipped = false;
        }
    }

    public void OnActionPurple()
    {
        if (forces.canUse)
        {
            playerAudio.PlayOneShot(currentForceSound);
            forces.Attract(colorMode);
            forces.Repulse(GetOppositeColor());
            StartCoroutine(forces.Cooldown());
        }
        else
        {
            playerAudio.PlayOneShot(forceSoundCooldown);
        }
    }

    public void OnActionYellow()
    {
        if (forces.canUse)
        {
            playerAudio.PlayOneShot(currentForceSound);
            forces.Attract(colorMode);
            forces.Repulse(GetOppositeColor());
            StartCoroutine(forces.Cooldown());
        }
        else
        {
            playerAudio.PlayOneShot(forceSoundCooldown);
        }
    }

    public ColorMode GetOppositeColor()
    {
        return colorMode == ColorMode.Black ? ColorMode.White : ColorMode.Black;
    }
}
