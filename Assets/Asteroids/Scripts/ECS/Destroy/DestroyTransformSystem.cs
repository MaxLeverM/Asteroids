using System;
using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.ObjectPool;
using Asteroids.Scripts.ECS.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Asteroids.Scripts.ECS.Systems
{
    public class DestroyTransformSystem : IEcsRunSystem
    {
        private EcsFilter<TransformComponent, DestroyEvent>.Exclude<PoolObject> destroyFilter;

        public void Run()
        {
            foreach (var i in destroyFilter)
            {
                var transformComponent = destroyFilter.Get1(i);
                Object.Destroy(transformComponent.transform.gameObject);
            }
        }
    }
}