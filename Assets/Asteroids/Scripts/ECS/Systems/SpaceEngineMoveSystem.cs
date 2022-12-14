using System;
using Asteroids.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Systems
{
    public class SpaceEngineMoveSystem : IEcsRunSystem
    {
        private EcsFilter<MoveEngineComponent, MovableComponent> engineFilter;

        public void Run()
        {
            foreach (var i in engineFilter)
            {
                ref var spaceEngine = ref engineFilter.Get1(i);
                ref var movable = ref engineFilter.Get2(i);

                Move(ref movable, ref spaceEngine);
            }
        }

        private void Move(ref MovableComponent movableComponent, ref MoveEngineComponent moveEngineComponent)
        {
            var temp = moveEngineComponent.moveDirection * (Time.deltaTime * moveEngineComponent.force);
            movableComponent.velocity += new Vector2(temp.x, temp.y);
        }
    }
}