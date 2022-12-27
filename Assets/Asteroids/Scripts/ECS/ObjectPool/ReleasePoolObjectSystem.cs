using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.ObjectPool
{
    public class ReleasePoolObjectSystem<T> : IEcsRunSystem where T : MonoBehaviour
    {
        private EcsFilter<PoolObject<T>, DestroyEvent> destroyFilter;

        public void Run()
        {
            foreach (var i in destroyFilter)
            {
                var poolObject = destroyFilter.Get1(i);

                poolObject.pool.Release(poolObject.poolObject);
            }
        }
    }
}