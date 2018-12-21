using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BeeMoveController : MonoBehaviour {

    private NavMeshAgent beeAgent;
    private Rigidbody rb;
    private LineOfSight los;
    
    [SerializeField]
    private Transform targetTransform = null;

    [SerializeField]
    private float maxSpeed = 1.0f;
    [SerializeField]
    private float maxAngularSpeed = 10.0f;

    [SerializeField]
    private float trackForSeconds = 2.0f;

    [SerializeField]
    private float zoomSpeed = 2.0f;

    private bool isTracking = true;

    [SerializeField]
    private float attackTurnSpeed = 3.0f;
    private NavMeshPath path;

    private void Awake() {
        beeAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        los = GetComponent<LineOfSight>();

        path = new NavMeshPath();

        los.OnTargetInLineOfSight += Attack;
    }

    private void Update() {
        if (isTracking) {
            TrackTarget();
        }
    }

    private void TrackTarget() {
        if (!beeAgent.pathPending) {
            beeAgent.speed = maxSpeed;
            beeAgent.angularSpeed = maxAngularSpeed;

            if (NavMesh.CalculatePath(transform.position, targetTransform.position, NavMesh.AllAreas, path)) {
                //beeAgent.SetDestination(targetTransform.position);
                beeAgent.SetPath(path);

                for (int i = 0; i < path.corners.Length - 1; i++)
                    Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            }
        }
    }

    private void ZoomForward() {
        rb.velocity = transform.forward * zoomSpeed;
    }

    private void Attack(GameObject target) {
        isTracking = false;
        rb.useGravity = false;
        beeAgent.enabled = false;
        StartCoroutine(TrackThenZoom(target));

        los.OnTargetInLineOfSight -= Attack;
    }

    private IEnumerator TrackThenZoom(GameObject target) {
        float timer = 0.0f;

        while (timer <= trackForSeconds) {
            timer += Time.deltaTime;
            Vector3 targetPoint = new Vector3(target.transform.position.x, 
                transform.position.y, target.transform.position.z) - transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * attackTurnSpeed);
            yield return null;
        }

        ZoomForward();
    }

    private void OnDisable() {
        los.OnTargetInLineOfSight -= Attack;
    }
}
