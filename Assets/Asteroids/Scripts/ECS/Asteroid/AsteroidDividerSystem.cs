using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.ObjectPool;
using Asteroids.Scripts.ECS.Services;
using Asteroids.Scripts.ECS.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Scripts.ECS.Systems
{
    public class AsteroidDividerSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter<Fragments, PoolObject, TransformComponent, DestroyEvent> filter;
        private IConfig config;
        private AsteroidSpawnerConfig spawnerConfig;
        
        public void Init()
        {
            spawnerConfig = config.AsteroidSpawnerConfig;
        }
        
        public void Run()
        {
            foreach (var i in filter)
            {
                if (spawnerConfig.AsteroidSpawnData.Count < 2)
                    return;
                
                ref var fragments = ref filter.Get1(i);
                ref var poolObject = ref filter.Get2(i);
                ref var originTransform = ref filter.Get3(i);
                for (int j = 0; j < spawnerConfig.AsteroidSpawnData.Count; j++)
                {
                    if (originTransform.transform.localScale.magnitude >= spawnerConfig.AsteroidSpawnData[j].Scale.magnitude)
                    {
                        SpawnFragments(j + 1, originTransform.transform, poolObject.pool);
                        break;
                    }
                }
            }
        }
        
        private void SpawnFragments(int index, Transform originTransform, ObjectPool<BaseView> pool)
        {
            for (int i = 0; i < spawnerConfig.AsteroidSpawnData[index - 1].PartsCount; i++)
            {
                var velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                velocity *= Random.Range(spawnerConfig.AsteroidSpawnData[index].VelocityRange.x,
                    spawnerConfig.AsteroidSpawnData[index].VelocityRange.y);
                CreateSpawnRequest(originTransform.position, velocity, pool , index);
            }
        }
        
        private void CreateSpawnRequest(Vector3 spawnPosition, Vector2 velocity, ObjectPool<BaseView> pool, int index)
        {

            var requestEntity = _world.NewEntity();
            requestEntity.Replace(new AsteroidSpawnRequest
            {
                asteroidPool = pool, 
                spawnData = spawnerConfig.AsteroidSpawnData[index], 
                velocity = velocity,
                spawnPoint = spawnPosition
            });
        }
    }
}