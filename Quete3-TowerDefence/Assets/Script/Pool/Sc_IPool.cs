public interface Sc_IPool<T>
{
    int PooledObjectCount { get; }

    int AliveObjectCount { get; }

    T Get();

    void Release(T obj);
}