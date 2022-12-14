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
            ref var transformComponent = ref player.Get<TransformComponent>();
            ref var movableComponent = ref player.Get<MovableComponent>();
            ref var moveEngineComponent = ref player.Get<MoveEngineComponent>();
            ref var rotationEngineComponent = ref player.Get<RotationEngineComponent>();

            var spawnedStarship = GameObject.Instantiate(config.Starship, Vector3.zero, Quaternion.identity);

            transformComponent.transform = spawnedStarship.transform;
            movableComponent.maxSpeed = 10f;
            movableComponent.damping = 1f;
            rotationEngineComponent.rotationOffset = 90f;
            moveEngineComponent.force = 10f;
        }
    }
}