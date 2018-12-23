using UnityEngine;
using System.Collections;
using System;

public class Bee : MonoBehaviour {
    public Transform TargetTransform { get; set; }

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

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        beeMoveController = GetComponent<BeeMoveController>();
        lineOfSight = GetComponent<LineOfSight>();
        damageOnHit = GetComponent<DamageOnHit>();
    }

    private void OnEnable() {
        rb.velocity = Vector3.zero;
        damageOnHit.OnDealtDamage += DealtDamage;
    }

    private void DealtDamage(DamageOnHit damageOnHit, Collider colliderHit) {
        OnDisableBee(this);
    }

    private void OnDisable() {
        damageOnHit.OnDealtDamage -= DealtDamage;
    }

    private void StartAiming() {
        beeMoveController.DisableTracking();
        StartCoroutine(AimAtTarget());
    }

    private void Attack() {
        rb.velocity = transform.forward * attackMoveSpeed;
    }

    public void Patrol() {
        beeMoveController.EnableTracking();
        StartCoroutine(PatrolUntilTargetDetected());
    }

    private IEnumerator AimAtTarget() {
        float timer = 0.0f;
        while (timer <= aimTimer) {
            transform.SmoothRotate(TargetTransform.position, turnSpeed, y: false);
            timer += Time.deltaTime;
            yield return null;
        }

        Attack();
    }

    private IEnumerator PatrolUntilTargetDetected() {
        bool targetDetected = false;
        lineOfSight.OnTargetInLineOfSight += TargetFound;

        void TargetFound(GameObject target) {
            targetDetected = true;
            TargetTransform = target.transform;
            lineOfSight.OnTargetInLineOfSight -= TargetFound;
        }

        while (!targetDetected) {
            beeMoveController.TrackTarget(TargetTransform.position);
            yield return null;
        }

        StartAiming();
    }
}
