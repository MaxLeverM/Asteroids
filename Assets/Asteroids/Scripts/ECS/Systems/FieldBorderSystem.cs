using Asteroids.Scripts.Core;
using Asteroids.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.Systems
{
    public class FieldBorderSystem : IEcsRunSystem
    {
        private EcsFilter<TransformComponent, MovableComponent> movableFilter = null;
        private FieldBound fieldBound;

        public void Run()
        {
            foreach (var i in movableFilter)
            {
                ref var fieldObject = ref movableFilter.Get1(i);

              //  if (Mathf.Abs(fieldObject.transform.position.x) <= fieldBound.ExtremePoint.x &&
                //    Mathf.Abs(fieldObject.transform.position.y) <= fieldBound.ExtremePoint.y) continue;
                
                if (fieldObject.transform.position.x > fieldBound.ExtremePoint.x)
                    fieldObject.transform.position = new Vector2(-fieldBound.ExtremePoint.x, fieldObject.transform.position.y);
                if (fieldObject.transform.position.x < -fieldBound.ExtremePoint.x)
                    fieldObject.transform.position = new Vector2(fieldBound.ExtremePoint.x, fieldObject.transform.position.y);
                if (fieldObject.transform.position.y > fieldBound.ExtremePoint.y)
                    fieldObject.transform.position = new Vector2(fieldObject.transform.position.x, -fieldBound.ExtremePoint.y);
                if (fieldObject.transform.position.y < -fieldBound.ExtremePoint.y)
                    fieldObject.transform.position = new Vector2(fieldObject.transform.position.x, fieldBound.ExtremePoint.y);
            }
        }
    }
}