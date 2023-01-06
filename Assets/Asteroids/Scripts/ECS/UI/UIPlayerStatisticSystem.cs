using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.UI;
using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.UI
{
    public class UIPlayerStatisticSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, Score> filterScore;
        private UIMediator ui;
        public void Run()
        {
            foreach (var i in filterScore)
            {
                ref var scoreComponent = ref filterScore.Get2(i);
                ui.GameStatisticView.OnScoreValueChanged(scoreComponent.score);
            }
        }
    }
}