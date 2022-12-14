using UnityEngine;

namespace Asteroids.Scripts.ECS.Components
{
    public struct MovableComponent
    {
        public Vector2 velocity;
        public float damping;
        public float maxSpeed;
    }
}