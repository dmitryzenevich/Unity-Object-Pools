using System.Linq;
using UnityEngine;

namespace ObjectPool
{
    public class MonoPoolsManager : MonoBehaviour
    {
        [SerializeField] private MonoPool[] _pools;

        public T GetPool<T>() where T : MonoPool
        {
            return _pools.FirstOrDefault(p => p is T) as T;
        } 
    }
}