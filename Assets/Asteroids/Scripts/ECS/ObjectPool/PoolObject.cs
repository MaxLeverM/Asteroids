using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Scripts.ECS.ObjectPool
{
    public struct PoolObject<T> where T: MonoBehaviour
    {
        public T poolObject;
        public ObjectPool<T> pool;
    }
}