using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "AI/State")]
public class State : ScriptableObject {

    [SerializeField]
    private StateAction[] actions;

    public void DoActions(StateController controller) {
        foreach (var action in actions) {
            action.Act(controller);
        }
    }

    public void EnterState(StateController controller) {
        foreach (var action in actions) {
            action.OnStateEnter(controller);
        }
    }

    public void ExitState(StateController controller) {
        foreach (var action in actions) {
            action.OnStateExit(controller);
        }
    }
}
