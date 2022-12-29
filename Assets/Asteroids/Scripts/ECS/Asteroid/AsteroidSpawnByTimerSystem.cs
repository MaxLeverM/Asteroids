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
    public class AsteroidSpawnByTimerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private IConfig config;
        private AsteroidSpawnerConfig spawnerConfig;
        private EcsFilter<PoolComponent, SpawnTimer, PoolType<AsteroidView>> asteroidsPoolFilter;
        private FieldBound fieldBound;

        public void Init()
        {
            spawnerConfig = config.AsteroidSpawnerConfig;
        }

        public void Run()
        {
            foreach (var i in asteroidsPoolFilter)
            {
                ref var spawnTimer = ref asteroidsPoolFilter.Get2(i);
                
                spawnTimer.currentTime += Time.deltaTime;
                if (spawnTimer.currentTime >= spawnTimer.targetTime)
                {
                    ref var poolComponent = ref asteroidsPoolFilter.Get1(i);

                    spawnTimer.currentTime = 0f;
                    spawnTimer.targetTime = Random.Range(spawnerConfig.SpawnInterval.x, spawnerConfig.SpawnInterval.y);
                    
                    if (poolComponent.ViewPool.Pool.CountActive < spawnerConfig.MaxCapacity)
                        Spawn(poolComponent.ViewPool.Pool);
                    if (poolComponent.ViewPool.Pool.CountActive <= spawnerConfig.StartCapacity/2)
                    {
                        for (int j = 0; j < spawnerConfig.StartCapacity - 1; j++)
                        {
                            Spawn(poolComponent.ViewPool.Pool);
                        }
                    }
                }
            }
        }

        private void Spawn(ObjectPool<BaseView> asteroidPool)
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

            var requestEntity = _world.NewEntity();
            requestEntity.Replace(new AsteroidSpawnRequest
            {
                asteroidPool = asteroidPool, 
                spawnData = spawnerConfig.AsteroidSpawnData[0], 
                velocity = velocity,
                spawnPoint = spawnPosition
            });
        }
    }
}