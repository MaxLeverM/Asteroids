using UnityEngine;

namespace Asteroids.Scripts.ECS.Components
{
    public struct SpaceEngineComponent
    {
        public float force;// = 0.5f;
        public float rotationOffset;// = 90f;
        public Vector3 lookAtPosition;
        public Vector2 moveDirection;
    }
}