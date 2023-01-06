using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.UI;
using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.UI
{
    public class UIStarshipStatisticSystem : IEcsRunSystem
    {
        private EcsFilter<TransformComponent, PlayerControlledComponent, MovableComponent> filter;
        private UIMediator ui;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var playerTransform = ref filter.Get1(i);
                ref var movableComponent = ref filter.Get3(i);
                ui.GameStatisticView.OnPositionUpdate(playerTransform.transform.position);
                ui.GameStatisticView.OnRotationUpdate(playerTransform.transform.rotation);
                ui.GameStatisticView.OnVelocityUpdate(movableComponent.velocity);
            }
        }
    }
}