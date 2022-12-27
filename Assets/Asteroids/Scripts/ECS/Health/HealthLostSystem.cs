using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.Components
{
    public class HealthLostSystem : IEcsRunSystem
    {
        private EcsFilter<HealthComponent> healthFilter;
        public void Run()
        {
            foreach (var i in healthFilter)
            {
                ref var health = ref healthFilter.Get1(i);
                if (health.hp <= 0)
                {
                    var entity = healthFilter.GetEntity(i);
                    entity.Get<DestroyEvent>();
                }
            }
        }
    }
}