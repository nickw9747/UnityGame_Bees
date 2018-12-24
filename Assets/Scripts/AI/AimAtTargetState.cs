using UnityEngine;

public class AimAtTargetState : AIState {
    private Transform transform;
    private Transform target;
    private float turnSpeed;

    public AimAtTargetState(Transform transform, Transform target, float turnSpeed) {
        this.transform = transform;
        this.target = target;
        this.turnSpeed = turnSpeed;
    }

    public override void EnterState() {
        // Play alarm animation
    }

    public override void ExitState() {
        
    }

    public override void Tick() {
        transform.SmoothRotate(target.position, turnSpeed, y: false);
    }
}
