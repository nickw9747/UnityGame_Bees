using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeeMoveController : MonoBehaviour {

    private NavMeshAgent beeAgent;

    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private float maxSpeed = 1.0f;

    private void Awake() {
        beeAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        beeAgent.speed = maxSpeed;
        if (!beeAgent.pathPending) {
            beeAgent.SetDestination(targetTransform.position);
        }
    }
}
