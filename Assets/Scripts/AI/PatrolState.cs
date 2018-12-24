using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;

public class PatrolState : AIState {
    private NavMeshAgent navMeshAgent;
    private Transform transform;
    private float maxMoveDistance;
    private float stopppingDistanceBuffer = 2;

    public PatrolState(NavMeshAgent navMeshAgent, float maxMoveDistance) {
        this.navMeshAgent = navMeshAgent;
        transform = navMeshAgent.transform;
        this.maxMoveDistance = maxMoveDistance;
    }

    public override void EnterState() {
        navMeshAgent.enabled = true;
    }

    public override void ExitState() {
        navMeshAgent.enabled = false;
    }

    public override void Tick() {
        if (CloseToDestination()) {
            MoveToRandomPosition();
        }
    }

    private void MoveToRandomPosition() {
        Vector3 destination = GetRandomPointOnMesh();
        navMeshAgent.SetDestination(destination);
    }

    private Vector3 GetRandomPointOnMesh() {
        Vector3 randomDirection = transform.InverseTransformDirection(UnityEngine.Random.insideUnitSphere);
        Vector3 adjustedDirection = randomDirection.With(x: Mathf.Clamp(randomDirection.x, -0.8f, 0.8f), y: 0, z: Mathf.Clamp(randomDirection.z, 0.5f, 1));
        Vector3 worldPoint = transform.position + transform.TransformDirection(randomDirection * maxMoveDistance);
        if (NavMesh.SamplePosition(worldPoint, out NavMeshHit navMeshPoint, maxMoveDistance, NavMesh.AllAreas)) {
            return navMeshPoint.position;
        } else {
            return transform.position;
        }
    }

    private bool CloseToDestination() {
        if (!navMeshAgent.pathPending) {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + stopppingDistanceBuffer) {
                return true;
            }
        }

        return false;
    }
}
