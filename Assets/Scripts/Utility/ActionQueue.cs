using System;
using System.Collections.Generic;

public abstract class GameAction
{
    public abstract void Execute();
}

public class ActionQueue : Singleton<ActionQueue>
{
    private readonly List<Action> _actions = new List<Action>();

    private ActionQueue() { }

    public void Update()
    {
        foreach (Action action in _actions)
        {
            action.Invoke();
        }

        _actions.Clear();
    }

    public void Add(GameAction gameAction)
    {
        _actions.Add(gameAction.Execute);
    }
}
