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
    public ColorMode colorMode;

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
        forces.Attract(colorMode);

        forces.Repulse(GetOppositeColor());
        StartCoroutine(forces.Cooldown());
    }

    public void OnActionYellow()
    {
        if (forces.canUse)
        {
            forces.Attract(ColorMode.White);
            forces.Repulse(ColorMode.Black);
            StartCoroutine(forces.Cooldown());
        }
    }

    public ColorMode GetOppositeColor()
    {
        return colorMode == ColorMode.Black ? ColorMode.White : ColorMode.Black;
    }
}
