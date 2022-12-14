using Asteroids.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Systems
{
    public class RechargeTimerSystem : IEcsRunSystem
    {
        private EcsFilter<RechargeTimeComponent, RechargingFlag> rechargingFilter;
        public void Run()
        {
            foreach (var i in rechargingFilter)
            {
                ref var rechargeTimer = ref rechargingFilter.Get1(i);
                
                rechargeTimer.currentRechargeTime += Time.deltaTime;
                
                if (rechargeTimer.currentRechargeTime >= rechargeTimer.timeToRecharge)
                {
                    rechargeTimer.currentRechargeTime = 0f;
                    rechargingFilter.GetEntity(i).Del<RechargingFlag>();
                }
            }
        }
    }
}