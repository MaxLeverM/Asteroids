using System;
using Asteroids.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Systems
{
    public class BulletSpawnSystem : IEcsRunSystem, IEcsInitSystem
    {
        private GameObject bulletContainer;
        private EcsFilter<BulletGunComponent, FireEvent> firedGuns;

        public void Init()
        {
            bulletContainer = new GameObject("BulletContainer");
        }

        public void Run()
        {
            foreach (var i in firedGuns)
            {
                var ent = firedGuns.GetEntity(i);
                
            }
        }
    }
}