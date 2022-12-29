using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.ObjectPool;
using Asteroids.Scripts.ECS.Services;
using Asteroids.Scripts.ECS.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Systems
{
    public class AsteroidPoolCreatorSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private IConfig config;
        private AsteroidSpawnerConfig spawnerConfig;
        public void Init()
        {
            spawnerConfig = config.AsteroidSpawnerConfig;
            
            var asteroidPoolEntity = _world.NewEntity();
            var container = new GameObject("AsteroidContainer");
            
            ref var rootTransform = ref asteroidPoolEntity.Get<TransformComponent>();
            rootTransform.transform = container.transform;

            asteroidPoolEntity.Get<PoolType<AsteroidView>>();
            
            ref var poolComponent = ref asteroidPoolEntity.Get<PoolComponent>();
            poolComponent.ViewPool = new GameViewPool(spawnerConfig.PrefabForSpawn,
                rootTransform.transform, spawnerConfig.StartCapacity, spawnerConfig.MaxCapacity);

            ref var timer = ref asteroidPoolEntity.Get<SpawnTimer>();
        }
    }
}