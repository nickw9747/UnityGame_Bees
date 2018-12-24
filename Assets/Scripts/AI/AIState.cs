using UnityEngine;
using System.Collections;

public abstract class AIState {

    public abstract void OnStateEnter();

    public abstract void Tick();

    public abstract void OnStateExit();

}
