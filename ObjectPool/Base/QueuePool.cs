using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool.Base
{
    public abstract class QueuePool<T> : MonoBehaviour, IPool<T> where T : class, IPoolable
    {
        [SerializeField] private protected int _poolAmount;
        
        private protected LinkedList<T> _poolObjects = new LinkedList<T>();

        public abstract T Create();

        public virtual T Take()
        {
            if (_poolObjects.Count == 0)
                _poolObjects.AddFirst(new LinkedListNode<T>(Create()));

            var poolObjectLastNode = _poolObjects.Last;
            if (poolObjectLastNode == null)
                return default(T);

            _poolObjects.RemoveLast();
            _poolObjects.AddFirst(poolObjectLastNode);
            
            poolObjectLastNode.Value.Spawn();
            return poolObjectLastNode.Value;
        }

        public virtual void Remove(T poolObject)
        {
            if (_poolObjects.Remove(poolObject))
            {
                poolObject.DeSpawn();
                _poolObjects.AddFirst(poolObject);
            }
        }
    }
}