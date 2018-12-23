using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    public Action<DamageOnHit, Collider> OnDealtDamage = delegate { };

    private void OnTriggerEnter(Collider colliderHit) {
        DealDamage(colliderHit);
        OnDealtDamage(this, colliderHit);
    }

    private void DealDamage(Collider colliderHit) {
        Health health = colliderHit.GetComponent<Health>();
        if (health != null) {
            health.TakeDamage(damage); 
        }
    }
}
