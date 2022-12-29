using System;
using System.Collections.Generic;
using Asteroids.Scripts.ECS.Components;
using Leopotam.Ecs;

namespace Asteroids.Scripts.ECS.Destroy
{
    public class DestroyTagChildSystem: IEcsRunSystem
    {
        private EcsFilter<Child, DestroyEvent> filter;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var child = ref filter.Get1(i);
                TagDestroy(child.children);
            }
        }

        private void TagDestroy(List<EcsEntity> children)
        {
            foreach (var child in children)
            {
                child.Get<DestroyEvent>();
                child.Get<RecursiveStopFlag>();
                if (child.Has<Child>() && !child.Has<RecursiveStopFlag>())
                {
                    TagDestroy(child.Get<Child>().children);
                }
            }
        }
    }
}