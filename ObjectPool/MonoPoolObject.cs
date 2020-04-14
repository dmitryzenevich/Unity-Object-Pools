using System;
using UnityEngine;

namespace ObjectPool
{
    [DisallowMultipleComponent]
    [Serializable]
    public abstract class MonoPoolObject : MonoBehaviour, IPoolable
    {
        public abstract bool IsActive { get; set; }
        
        public abstract event Action SpawnEvent;
        public abstract event Action DeSpawnEvent;
        
        public abstract void Spawn();
        public abstract void DeSpawn();
    }
}