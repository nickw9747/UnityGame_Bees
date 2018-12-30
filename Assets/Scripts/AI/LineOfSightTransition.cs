using UnityEngine;

[CreateAssetMenu(menuName = "AI/Transition/LineOfSight")]
public class LineOfSightTransition : Transition {

    [SerializeField]
    private float viewRadius = 4.0f;

    [SerializeField]
    private LayerMask targetMask = 1;
    [SerializeField]
    private LayerMask obstacleMask = 1;

    [SerializeField]
    private Color gizmoColour = Color.blue;

    public override bool CheckTransition(StateController controller) {
        Collider[] collidersInRange = Physics.OverlapSphere(controller.transform.position, viewRadius, targetMask);

        foreach (var colliderInRange in collidersInRange) {
            Vector3 directionToTarget = (colliderInRange.transform.position - controller.transform.position).normalized;
            float distanceToTarget = Vector3.Distance(controller.transform.position, colliderInRange.transform.position);
            if (!Physics.Raycast(controller.transform.position, directionToTarget, distanceToTarget, obstacleMask)) {
                return true;
            }
        }

        return false;
        
    }

    public override void TransitionGizmo(StateController controller) {
        Gizmos.color = gizmoColour;
        Gizmos.DrawWireSphere(controller.transform.position, viewRadius);
    }
}
