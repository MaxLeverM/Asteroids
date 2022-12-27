using UnityEngine;

namespace Asteroids.Scripts.ECS.ObjectPool
{
    public struct PoolComponent<T> where T: MonoBehaviour
    {
        public GameObjectPool<T> ObjectPool;
    }
}