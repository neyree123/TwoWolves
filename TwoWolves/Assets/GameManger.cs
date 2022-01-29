using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManger : MonoBehaviour
{
    private int playerNum = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test(PlayerInput input)
    {
        playerNum++;

        if (playerNum == 1)
        {
            input.SwitchCurrentActionMap("PurpleWolf");
        }
        else
        {
            input.SwitchCurrentActionMap("YellowWolf");
        }
    }
}
