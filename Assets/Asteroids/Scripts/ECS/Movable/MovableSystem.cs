using Asteroids.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Systems
{
    public class MovableSystem : IEcsRunSystem
    {
        private EcsFilter<TransformComponent, MovableComponent> movableFilter = null;
        
        public void Run()
        {
            foreach (var i in movableFilter)
            {
                ref var transformComponent = ref movableFilter.Get1(i);
                ref var movable = ref movableFilter.Get2(i);

                transformComponent.transform.position =
                    (Vector2) transformComponent.transform.position + (movable.velocity * Time.deltaTime);

                if (movable.velocity.magnitude > movable.maxSpeed)
                {
                    movable.velocity.Normalize();
                    movable.velocity *= movable.maxSpeed;
                }

                movable.velocity *= movable.damping;
            }
        }
    }
}