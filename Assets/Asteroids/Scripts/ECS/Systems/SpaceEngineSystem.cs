using System;
using Asteroids.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Systems
{
    public class SpaceEngineSystem : IEcsRunSystem
    {
        private EcsFilter<SpaceEngineComponent, MovableComponent> engineFilter;

        public void Run()
        {
            foreach (var i in engineFilter)
            {
                ref var spaceEngine = ref engineFilter.Get1(i);
                ref var movable = ref engineFilter.Get2(i);

                Move(ref movable, ref spaceEngine);
                LookAtPoint(ref movable, ref spaceEngine);
            }
        }

        public void Move(ref MovableComponent movableComponent, ref SpaceEngineComponent spaceEngineComponent) //TODO Split this on two systems
        {
            var temp = spaceEngineComponent.moveDirection * (Time.deltaTime * spaceEngineComponent.force);
            movableComponent.velocity += new Vector2(temp.x, temp.y);
        }

        public void LookAtPoint(ref MovableComponent movableComponent, ref SpaceEngineComponent spaceEngineComponent)
        {
            var diff = spaceEngineComponent.lookAtPosition - movableComponent.transform.position;
            diff.Normalize();
            float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            movableComponent.transform.rotation = Quaternion.Euler(0f, 0f, z - spaceEngineComponent.rotationOffset);
        }
    }
}