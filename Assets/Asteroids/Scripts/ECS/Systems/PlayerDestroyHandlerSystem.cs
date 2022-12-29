using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.UI;
using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.Systems
{
    public class PlayerDestroyHandlerSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, Score, DestroyEvent> filter;
        private UIMediator ui;
        
        public void Run()
        {
            foreach (var i in filter)
            {
                ui.GameOverView.ShowGameOverScreen(filter.Get2(i).score);
            }
        }
    }
}