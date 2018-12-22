using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private Action<Collider> OnDealtDamage = delegate { };

    private void OnTriggerEnter(Collider colliderHit) {
        DealDamage(colliderHit);
        OnDealtDamage(colliderHit);
    }

    private void DealDamage(Collider colliderHit) {
        Health health = colliderHit.GetComponent<Health>();
        if (health != null) {
            health.TakeDamage(damage); 
        }
    }
}
