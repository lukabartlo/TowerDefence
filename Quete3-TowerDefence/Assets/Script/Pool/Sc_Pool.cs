using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class Sc_Pool<T> : Sc_IPool<T> where T : class, Sc_IPooledObject<T>
{
    private readonly Func<T> _createFunc;
    private readonly Action<T> _onGetFunc;
    private readonly Action<T> _onReleaseFunc;
    private readonly Stack<T> _pooledObjects;

    private int _aliveObjectsCount;

    public int PooledObjectCount => _pooledObjects.Count;

    public int AliveObjectCount => _aliveObjectsCount;

    public Sc_Pool(Func<T> createFunc, int capacity = 50, int preAllocationCount = 0) : this(createFunc, null, null, capacity, preAllocationCount) { }

    public Sc_Pool(Func<T> createFunc, Action<T> onGetFunc, Action<T> onReleaseFunc, int capacity = 50, int preAllocationCount = 0)
    {
        Assert.IsNotNull(createFunc, "The object creation function can't be null.");
        Assert.IsTrue(capacity >= 1, "The capacity of the pool must be greater than one.");
        Assert.IsTrue(preAllocationCount >= 0, "The pre-allocation count of the pool must be greater than or equal to 0.");

        _pooledObjects = new Stack<T>(capacity);
        _createFunc = createFunc;
        _onGetFunc = onGetFunc;
        _onReleaseFunc = onReleaseFunc;
        _aliveObjectsCount = 0;
        PreAllocatePooledObject(preAllocationCount);
    }

    public void PreAllocatePooledObject(int preAllocationCount)
    {
        for (int i = 0; i < preAllocationCount; i++)
        {
            T pooledObject = InstantiatePoolObject();
            _aliveObjectsCount++;
            Release(pooledObject);
        }
    }

    public T Get()
    {
        T pooledObject;
        if (_pooledObjects.Count > 0)
        {
            pooledObject = _pooledObjects.Pop();
        }
        else
        {
            pooledObject = InstantiatePoolObject();
        }
        _aliveObjectsCount++;
        _onGetFunc?.Invoke(pooledObject);
        return pooledObject;
    }

    private T InstantiatePoolObject()
    {
        T pooledObject = _createFunc.Invoke();
        Assert.IsNotNull(pooledObject, "The object to release can't be null.");
        pooledObject.SetPool(this);
        return pooledObject;
    }


    public void Release(T pooledObject)
    {
        Assert.IsNotNull(pooledObject, "The object to release can't be null.");
        _pooledObjects.Push(pooledObject);
        _aliveObjectsCount--;
        _onReleaseFunc?.Invoke(pooledObject);
    }
}