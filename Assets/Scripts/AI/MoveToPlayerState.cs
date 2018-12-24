using UnityEngine;
using System.Collections;

public class MoveToPlayerState : AIState {
    private BeeMoveController beeMoveController;
    private Transform target;

    public MoveToPlayerState(BeeMoveController moveController, Transform targetTransform) {
        beeMoveController = moveController;
        target = targetTransform;
    }

    public override void OnStateEnter() {
        beeMoveController.EnableTracking();
    }

    public override void OnStateExit() {
        beeMoveController.DisableTracking();
    }

    public override void Tick() {
        beeMoveController.TrackTarget(target.position);
    }
}
