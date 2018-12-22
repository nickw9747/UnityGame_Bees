using UnityEngine;
using System.Collections;

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

    private ObjectPool<Bee> beePool;

    private void Start() {
        beePool = new ObjectPool<Bee>(beePrefab, initialBeePoolSize, gameObject);

        StartCoroutine(SpawnOnTimer());
    }

    private void SpawnBee() {
        Bee newBee = beePool.Get();
        newBee.gameObject.SetActive(true);
        newBee.TargetTransform = target;
    }

    private IEnumerator SpawnOnTimer() {
        while (spawn) {
            yield return new WaitForSeconds(spawnTimer);
            SpawnBee();
        }
    }
}
