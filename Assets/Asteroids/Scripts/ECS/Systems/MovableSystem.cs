using Asteroids.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Systems
{
    public class MovableSystem : IEcsRunSystem
    {
        private EcsFilter<MovableComponent> movableFilter = null;
        
        public void Run()
        {
            foreach (var i in movableFilter)
            {
                ref var fieldObject = ref movableFilter.Get1(i);

                fieldObject.transform.position =
                    (Vector2) fieldObject.transform.position + (fieldObject.velocity * Time.deltaTime);

                if (fieldObject.velocity.magnitude > fieldObject.maxSpeed)
                {
                    fieldObject.velocity.Normalize();
                    fieldObject.velocity *= fieldObject.maxSpeed;
                }

                fieldObject.velocity *= fieldObject.damping;
            }
        }
    }
}