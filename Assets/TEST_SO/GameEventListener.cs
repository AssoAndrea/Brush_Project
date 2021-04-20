using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent[] GameEvent;
    public UnityEvent response;

    private void OnEnable()
    {
        foreach (GameEvent ev in GameEvent)
        {
            ev.RegisterLisenter(this);
        }
    }
    private void OnDisable()
    {
        foreach (GameEvent ev in GameEvent)
        {
            ev.UnregisterListener(this);
        }
    }
    public void OnEventRaised()
    {
        response.Invoke();
    }
}
