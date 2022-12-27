using Asteroids.Scripts.Core;
using Asteroids.Scripts.Core.Interfaces;
using Asteroids.Scripts.Core.Utilities;
using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.ObjectPool;
using Asteroids.Scripts.ECS.Services;
using Asteroids.Scripts.ECS.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Scripts.ECS.Systems
{
    public class AsteroidSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private IConfig config;
        private AsteroidSpawnerConfig spawnerConfig;
        private EcsFilter<TransformComponent, PoolComponent<AsteroidView>, SpawnTimer> asteroidsPoolFilter;
        private FieldBound fieldBound;

        public void Init()
        {
            spawnerConfig = config.AsteroidSpawnerConfig;
        }

        public void Run()
        {
            foreach (var i in asteroidsPoolFilter)
            {
                ref var spawnTimer = ref asteroidsPoolFilter.Get3(i);
                
                spawnTimer.currentTime += Time.deltaTime;
                if (spawnTimer.currentTime >= spawnTimer.targetTime)
                {
                    ref var rootTransform = ref asteroidsPoolFilter.Get1(i);
                    ref var poolComponent = ref asteroidsPoolFilter.Get2(i);

                    spawnTimer.currentTime = 0f;
                    spawnTimer.targetTime = Random.Range(spawnerConfig.SpawnInterval.x, spawnerConfig.SpawnInterval.y);
                    
                    if (poolComponent.ObjectPool.Pool.CountActive < spawnerConfig.MaxCapacity)
                        Spawn(poolComponent.ObjectPool.Pool, rootTransform.transform);
                    if (poolComponent.ObjectPool.Pool.CountActive == 1)
                    {
                        for (int j = 0; j < spawnerConfig.StartCapacity - 1; j++)
                        {
                            Spawn(poolComponent.ObjectPool.Pool, rootTransform.transform);
                        }
                    }
                }
            }
        }

        private void Spawn(ObjectPool<AsteroidView> asteroidPool, Transform root)
        {
            if (spawnerConfig.AsteroidSpawnData.Count < 1)
                return;

            var pointOnEdge =
                ProjectMath.RandomPointOutsideRectangle(fieldBound.ExtremePoint.x * 2, fieldBound.ExtremePoint.y * 2);
            var spawnPosition = new Vector2(pointOnEdge.x - fieldBound.ExtremePoint.x,
                pointOnEdge.y - fieldBound.ExtremePoint.y);

            var velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            velocity *= Random.Range(spawnerConfig.AsteroidSpawnData[0].VelocityRange.x,
                spawnerConfig.AsteroidSpawnData[0].VelocityRange.y);

            Spawn(asteroidPool, root, spawnPosition, velocity, spawnerConfig.AsteroidSpawnData[0]);
        }

        private void Spawn(ObjectPool<AsteroidView> asteroidPool, Transform root, Vector2 spawnPoint, Vector2 velocity,
            AsteroidSpawnData spawnData)
        {
            var asteroidView = asteroidPool.Get();
            asteroidView.transform.SetParent(root);

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

            ref var score = ref asteroid.Get<Score>();
            score.points = spawnData.ScoreForDestroy;

            ref var collider = ref asteroid.Get<Collider2DComponent>();
            collider.Collider = asteroidView.AsteroidCollider;

            ref var poolObject = ref asteroid.Get<PoolObject<AsteroidView>>();
            poolObject.poolObject = asteroidView;
            poolObject.pool = asteroidPool;
        }
    }
}