using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool move = false;
    private Vector2 moveDir;
    private Rigidbody2D rb;
    public float speed = 5f;
    Forces forces;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        forces = GetComponent<Forces>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * speed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
    }

    public void OnActionPurple()
    {
        forces.Attract(ColorMode.Black);
        forces.Repulse(ColorMode.White);
        StartCoroutine(forces.Cooldown());
    }

    public void OnActionYellow()
    {
        forces.Attract(ColorMode.White);
        forces.Repulse(ColorMode.Black);
        StartCoroutine(forces.Cooldown());
    }
}
