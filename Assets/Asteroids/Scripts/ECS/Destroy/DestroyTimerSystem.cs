using Asteroids.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Systems
{
    public class DestroyTimerSystem : IEcsRunSystem
    {
        private EcsFilter<DestroyTimeComponent> destroyFilter;
        public void Run()
        {
            foreach (var i in destroyFilter)
            {
                ref var destroyTimer = ref destroyFilter.Get1(i);
                if (destroyTimer.currentTime > destroyTimer.timeToDestroy)
                {
                    destroyFilter.GetEntity(i).Get<DestroyEvent>();
                    continue;
                }

                destroyTimer.currentTime += Time.deltaTime;
            }
        }
    }
}