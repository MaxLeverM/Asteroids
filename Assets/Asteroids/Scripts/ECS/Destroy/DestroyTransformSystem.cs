using Asteroids.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Systems
{
    public class DestroyTransformSystem : IEcsRunSystem
    {
        private EcsFilter<TransformComponent, DestroyEvent> destroyFilter;

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