using UnityEngine;

public class Bee : MonoBehaviour {
    [SerializeField]
    private Transform targetTransform = null;
    public Transform TargetTransform { get; set; }

    private BeeMoveController beeMoveController;

    private BeeState beeState;

    private void Awake() {
        beeMoveController = GetComponent<BeeMoveController>();

        if (TargetTransform == null) {
            TargetTransform = targetTransform;
        }
    }

    private void OnEnable() {
        beeState = BeeState.Patrolling;
    }

    private void Update() {
        switch (beeState) {
            case BeeState.Patrolling:
                beeMoveController.TrackTarget(TargetTransform.position);
                break;
            case BeeState.Aiming:
                break;
            case BeeState.Attacking:
                break;
            default:
                break;
        }
    }
}

public enum BeeState {
    Patrolling,
    Aiming,
    Attacking
}