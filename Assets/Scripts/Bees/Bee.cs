using UnityEngine;
using System.Collections;
using System;

public class Bee : MonoBehaviour {
    [SerializeField]
    private Transform targetTransform = null;

    [SerializeField]
    private float turnSpeed = 8.0f;
    [SerializeField]
    private float aimTimer = 1.5f;
    [SerializeField]
    private float attackMoveSpeed = 6.0f;

    private BeeMoveController beeMoveController;
    private Rigidbody rb;
    private LineOfSight los;

    private BeeState beeState;
    public BeeState CurrentBeeState {
        get {
            return beeState;
        }
        set {
            if (value != beeState) {
                beeState = value;
                switch (value) {
                    case BeeState.Aiming:
                        StartCoroutine(AimTimer());
                        break;
                    case BeeState.Attack:
                        Attack();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        beeMoveController = GetComponent<BeeMoveController>();
        los = GetComponent<LineOfSight>();
    }

    private void OnEnable() {
        CurrentBeeState = BeeState.Patrolling;

        los.OnTargetInLineOfSight += StartAiming;
    }

    private void OnDisable() {
        los.OnTargetInLineOfSight -= StartAiming;
    }

    private void StartAiming(GameObject target) {
        targetTransform = target.transform;
        beeMoveController.StopTracking();
        los.OnTargetInLineOfSight -= StartAiming;
        CurrentBeeState = BeeState.Aiming;
    }

    private void Update() {
        switch (CurrentBeeState) {
            case BeeState.Patrolling:
                beeMoveController.TrackTarget(targetTransform.position);
                break;
            case BeeState.Aiming:
                transform.SmoothRotate(targetTransform.position, turnSpeed, y: false);
                break;
            case BeeState.Attack:
                break;
            case BeeState.Attacking:
                break;
            default:
                break;
        }
    }

    private void Attack() {
        rb.velocity = transform.forward * attackMoveSpeed;
        CurrentBeeState = BeeState.Attacking;
    }

    private IEnumerator AimTimer() {
        yield return new WaitForSeconds(aimTimer);
        CurrentBeeState = BeeState.Attack;
    }

    public void SetTarget(Transform target) {
        targetTransform = target;
    }
}

public enum BeeState {
    Patrolling,
    Aiming,
    Attack,
    Attacking
}