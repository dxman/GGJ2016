using System;
using System.Collections.Generic;

public class StackProcess : Process
{
    private readonly List<Process> _processes;

    public StackProcess()
    {
        _processes = new List<Process>();
    }

    public void AddProcess(Process process)
    {
        _processes.Add(process);
    }

    public override void Update(float timeElapsed)
    {
        if (_processes.Count > 0)
        {
            _processes[_processes.Count - 1].Update(timeElapsed);

            for (int i = 0; i < _processes.Count; i++)
            {
                if (_processes[i].Finished)
                {
                    _processes.RemoveAt(i);
                }
            }
        }
    }
}
