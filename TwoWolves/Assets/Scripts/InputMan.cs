using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMan : MonoBehaviour
{
    public PlayerController purpleWolf;
    public PlayerController yellowWolf;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnYellowMove(InputValue value)
    {
        yellowWolf.OnMove(value);
    }
    private void OnPurpleMove(InputValue value)
    {
        purpleWolf.OnMove(value); 
    }

    private void OnPurpleAction(InputValue value)
    {
        purpleWolf.OnActionPurple();
    }

    private void OnYellowAction(InputValue value)
    {
        yellowWolf.OnActionYellow();
    }
}
