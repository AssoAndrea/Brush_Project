using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EVENT", menuName = "Events/EVENT")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        foreach (GameEventListener listener in listeners)
        {
            listener.OnEventRaised();
        }
    }

    public void RegisterLisenter(GameEventListener listener) => listeners.Add(listener);
    public void UnregisterListener(GameEventListener listener) => listeners.Remove(listener);
}
