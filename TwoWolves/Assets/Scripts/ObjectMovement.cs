using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        //Get a random direction
        float degrees = (float)Random.Range(0, 360) * Mathf.Deg2Rad;

        float xDir = Mathf.Cos(degrees);
        float yDir = Mathf.Sin(degrees);

        rb.AddRelativeForce((new Vector2(xDir, yDir)).normalized * speed, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //rb.velocity = -rb.velocity;
        ContactPoint2D[] points = collision.contacts;

        foreach(ContactPoint2D contact in points)
        {
            rb.AddRelativeForce(contact.normal.normalized * speed);
        }
    }
}
