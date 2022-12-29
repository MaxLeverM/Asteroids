using System.Collections.Generic;
using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.Components
{
    public struct Child
    {
        public List<EcsEntity> children;
    }
}