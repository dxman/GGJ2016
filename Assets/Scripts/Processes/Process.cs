using System;

/// <summary>
/// http://gamedev.stackexchange.com/questions/26886/how-to-chain-actions-animations-together-and-delay-their-execution
/// </summary>
public abstract class Process
{
    public bool Finished = false;

    public abstract void Update(float timeElapsed);
}
