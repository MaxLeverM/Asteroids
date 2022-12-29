using UnityEngine;

namespace Asteroids.Scripts.ECS.Components
{
    public struct MoveEngineComponent
    {
        public float force;
        public Vector2 moveDirection;
    }

    public struct RotationEngineComponent
    {
        public float rotationOffset;
        public Vector3 lookAtPosition;
    }
}