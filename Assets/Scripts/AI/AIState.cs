using UnityEngine;
using System.Collections;
using System;

public abstract class AIState {

    public abstract void EnterState();

    public abstract void Tick();

    public abstract void ExitState();

}

public struct StateTransition {
    public AIState nextState;
    public Action transitionOn;
}

