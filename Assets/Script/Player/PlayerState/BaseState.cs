using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IState
{
    public virtual void EnterState() { }

    public virtual void ExitState() { }

    public virtual void UpdateState() { }

}
