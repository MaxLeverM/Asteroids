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
            var spawnedStarship = GameObject.Instantiate(config.Starship, Vector3.zero, Quaternion.identity);

            CreateStarshipEntity(spawnedStarship.transform);
            CreateBulletGun(spawnedStarship.transform);
        }

        private void CreateStarshipEntity(Transform starshipTransform)
        {
            var player = _world.NewEntity();
            ref var playerComponent = ref player.Get<PlayerComponent>();
            ref var transformComponent = ref player.Get<TransformComponent>();
            ref var movableComponent = ref player.Get<MovableComponent>();
            ref var moveEngineComponent = ref player.Get<MoveEngineComponent>();
            ref var rotationEngineComponent = ref player.Get<RotationEngineComponent>();

            transformComponent.transform = starshipTransform;
            movableComponent.maxSpeed = 5f;
            movableComponent.damping = 1f;
            rotationEngineComponent.rotationOffset = 90f;
            moveEngineComponent.force = 10f;
        }

        private void CreateBulletGun(Transform bulletGunTransform)
        {
            var bulletGun = _world.NewEntity();
            ref var bulletGunComponent = ref bulletGun.Get<BulletGunComponent>();

            bulletGunComponent.damage = 50;
            bulletGunComponent.velocity = 7.5f;

            ref var rechargeTimeComponent = ref bulletGun.Get<RechargeTimeComponent>();
            rechargeTimeComponent.timeToRecharge = 0.1f;

            ref var inputMainAttackListener = ref bulletGun.Get<InputMainAttackListener>();
            ref var transformComponent = ref bulletGun.Get<TransformComponent>();

            transformComponent.transform = bulletGunTransform;
        }
    }
}