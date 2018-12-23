using UnityEngine;
using System.Collections;

public class BeeSpawner : MonoBehaviour {

    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private bool spawn = true;
    private bool spawning = false;
    [SerializeField]
    private float spawnTimer = 5.0f;

    private void Update() {
        if (spawn && !spawning) {
            StartCoroutine(SpawnAfterTimer());
        }
    }

    private void SpawnBee() {
        Bee newBee = BeeObjectPool.Instance.Get();
        newBee.TargetTransform = target;
        newBee.transform.position = transform.position;
        newBee.gameObject.SetActive(true);
        newBee.Patrol();

        newBee.OnDisableBee += ReturnBeeToPool;
    }

    private void ReturnBeeToPool(Bee bee) {
        bee.OnDisableBee -= ReturnBeeToPool;
        BeeObjectPool.Instance.ReturnToPool(bee);
    }

    private IEnumerator SpawnAfterTimer() {
        spawning = true;
        yield return new WaitForSeconds(spawnTimer);
        if (spawn) {
            SpawnBee();
        }
        spawning = false;
    }
}
