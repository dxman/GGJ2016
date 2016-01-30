using System;
using System.Collections.Generic;

public abstract class GameEvent
{
}

public class EventSystem : Singleton<EventSystem>
{
    public delegate void EventDelegate<T>(T e) where T : GameEvent;
    private delegate void EventDelegate(GameEvent e);

    private readonly Dictionary<Type, EventDelegate> _delegates = new Dictionary<Type, EventDelegate>();
    private readonly Dictionary<Delegate, EventDelegate> _delegateLookup = new Dictionary<Delegate, EventDelegate>();

    public void AddListener<T>(EventDelegate<T> eventDelegate) where T : GameEvent
    {
        // Early-out if we've already registered this delegate
        if (_delegateLookup.ContainsKey(eventDelegate))
            return;

        // Create a new non-generic delegate which calls our generic one.
        // This is the delegate we actually invoke.
        EventDelegate internalDelegate = (e) => eventDelegate((T)e);
        _delegateLookup[eventDelegate] = internalDelegate;

        EventDelegate tempDel;
        if (_delegates.TryGetValue(typeof(T), out tempDel))
        {
            _delegates[typeof(T)] = tempDel += internalDelegate;
        }
        else
        {
            _delegates[typeof(T)] = internalDelegate;
        }
    }

    public void RemoveListener<T>(EventDelegate<T> del) where T : GameEvent
    {
        EventDelegate internalDelegate;
        if (_delegateLookup.TryGetValue(del, out internalDelegate))
        {
            EventDelegate tempDel;
            if (_delegates.TryGetValue(typeof(T), out tempDel))
            {
                tempDel -= internalDelegate;
                if (tempDel == null)
                {
                    _delegates.Remove(typeof(T));
                }
                else
                {
                    _delegates[typeof(T)] = tempDel;
                }
            }

            _delegateLookup.Remove(del);
        }
    }

    public void Raise(GameEvent e)
    {
        EventDelegate del;
        if (_delegates.TryGetValue(e.GetType(), out del))
        {
            del.Invoke(e);
        }
    }
}