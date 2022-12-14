using Asteroids.Scripts.ECS.Components;
using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.Systems
{
    public class RechargeFireSystem : IEcsRunSystem
    {
        private EcsFilter<RechargeTimeComponent, FireEvent> rechargeFireFilter;
        public void Run()
        {
            foreach (var i in rechargeFireFilter)
            {
                ref var rechargeTimer = ref rechargeFireFilter.Get1(i);

                if (rechargeTimer.currentRechargeTime == 0f)
                {
                    rechargeFireFilter.GetEntity(i).Get<RechargingFlag>();
                }
                else
                {
                    rechargeFireFilter.GetEntity(i).Del<FireEvent>();
                }
            }
        }
    }
}