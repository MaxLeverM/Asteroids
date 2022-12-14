﻿using System;
using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.Services;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Systems
{
    public class BulletSpawnSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;
        private GameObject bulletContainer;
        private IConfig config;
        private EcsFilter<TransformComponent, BulletGunComponent, FireEvent> fireGuns;

        public void Init()
        {
            bulletContainer = new GameObject("BulletContainer");
        }

        public void Run()
        {
            foreach (var i in fireGuns)
            {
                ref var gunTransform = ref fireGuns.Get1(i);
                ref var gunComponent = ref fireGuns.Get2(i);
                
                var bulletView = GameObject.Instantiate(config.Bullet, gunTransform.transform.position, Quaternion.identity);
                bulletView.transform.SetParent(bulletContainer.transform);

                var bullet = _world.NewEntity();
                ref var bulletTransform = ref bullet.Get<TransformComponent>();
                bulletTransform.transform = bulletView.transform;

                ref var movable = ref bullet.Get<MovableComponent>();
                movable.damping = 1;
                movable.maxSpeed = 10;
                movable.velocity = gunTransform.transform.up * gunComponent.velocity;

                ref var destroyTimer = ref bullet.Get<DestroyTimeComponent>();
                destroyTimer.timeToDestroy = 5f;
            }
        }
    }
}