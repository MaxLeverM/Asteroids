using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.Components
{
    public struct DamageEvent
    {
        public float damage;
        public EcsEntity damageTarget;
    }
}