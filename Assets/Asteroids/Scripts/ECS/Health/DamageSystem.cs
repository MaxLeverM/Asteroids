using System;
using Asteroids.Scripts.ECS.UnityComponents;
using LeoEcsPhysics;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

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
                if (!collider.collider2D.gameObject.TryGetComponent(out BaseView target)) continue;

                MakeDamage(sender, target);
            }
        }

        private void MakeDamage(BaseView sender, BaseView target)
        {
            if (!sender.Entity.Has<Damage>()) return;
            if (!target.Entity.IsAlive() || !target.Entity.Has<HealthComponent>()) return;
            if (sender.Entity.Has<TeamComponent>() && target.Entity.Has<TeamComponent>()
                                                   && sender.Entity.Get<TeamComponent>().team
                                                       .Equals(target.Entity.Get<TeamComponent>().team)) return;
            if (sender.Entity.Has<Owner>())
            {
                ref var damageOwner = ref target.Entity.Get<FinalDamageOwner>();
                damageOwner.owner = sender.Entity.Get<Owner>().master;
            }

            ref var damage = ref sender.Entity.Get<Damage>();
            ref var health = ref target.Entity.Get<HealthComponent>();
            health.hp -= damage.damage;
        }
    }
}