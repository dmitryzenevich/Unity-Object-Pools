using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectPool.Base
{
    public abstract class Pool<T> : MonoBehaviour, IPool<T> where T : class, IPoolable
    {
        [SerializeField] private protected int _poolAmount;
        
        [SerializeField] private protected T poolObjectPrefab;

        protected internal List<T> _poolObjects = new List<T>();

        protected abstract bool IsActive(T poolObject);

        protected abstract void SetActive(T poolObject, bool active);
        
        public abstract T Create();

        public virtual T Take()
        {
            if (_poolObjects.Count == 0)
                _poolObjects.Add(Create());

            var poolObject = _poolObjects.FirstOrDefault(o => IsActive(o) == false);

            if (poolObject != null)
            {
                poolObject.Spawn();
                return poolObject;
            }
            
            var monoPoolObject = Create();
            _poolObjects.Add(monoPoolObject);
            monoPoolObject.Spawn();
            return monoPoolObject;
        }

        public virtual void Remove(T poolObject)
        {
            if (_poolObjects.Contains(poolObject))
            {
                if (IsActive(poolObject))
                {
                    poolObject.DeSpawn();
                    SetActive(poolObject, false);
                }
            }
            else
            {
                poolObject.DeSpawn();
                SetActive(poolObject, false);
                _poolObjects.Add(poolObject);
            }
        }
    }
}