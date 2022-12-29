using System;
using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.Destroy;
using Asteroids.Scripts.ECS.ObjectPool;
using Asteroids.Scripts.ECS.Services;
using Asteroids.Scripts.ECS.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Asteroids.Scripts.ECS.Systems
{
    public class BulletSpawnSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private IConfig config;
        private EcsFilter<TransformComponent, BulletGunComponent, FireEvent> fireGuns;
        private EcsFilter<TransformComponent, PoolComponent, PoolType<BulletView>> bulletPool;

        public void Run()
        {
            foreach (var i in fireGuns)
            {
                ref var gunTransform = ref fireGuns.Get1(i);
                ref var gunComponent = ref fireGuns.Get2(i);

                ref var pool = ref bulletPool.Get2(0);//fix index
                if (pool.ViewPool.Pool.Get() is not BulletView bulletView) return;
                
                var bullet = _world.NewEntity();
                bulletView.Entity = bullet;
                ref var bulletTransform = ref bullet.Get<TransformComponent>();
                bulletTransform.transform = bulletView.transform;
                bulletTransform.transform.position = gunTransform.transform.position;

                ref var movable = ref bullet.Get<MovableComponent>();
                movable.damping = 1;
                movable.maxSpeed = 10;
                movable.velocity = gunTransform.transform.up * gunComponent.velocity;

                ref var destroyTimer = ref bullet.Get<DestroyTimeComponent>();
                destroyTimer.timeToDestroy = 2.5f;

                ref var damage = ref bullet.Get<Damage>();
                damage.damage = 25f;

                ref var collider2D = ref bullet.Get<Collider2DComponent>();
                collider2D.Collider = bulletView.BulletCollider;
                
                ref var poolObject = ref bullet.Get<PoolObject>();
                poolObject.poolObject = bulletView;
                poolObject.pool = pool.ViewPool.Pool;

                if (fireGuns.GetEntity(i).Has<TeamComponent>())
                {
                    ref var team = ref bullet.Get<TeamComponent>();
                    team.team = fireGuns.GetEntity(i).Get<TeamComponent>().team;
                }

                if (fireGuns.GetEntity(i).Has<Owner>())
                {
                    ref var owner = ref bullet.Get<Owner>();
                    owner.master = fireGuns.GetEntity(i).Get<Owner>().master;
                }

                bullet.Get<DestroyOnCollide>();
            }
        }
    }
}