using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StateController : MonoBehaviour {

    public NavMeshAgent Agent { get; private set; }

    [SerializeField]
    private Transform target;
    public Transform Target { get { return target; } }

    [SerializeField]
    private StateTree[] stateTrees;
    private StateTree activeStateTree;

    private void Awake() {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        activeStateTree = stateTrees[0];
    }

    private void Update() {
        activeStateTree.state.DoActions(this);
        CheckTransitions();
    }

    public void CheckTransitions() {
        foreach (var transitionBranch in activeStateTree.transitionBranches) {
            foreach (var transition in transitionBranch.transitions) {
                if (transition.CheckTransition(this)) {
                    TransitionState(transitionBranch.nextStateIndex);
                }
            }
        }
    }

    public void TransitionState(int nextStateIndex) {
        if (activeStateTree.state != null) {
            activeStateTree.state.ExitState(this);
        }
        activeStateTree = stateTrees[nextStateIndex];
        activeStateTree.state.EnterState(this);
    }

}

[System.Serializable]
public struct StateTree {
    public State state;
    public TransitionBranch[] transitionBranches;
}

[System.Serializable]
public struct TransitionBranch {
    public Transition[] transitions;
    public int nextStateIndex;
}
