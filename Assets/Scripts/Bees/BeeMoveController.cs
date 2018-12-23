using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BeeMoveController : MonoBehaviour {

    private NavMeshAgent beeAgent;
    private NavMeshPath path;

    private void Awake() {
        beeAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }

    public void TrackTarget(Vector3 target) {
        if (!beeAgent.pathPending) {
            beeAgent.SetDestination(target);
        }
    }

    public void SetMaxSpeed(float maxSpeed) {
        if (maxSpeed >= 0.0f) {
            beeAgent.speed = maxSpeed;
        }
    }

    public void SetMaxAngularSpeed(float maxAngularSpeed) {
        if (maxAngularSpeed >= 0.0f) {
            beeAgent.angularSpeed = maxAngularSpeed;
        }
    }

    public void StopTracking() {
        beeAgent.enabled = false;
    }

    public void StartTracking() {
        beeAgent.enabled = true;
    }
}
