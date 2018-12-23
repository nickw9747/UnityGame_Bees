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
    private LineOfSight lineOfSight;
    private DamageOnHit damageOnHit;

    public Action<Bee> OnDisableBee = delegate { };

    private BeeState beeState;
    public BeeState CurrentBeeState {
        get {
            return beeState;
        }
        set {
            if (value != beeState) {
                beeState = value;
                switch (value) {
                    case BeeState.Patrolling:
                        beeMoveController.StartTracking();
                        rb.velocity = Vector3.zero;
                        break;
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
        lineOfSight = GetComponent<LineOfSight>();
        damageOnHit = GetComponent<DamageOnHit>();
    }

    private void OnEnable() {
        CurrentBeeState = BeeState.Patrolling;

        lineOfSight.OnTargetInLineOfSight += StartAiming;
        damageOnHit.OnDealtDamage += DealtDamage;
    }

    private void DealtDamage(DamageOnHit damageOnHit, Collider colliderHit) {
        OnDisableBee(this);
    }

    private void OnDisable() {
        lineOfSight.OnTargetInLineOfSight -= StartAiming;
        damageOnHit.OnDealtDamage -= DealtDamage;
    }

    private void StartAiming(GameObject target) {
        targetTransform = target.transform;
        beeMoveController.StopTracking();
        lineOfSight.OnTargetInLineOfSight -= StartAiming;
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