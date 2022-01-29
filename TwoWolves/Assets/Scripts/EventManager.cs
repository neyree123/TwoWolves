using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    public event Action EndGame;

    public void TriggerEndGame()
    {
        EndGame?.Invoke();
    }
}
