using UnityEngine;
using System.Collections;
using System;

public class Health : MonoBehaviour {

    public bool IsDead { get; private set; }
    public Action OnDeath;

    [SerializeField]
    private int maxHealth = 10;
    public int CurrentHealth { get; private set; }

    private void Awake() {
        CurrentHealth = maxHealth;
        IsDead = false;
    }

    public void TakeDamage(int damageAmount) {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0) {
            IsDead = true;
            OnDeath?.Invoke();
        }
    }
}
