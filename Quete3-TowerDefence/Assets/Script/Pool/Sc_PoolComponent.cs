using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

public class Sc_PoolComponent<T> : Sc_IPool<T> where T : Component, Sc_IPooledObject<T>
{
    private Sc_Pool<T> _pool;

    public int PooledObjectCount => _pool.PooledObjectCount;

    public int AliveObjectCount => _pool.AliveObjectCount;

    public Sc_PoolComponent(GameObject prefab, int capacity = 50, int preAllocationCount = 0)
    {
        Assert.IsNotNull(prefab, "The prefab cannot be null.");
        _pool = new Sc_Pool<T>(() =>
        {
            GameObject gameObject = Object.Instantiate(prefab);
            return gameObject.GetComponent<T>();
        },
        (pooledObject) => { pooledObject.gameObject.SetActive(true); },
        (pooledObject) => { pooledObject.gameObject.SetActive(false); },
        capacity,
        preAllocationCount);
    }

    public T Get()
    {
        return _pool.Get();
    }

    public void Release(T obj)
    {
        _pool.Release(obj);
    }
}