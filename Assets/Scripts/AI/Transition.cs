using UnityEngine;
using System;

public abstract class Transition : ScriptableObject {

    public abstract bool CheckTransition(StateController controller);

    public abstract void TransitionGizmo(StateController controller);
}
