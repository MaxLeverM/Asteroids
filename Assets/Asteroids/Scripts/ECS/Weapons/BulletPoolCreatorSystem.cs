using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.ObjectPool;
using Asteroids.Scripts.ECS.Services;
using Asteroids.Scripts.ECS.Systems;
using Asteroids.Scripts.ECS.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Weapons
{
    public class BulletPoolCreatorSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private IConfig config;
        
        public void Init()
        {
            var poolEntity = _world.NewEntity();
            var container = new GameObject("BulletContainer");
            
            ref var rootTransform = ref poolEntity.Get<TransformComponent>();
            rootTransform.transform = container.transform;

            poolEntity.Get<PoolType<BulletView>>();
            
            ref var poolComponent = ref poolEntity.Get<PoolComponent>();
            poolComponent.ViewPool = new GameViewPool(config.Bullet,
                rootTransform.transform, 10, 50);
        }
    }
}