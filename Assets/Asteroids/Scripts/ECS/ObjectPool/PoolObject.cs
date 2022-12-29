using Asteroids.Scripts.ECS.UnityComponents;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Scripts.ECS.ObjectPool
{
    public struct PoolObject
    {
        public BaseView poolObject;
        public ObjectPool<BaseView> pool;
    }
}