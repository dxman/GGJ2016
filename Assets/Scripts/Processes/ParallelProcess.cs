using System;
using System.Collections.Generic;

public class ParallelProcess : Process
{
    private readonly List<Process> _processes;

    public ParallelProcess()
    {
        _processes = new List<Process>();
    }

    public void AddProcess(Process process)
    {
        _processes.Add(process);
    }

    public override void Update(float timeElapsed)
    {
        for (int i = 0; i < _processes.Count; i++)
        {
            _processes[i].Update(timeElapsed);
        }

        for (int i = 0; i < _processes.Count; i++)
        {
            if (_processes[i].Finished)
            {
                _processes.RemoveAt(i);
            }
        }
    }
}
