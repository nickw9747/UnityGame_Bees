using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BeeSpawner : MonoBehaviour {

    [SerializeField]
    private Bee beePrefab = null;
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private int initialBeePoolSize = 5;
    [SerializeField]
    private bool spawn = true;
    [SerializeField]
    private float spawnTimer = 5.0f;

    private void Start() {
        StartCoroutine(SpawnOnTimer());
    }

    private void SpawnBee() {
        Bee newBee = BeeObjectPool.Instance.Get();
        newBee.gameObject.SetActive(true);
        newBee.SetTarget(target);
        newBee.transform.ResetTransform();

        newBee.OnDisableBee += ReturnBeeToPool;
    }

    private void ReturnBeeToPool(Bee bee) {
        bee.OnDisableBee -= ReturnBeeToPool;
        BeeObjectPool.Instance.ReturnToPool(bee);
    }

    private IEnumerator SpawnOnTimer() {
        while (spawn) {
            yield return new WaitForSeconds(spawnTimer);
            SpawnBee();
        }
    }
}
