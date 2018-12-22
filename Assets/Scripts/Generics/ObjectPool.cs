using UnityEngine;
using System.Collections.Generic;

public class ObjectPool<T> where T : Component {
    
    public ObjectPool(T objectToPool, int preArmPoolSize, GameObject parentObject) {
        prefab = objectToPool;
        initialPoolSize = preArmPoolSize;
        parent = parentObject;
    }

    private T prefab = null;
    private int initialPoolSize = 5;
    private GameObject parent;
    private Queue<T> pool = new Queue<T>();

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
            T newObject = UnityEngine.Object.Instantiate(prefab, parent.transform);
            newObject.gameObject.SetActive(false);
            pool.Enqueue(newObject);
        }
    }
}
