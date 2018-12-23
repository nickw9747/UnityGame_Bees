using UnityEngine;
using System.Collections.Generic;

public class ObjectPool<T> : Singleton<ObjectPool<T>> where T : Component {

    [SerializeField]
    private T prefab = null;
    [SerializeField]
    private int initialPoolSize = 5;

    private Queue<T> pool = new Queue<T>();

    private void Start() {
        AddNewToPool(initialPoolSize);
    }

    public T Get() {
        if (pool.Count == 0) {
            AddNewToPool(1);
        }
        return pool.Dequeue();
    }

    public void ReturnToPool(T objectToReturn) {
        objectToReturn.gameObject.SetActive(false);
        pool.Enqueue(objectToReturn);
    }

    private void AddNewToPool(int count) {
        for (int i = 0; i < count; i++) {
            T newObject = Instantiate(prefab, transform);
            newObject.gameObject.SetActive(false);
            pool.Enqueue(newObject);
        }
    }
}
