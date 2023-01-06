using Asteroids.Scripts.ECS.UnityComponents;
using LeoEcsPhysics;
using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.Components
{
    public class CollisionDamageSystem : IEcsRunSystem
    {
        private EcsFilter<OnTriggerEnter2DEvent> filter;
        private EcsWorld _world;

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

            var damage = _world.NewEntity();
            ref var damageEvent = ref damage.Get<DamageEvent>();
            damageEvent.damage = sender.Entity.Get<Damage>().damage;
            damageEvent.damageTarget = target.Entity;
            
            if (sender.Entity.Has<Owner>())
            {
                ref var damageOwner = ref damage.Get<FinalDamageOwner>();
                damageOwner.owner = sender.Entity.Get<Owner>().master;
            }
        }
    }
}