using System;

namespace ObjectPool
{
    public interface IPoolable
    {
        event Action SpawnEvent;
        event Action DeSpawnEvent;
        void Spawn();
        void DeSpawn();
    }
}