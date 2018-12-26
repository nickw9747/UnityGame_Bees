using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "AI/Action/RandomPatrol")]
public class RandomPatrolAction : StateAction {

    [SerializeField]
    private float moveDistance = 10.0f;
    [SerializeField]
    private float stoppingDistanceBuffer = 2;
    [SerializeField]
    private float maxPathFactor = 1.2f;

    public override void Act(StateController controller) {
        if (CloseToDestination(controller.Agent) || controller.Agent.remainingDistance >= (moveDistance + stoppingDistanceBuffer) * maxPathFactor) {
            MoveToRandomPosition(controller.Agent);
        }
    }

    public override void OnStateEnter(StateController controller) {
        controller.Agent.enabled = true;
    }

    public override void OnStateExit(StateController controller) {
        controller.Agent.enabled = false;
    }

    private void MoveToRandomPosition(NavMeshAgent navMeshAgent) {
        Vector3 destination = GetRandomPointOnMesh(navMeshAgent.transform);
        navMeshAgent.SetDestination(destination);
    }

    private Vector3 GetRandomPointOnMesh(Transform transform) {
        Vector3 randomDirection = transform.InverseTransformDirection(Random.insideUnitSphere);
        Vector3 adjustedDirection = randomDirection.With(
            x: Mathf.Clamp(randomDirection.x, -0.8f, 0.8f), 
            y: 0, 
            z: Mathf.Clamp(randomDirection.z, 0.5f, 1));

        Vector3 worldPoint = transform.position + transform.TransformDirection(randomDirection * moveDistance);
        if (NavMesh.SamplePosition(worldPoint, out NavMeshHit navMeshPoint, moveDistance, NavMesh.AllAreas)) {
            return navMeshPoint.position;
        }
        else {
            return transform.position;
        }
    }

    private bool CloseToDestination(NavMeshAgent navMeshAgent) {
        if (!navMeshAgent.pathPending) {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + stoppingDistanceBuffer) {
                return true;
            }
        }
        return false;
    }
}
