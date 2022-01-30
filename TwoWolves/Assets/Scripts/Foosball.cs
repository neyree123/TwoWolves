 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foosball : MonoBehaviour
{
    Forces force;
    [SerializeField]
    SetBorders setBorders;
    [SerializeField]
    SetBorders.Anchor anchor;
    // Start is called before the first frame update
    void Start()
    {
        force = GetComponent<Forces>();
        setBorders.SetRelativePos(gameObject, anchor);
    }

    // Update is called once per frame
    void Update()
    {
        if(force.canUse)
        {
            force.Repulse(ColorMode.Black);
            force.Repulse(ColorMode.White);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("White") || collision.gameObject.CompareTag("Black"))
        {

        }
    }
}
