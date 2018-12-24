using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;

public class Bee2 : MonoBehaviour {

    [SerializeField]
    private Transform targetTransform = null;

    private AIState startingState = null;
    private AIState currentState = null;

    private NavMeshAgent navMeshAgent;
    private LineOfSight lineOfSight;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lineOfSight = GetComponent<LineOfSight>();
    }

    private void OnEnable() {
        //SwitchState(new FollowTransformState(navMeshAgent, targetTransform));
        SwitchState(new PatrolState(navMeshAgent, 10));
        lineOfSight.OnTargetInLineOfSight += TargetSpotted;
    }

    private void TargetSpotted(GameObject obj) {
        SwitchState(new AimAtTargetState(transform, obj.transform, 6));
    }

    private void OnDisable() {
        NullState();
    }

    private void SwitchState(AIState newState) {
        currentState?.ExitState();
        newState.EnterState();
        currentState = newState;
    }

    private void NullState() {
        currentState.ExitState();
        currentState = null;
    }

    private void Update() {
        currentState.Tick();
    }

}
