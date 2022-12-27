using Asteroids.Scripts.ECS.Components;
using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.Systems
{
    public class DestroyEntitySystem : IEcsRunSystem
    {
        private EcsFilter<DestroyEvent> destroyFilter;

        public void Run()
        {
            foreach (var i in destroyFilter)
            {
                var ent = destroyFilter.GetEntity(i);
                ent.Destroy();
            }
        }
    }
}