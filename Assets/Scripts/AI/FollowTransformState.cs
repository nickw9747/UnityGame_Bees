using UnityEngine;
using UnityEngine.AI;

public class FollowTransformState : AIState {
    private NavMeshAgent navMeshAgent;
    private Transform target;

    public FollowTransformState(NavMeshAgent navMeshAgent, Transform targetTransform) {
        this.navMeshAgent = navMeshAgent;
        target = targetTransform;
    }

    public override void EnterState() {
        navMeshAgent.enabled = true;
    }

    public override void ExitState() {
        navMeshAgent.enabled = false;
    }

    public override void Tick() {
        TrackTarget(target.position);
    }

    private void TrackTarget(Vector3 target) {
        if (!navMeshAgent.pathPending) {
            navMeshAgent.SetDestination(target);
        }
    }
}
