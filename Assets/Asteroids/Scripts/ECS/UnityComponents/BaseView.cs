using Leopotam.Ecs;
using UnityEngine;

namespace Asteroids.Scripts.ECS.UnityComponents
{
    public abstract class BaseView : MonoBehaviour
    {
        public EcsEntity Entity { get; set; }

        public virtual void ResetView()
        {
            Entity = default;
        }
    }
}