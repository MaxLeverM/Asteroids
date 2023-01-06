using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.UI;
using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.Systems
{
    public class PlayerDestroyHandlerSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerControlledComponent, Owner, DestroyEvent> filter;
        private UIMediator ui;
        
        public void Run()
        {
            foreach (var i in filter)
            {
                var owner = filter.Get2(i);
                owner.master.Get<GameOverEvent>();
            }
        }
    }
}