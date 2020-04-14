namespace ObjectPool
{
    public interface IPool<T> where T : IPoolable
    {
        T Create();
        T Take();
        void Remove(T poolObject);
    }
}