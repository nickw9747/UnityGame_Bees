﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeeMoveController : MonoBehaviour {

    private NavMeshAgent beeAgent;

    [SerializeField]
    private Transform targetTransform = null;

    [SerializeField]
    private float maxSpeed = 1.0f;
    [SerializeField]
    private float maxAngularSpeed = 10.0f;

    [SerializeField]
    private float trackForSeconds = 2.0f;
    private float trackingTimer = 0.0f;

    private void Awake() {
        beeAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        trackingTimer += Time.deltaTime;
        beeAgent.speed = maxSpeed;
        beeAgent.angularSpeed = maxAngularSpeed;

        if (trackingTimer <= trackForSeconds) {
            TrackTarget();
        }
        else {
            ZoomToTarget();
        }
    }

    private void TrackTarget() {
        if (!beeAgent.pathPending) {
            beeAgent.SetDestination(targetTransform.position);
        }
    }

    private void ZoomToTarget() {

    }
}
