using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeeMoveController : MonoBehaviour {

    private NavMeshAgent beeAgent;
    private Rigidbody rb;

    [SerializeField]
    private Transform targetTransform = null;

    [SerializeField]
    private float maxSpeed = 1.0f;
    [SerializeField]
    private float maxAngularSpeed = 10.0f;

    [SerializeField]
    private float trackForSeconds = 2.0f;
    private float timer = 0.0f;

    [SerializeField]
    private float zoomSpeed = 2.0f;

    private bool tracking = true;
    private bool zooming = false;

    private void Awake() {
        beeAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {

        if (timer <= trackForSeconds && tracking) {
            timer += Time.deltaTime;
            beeAgent.speed = maxSpeed;
            beeAgent.angularSpeed = maxAngularSpeed;
            TrackTarget();
        }
        else if (tracking) {
            transform.rotation.SetLookRotation(targetTransform.position);

        }
        else if (!zooming) {
            //beeAgent.isStopped = true;
            beeAgent.enabled = false;
            rb.useGravity = false;
            ZoomForward();
            zooming = true;
        }
    }

    private void TrackTarget() {
        if (!beeAgent.pathPending) {
            beeAgent.SetDestination(targetTransform.position);
        }
    }

    private void ZoomForward() {
        //transform.position += transform.forward * zoomSpeed * Time.deltaTime;
        rb.velocity = transform.forward * zoomSpeed;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(gameObject.name + " collided (trigger) with " + other.name);
    } 
}
