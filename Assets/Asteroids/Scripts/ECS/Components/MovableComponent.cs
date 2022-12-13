using UnityEngine;

namespace Asteroids.Scripts.ECS.Components
{
    public struct MovableComponent
    {
        public Transform transform;
        public Vector2 velocity;
        public float damping;
        public float maxSpeed;
    }
}