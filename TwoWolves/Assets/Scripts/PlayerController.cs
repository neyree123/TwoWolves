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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        Debug.Log("Action Purple");
    }

    public void OnActionYellow()
    {
        Debug.Log("Action Yellow");
    }
}
