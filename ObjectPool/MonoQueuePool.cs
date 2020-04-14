using System;
using ObjectPool.Base;
using UnityEngine;

namespace ObjectPool
{
    public abstract class MonoQueuePool : MonoQueuePool<MonoPoolObject>
    {
        
    }

    public abstract class MonoQueuePool<T> : QueuePool<T> where T : MonoPoolObject
    {
        public T poolObject;

        private void Awake()
        {
            if (_poolObjects.Count == 0)
            {
                for (var i = 0; i < _poolAmount; i++)
                {
                    var monoPoolObject = Create();
                    monoPoolObject.name = $"{typeof(T).Name} {i}";
                    _poolObjects.AddFirst(monoPoolObject);
                }
            }
        }

        public override T Create()
        {
            if (poolObject == null)
                throw new NullReferenceException(nameof(poolObject));
            
            var monoPoolObject = Instantiate(poolObject, Vector3.zero, Quaternion.identity, transform);
            monoPoolObject.gameObject.SetActive(false);
            return monoPoolObject;
        }

        public override T Take()
        {
            var monoPoolObject = base.Take();
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