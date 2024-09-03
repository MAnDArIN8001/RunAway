using System;
using UnityEngine;

public abstract class State
{
    public abstract event Action OnStateClosed;

    public abstract void Enter();

    public abstract void Update();

    public abstract void BreakState();

    public abstract void Exit();

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
