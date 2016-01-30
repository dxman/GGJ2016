using System;

public class GenericProcess : Process
{
    private readonly Action _action;

    public GenericProcess(Action action)
    {
        _action = action;
    }

    public override void Update(float timeElapsed)
    {
        _action();

        Finished = true;
    }
}
