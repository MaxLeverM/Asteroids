using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.Services;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private IConfig config;
        
        public void Init()
        {
            var player = _world.NewEntity();
            ref var playerComponent = ref player.Get<PlayerComponent>();
            ref var movableComponent = ref player.Get<MovableComponent>();
            ref var spaceEngineComponent = ref player.Get<SpaceEngineComponent>();

            var spawnedStarship = GameObject.Instantiate(config.Starship, Vector3.zero, Quaternion.identity);

            movableComponent.transform = spawnedStarship.transform;
            movableComponent.maxSpeed = 10f;
            movableComponent.damping = 1f;
            spaceEngineComponent.rotationOffset = 90f;
            spaceEngineComponent.force = 10f;
        }
    }
}