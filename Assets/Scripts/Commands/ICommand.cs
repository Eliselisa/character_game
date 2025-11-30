using UnityEngine;

public interface ICommand
{
    bool Completed { get; }

    void Execute();

    void Update();

    void Stop();
}
