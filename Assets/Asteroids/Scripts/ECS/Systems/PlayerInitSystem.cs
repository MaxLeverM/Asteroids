using System.Collections.Generic;
using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.Services;
using Asteroids.Scripts.ECS.UnityComponents;
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

            var starship = CreateStarshipEntity(spawnedStarship.transform, spawnedStarship);
            var bulletGun = CreateBulletGun(spawnedStarship.transform);
            
            ref var child = ref starship.Get<Child>();
            child.children = new List<EcsEntity>();
            child.children.Add(bulletGun);

            ref var owner = ref bulletGun.Get<Owner>();
            owner.master = starship;
        }

        private EcsEntity CreateStarshipEntity(Transform starshipTransform, BaseView view)
        {
            var player = _world.NewEntity();
            ref var playerComponent = ref player.Get<PlayerComponent>();
            ref var transformComponent = ref player.Get<TransformComponent>();
            ref var movableComponent = ref player.Get<MovableComponent>();
            ref var moveEngineComponent = ref player.Get<MoveEngineComponent>();
            ref var rotationEngineComponent = ref player.Get<RotationEngineComponent>();
            ref var health = ref player.Get<HealthComponent>();
            ref var team = ref player.Get<TeamComponent>();
            player.Get<Score>();

            view.Entity = player;
            transformComponent.transform = starshipTransform;
            movableComponent.maxSpeed = 5f;
            movableComponent.damping = 1f;
            rotationEngineComponent.rotationOffset = 90f;
            moveEngineComponent.force = 10f;
            health.hp = 100f;
            health.maxHp = 100f;
            team.team = TeamID.Player;
            return player;
        }

        private EcsEntity CreateBulletGun(Transform bulletGunTransform)
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

            ref var team = ref bulletGun.Get<TeamComponent>();
            team.team = TeamID.Player;
            
            return bulletGun;
        }
    }
}