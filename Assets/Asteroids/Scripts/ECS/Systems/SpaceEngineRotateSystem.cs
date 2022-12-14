using Asteroids.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Systems
{
    public class SpaceEngineRotateSystem : IEcsRunSystem
    {
        private EcsFilter<RotationEngineComponent, TransformComponent> engineFilter;

        public void Run()
        {
            foreach (var i in engineFilter)
            {
                ref var rotationEngine = ref engineFilter.Get1(i);
                var transform = engineFilter.Get2(i).transform;
                
                LookAtPoint(ref rotationEngine, transform);
            }
        }

        private void LookAtPoint(ref RotationEngineComponent rotationEngineComponent, Transform transform)
        {
            var diff = rotationEngineComponent.lookAtPosition - transform.position;
            diff.Normalize();
            float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, z - rotationEngineComponent.rotationOffset);
        }
    }
}