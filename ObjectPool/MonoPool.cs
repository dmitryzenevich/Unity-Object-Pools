using System;
using ObjectPool.Base;
using UnityEngine;

namespace ObjectPool
{
    public abstract class MonoPool : MonoPool<MonoPoolObject>
    {
        
    }

    public abstract class MonoPool<T> : Pool<T> where T : MonoPoolObject
    {
        private void Awake()
        {
            if (_poolObjects.Count == 0)
            {
                for (var i = 0; i < _poolAmount; i++)
                {
                    var monoPoolObject = Create();
                    monoPoolObject.name = $"{typeof(T).Name} {i}";
                    _poolObjects.Add(monoPoolObject);
                }
            }
        }

        protected override bool IsActive(T poolObject)
        {
            return poolObject.gameObject.activeInHierarchy;
        }

        protected override void SetActive(T poolObject, bool active)
        {
            poolObject.gameObject.SetActive(active);
        }

        public override T Create()
        {
            if (poolObjectPrefab == null)
                throw new NullReferenceException(nameof(poolObjectPrefab));
            
            var monoPoolObject = Instantiate(poolObjectPrefab, Vector3.zero, Quaternion.identity, transform);
            monoPoolObject.gameObject.SetActive(false);
            return monoPoolObject;
        }

        public override T Take()
        {
            var monoPoolObject = base.Take();
            monoPoolObject.gameObject.SetActive(true);

            return monoPoolObject;
        }

        public virtual T Take(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var monoPoolObject = base.Take();
            monoPoolObject.transform.position = position;
            monoPoolObject.transform.rotation = rotation;
            monoPoolObject.transform.parent = parent;
            
            monoPoolObject.gameObject.SetActive(true);
            
            return monoPoolObject;
        }

        public override void Remove(T poolObject)
        {
            base.Remove(poolObject);
            poolObject.gameObject.SetActive(false);
        }
    }
}