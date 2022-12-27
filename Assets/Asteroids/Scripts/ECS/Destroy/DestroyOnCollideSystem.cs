using Asteroids.Scripts.ECS.Components;
using Asteroids.Scripts.ECS.UnityComponents;
using LeoEcsPhysics;
using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.Destroy
{
    public class DestroyOnCollideSystem : IEcsRunSystem
    {
        private EcsFilter<OnTriggerEnter2DEvent> filter;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var collider = ref filter.Get1(i);

                if (!collider.senderGameObject.TryGetComponent(out BaseView sender)) continue;
                if (!sender.Entity.Has<DestroyOnCollide>()) continue;
                sender.Entity.Get<DestroyEvent>();
            }
        }
    }
}