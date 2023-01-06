using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.Components
{
    public class DamageSystem : IEcsRunSystem
    {
        private EcsFilter<DamageEvent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var damageEvent = ref filter.Get1(i);

                if (damageEvent.damageTarget.Has<HealthComponent>())
                {
                    ref var targetHealth = ref damageEvent.damageTarget.Get<HealthComponent>();
                    targetHealth.hp -= damageEvent.damage;

                    if (filter.GetEntity(i).Has<FinalDamageOwner>())
                    {
                        ref var finalDamageOwner = ref damageEvent.damageTarget.Get<FinalDamageOwner>();
                        finalDamageOwner.owner = filter.GetEntity(i).Get<FinalDamageOwner>().owner;
                    }
                }
                
                filter.GetEntity(i).Destroy();
            }
        }
    }
}