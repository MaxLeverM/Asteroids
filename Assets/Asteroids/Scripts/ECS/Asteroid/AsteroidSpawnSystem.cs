using System;
using Asteroids.Scripts.Core;
using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.ObjectPool;
using Asteroids.Scripts.ECS.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Scripts.ECS.Systems
{
    public class AsteroidSpawnSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<AsteroidSpawnRequest> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var request = ref filter.Get1(i);
                Spawn(request.asteroidPool, request.spawnPoint, request.velocity, request.spawnData);
                filter.GetEntity(i).Destroy();
            }
        }

        private void Spawn(ObjectPool<BaseView> asteroidPool, Vector2 spawnPoint, Vector2 velocity, AsteroidSpawnData spawnData)
        {
            if (asteroidPool.Get() is not AsteroidView asteroidView) return;

            var asteroid = _world.NewEntity();
            asteroidView.Entity = asteroid;

            ref var asteroidTransform = ref asteroid.Get<TransformComponent>();
            asteroidTransform.transform = asteroidView.transform;
            asteroidTransform.transform.position = spawnPoint;
            asteroidTransform.transform.localScale = spawnData.Scale;

            ref var movable = ref asteroid.Get<MovableComponent>();
            movable.damping = 1f;
            movable.maxSpeed = spawnData.VelocityRange.y;
            movable.velocity = velocity;

            ref var health = ref asteroid.Get<HealthComponent>();
            health.hp = spawnData.Hp;
            health.maxHp = spawnData.Hp;

            ref var score = ref asteroid.Get<ScoreReward>();
            score.points = spawnData.ScoreForDestroy;

            ref var collider = ref asteroid.Get<Collider2DComponent>();
            collider.Collider = asteroidView.AsteroidCollider;

            ref var poolObject = ref asteroid.Get<PoolObject>();
            poolObject.poolObject = asteroidView;
            poolObject.pool = asteroidPool;

            ref var fragments = ref asteroid.Get<Fragments>();
            fragments.partsCount = spawnData.PartsCount;

            ref var damage = ref asteroid.Get<Damage>();
            damage.damage = 100f;

            ref var team = ref asteroid.Get<TeamComponent>();
            team.team = TeamID.Enemy;
        }
    }
}