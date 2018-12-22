using UnityEngine;
using System.Collections;
using System;

public class LineOfSight : MonoBehaviour {

    [SerializeField]
    private float viewRadius = 4.0f;

    [SerializeField]
    private LayerMask targetMask = 1;
    [SerializeField]
    private LayerMask obstacleMask = 1;

    public Action<GameObject> OnTargetInLineOfSight = delegate { };

    private void Update() {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        foreach (var colliderInRange in collidersInRange) {
            Vector3 directionToTarget = (colliderInRange.transform.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, colliderInRange.transform.position);
            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask)) {
                OnTargetInLineOfSight(colliderInRange.gameObject);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

}
