using UnityEngine;
using System.Collections;

public class PatrolState : AIState {
    private BeeMoveController beeMoveController;

    public PatrolState(BeeMoveController moveController) {
        beeMoveController = moveController;
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
