using System;
using Asteroids.Scripts.ECS.UnityComponents;
using LeoEcsPhysics;
using Leopotam.Ecs;
using Unity.VisualScripting;

namespace Asteroids.Scripts.ECS.Components
{
    public class DamageSystem : IEcsRunSystem
    {
        private EcsFilter<OnTriggerEnter2DEvent> filter;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var collider = ref filter.Get1(i);

                if (!collider.senderGameObject.TryGetComponent(out BaseView sender)) continue;
                if (!sender.Entity.Has<Damage>()) continue;
                ref var damage = ref sender.Entity.Get<Damage>();
                if (collider.collider2D.gameObject.TryGetComponent(out BaseView target))
                {
                    if (target.Entity.IsAlive() && target.Entity.Has<HealthComponent>())
                    {
                        ref var health = ref target.Entity.Get<HealthComponent>();
                        health.hp -= damage.damage;
                    }
                }
            }
        }
    }
}