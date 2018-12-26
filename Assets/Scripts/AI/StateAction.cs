using UnityEngine;
using System.Collections;

public abstract class StateAction : ScriptableObject {

    public abstract void OnStateEnter(StateController controller);

    public abstract void Act(StateController controller);

    public abstract void OnStateExit(StateController controller);

}
