public interface Sc_IPooledObject<T> where T : class, Sc_IPooledObject<T>
{
    public void SetPool(Sc_Pool<T> pool);
}
